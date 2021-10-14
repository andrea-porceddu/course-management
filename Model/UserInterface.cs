using System;
using System.Collections.Generic;
using NodaTime;
using NodaTime.Text;

namespace CourseManagement.Model
{
  public class UserInterface {

    public CourseService CourseService { get; set; }
    
    const string CONSOLE_ANSWER = "=> ";

    const string CONSOLE_MENU = @"
      ******************** CONSOLE MENU ************************
      ***                                                    ***    
      ***    enter 'c' to create a new course                ***
      ***    enter 'e' to create a new course edition        ***
      ***                                                    ***
      ***    enter 'v' to see all courses                    ***
      ***    enter 'w' to see all course editions            ***
      ***                                                    ***
      ***    enter 'f' to find a course edition by course    ***
      ***                                                    ***
      ***    enter 'q' to quit                               ***
      ***                                                    ***
      **********************************************************
    ";

    public UserInterface(CourseService service)
    {
      CourseService = service;
    }
    
    public void Start()
    {
      bool quit = false;
      do
      {
        // Console.WriteLine(CONSOLE_DIVIDER);
        Console.WriteLine(CONSOLE_MENU);
        char a = ReadChar(CONSOLE_ANSWER);
        switch (a)
        {
          case 'c':
            CreateCourse();
            break;
          case 'e':
            CreateCourseEdition();
            break;
          case 'v':
            ShowCourses();
            break;
          case 'w':
            ShowCourseEditions();
            break;
          case 'f':
            ShowCourseEditionsByCourse();
            break;
          case 'q':
            quit = true;
            break;
        }
      } while (!quit);
    }

    #region Course
    private Course CreateCourse()
    {
      long id = GenerateCourseId();
      string title = ReadString("Enter course title: ");
      int totalHours = (int)ReadLong("Enter total course hours: ");
      string description = ReadString("Enter course description: ");
      decimal price = ReadDecimal("Enter course price: ");

      Course c = new Course(
        id: id,
        title: title,
        totalHours: totalHours,
        description: description,
        price: price
      );

      CourseService.CreateCourse(c);
      return c;
    }

    private void ShowCourses()
    {
      List<Course> courses = (List<Course>)CourseService.GetAllCourses();
      if (courses.Count > 0) {
        foreach (var c in courses)
        {
          Console.WriteLine(c.ToString());
        }
      }
      else
      {
        Console.WriteLine("There aren't courses at the moment, please enter one.");
      }
    }

    private long GenerateCourseId() {
      List<Course> courses = (List<Course>)CourseService.GetAllCourses();
      int i = 1;
      foreach (var c in courses)
      {
        i++;
      }
      return i;
    }
    #endregion
    
    #region CourseEdition
    private CourseEdition CreateCourseEdition()
    {
      long id = GenerateCourseEditionId();
      long courseId = ReadLong("Which course do you want to associate with this edition? Enter the id of the course: ");
      if (!CourseExists(courseId)) {
        Console.WriteLine("You cannot associate the edition with this course because the course does not exist.");
        return null;
      }
      string code = ReadString("Enter course edition code: ");
      LocalDate start = ReadLocalDate("Please follow this format (yyyy-MM-dd) to insert a course edition start date: ");
      LocalDate end = ReadLocalDate("Please follow this format (yyyy-MM-dd) to insert a course edition end date: ");
      int capacity = (int)ReadLong("Enter the maximum number of students who can enroll in this edition: ");
      decimal price = ReadDecimal("Enter course edition price: ");
      bool isVirtual = IsVirtual("Will this edition take place remotely? [y/n]:");

      CourseEdition e = new CourseEdition(
        id: id,
        courseId: courseId,
        code: code,
        start: start,
        end: end,
        capacity: capacity,
        price: price,
        isVirtual: isVirtual
      );

      CourseService.CreateCourseEdition(e);
      return e;
    }

    private void ShowCourseEditions()
    {
      List<CourseEdition> editions = (List<CourseEdition>)CourseService.GetAllCourseEditions();
      if (editions.Count > 0) {
        foreach (var e in editions)
        {
          Console.WriteLine(e.ToString());
        }
      }
      else
      {
        Console.WriteLine("There aren't course editions at the moment, please enter one.");
      }
    }

    private void ShowCourseEditionsByCourse() {
      long courseId = ReadLong("Enter the course id to find its editions: ");
      if (!CourseExists(courseId)) {
        Console.WriteLine(@"
          This course does not exist, enter an existing course id or:
          Press 'v' to see all available courses.
          Press 'c' to create a course.
        ");
      }
      else
      {
        List<CourseEdition> editions = (List<CourseEdition>)CourseService.GetAllCourseEditions();
        if (editions.Count > 0) {
          foreach (var e in editions)
          {
            if (e.CourseId == courseId) {
              Console.WriteLine(e.ToString());
            }
            else
            {
              Console.WriteLine("This course has no editions, you can enter one by pressing 'e'.");
            }          
          }
        }
        else
        {
          Console.WriteLine("There aren't course editions at the moment, please enter one.");
        }
      }
    }

    private long GenerateCourseEditionId() {
      List<CourseEdition> editions = (List<CourseEdition>)CourseService.GetAllCourseEditions();
      int i = 1;
      foreach (var e in editions)
      {
        i++;
      }
      return i;
    }

    #endregion

    #region Helpers

    private string ReadString(string prompt)
    {
      string answer = null;
      do
      {
        Console.Write(prompt);
        answer = Console.ReadLine().Trim();
        if (string.IsNullOrEmpty(answer))
        {
          Console.WriteLine("You haven't entered anything, please try again.");
        }
      } while (string.IsNullOrEmpty(answer));
      return answer;
    }

    private char ReadChar(string prompt)
    {
      return ReadString(prompt)[0];
    }

    private long ReadLong(string prompt)
    {
      bool isNumber = false;
      long num;
      do
      {
        string answer = ReadString(prompt);
        isNumber = long.TryParse(answer, out num);
        if (!isNumber)
        {
          Console.WriteLine("Please enter a number.");
        }
      } while (!isNumber);
      return num;
    }

    private decimal ReadDecimal(string prompt)
    {
      bool isNumber = false;
      decimal decNum;
      do
      {
        string answer = ReadString(prompt);
        isNumber = decimal.TryParse(answer, out decNum);
        if (!isNumber)
        {
          Console.WriteLine("please enter a number.");
        }
      } while (!isNumber);
      return decNum;
    }

    private LocalDate ReadLocalDate(string prompt)
    {
      bool success = false;
      LocalDate date;
      do
      {
        string answer = ReadString(prompt);
        success = DateParse(answer, out date);
      } while (!success);
      return date;
    }

    private bool DateParse(string input, out LocalDate date)
    {
      var pattern = LocalDatePattern.CreateWithInvariantCulture("yyyy-MM-dd");
      var parseResult = pattern.Parse(input);
      date = parseResult.GetValueOrThrow();
      return true;
    }

    private bool IsVirtual(string prompt)
    {
      string opt = "";
      bool boolVal = false;
      do {
        opt = ReadString(prompt);
        if (opt == "y")
        {
          boolVal = true;
        }
      } while (opt != "y" && opt != "n" && opt != "");
      return boolVal;
    }

    public bool CourseExists(long id) {
      List<Course> courses = (List<Course>)CourseService.GetAllCourses();
      foreach (var c in courses)
      {
        if (c.Id == id) return true;
      }
      return false;
    }
    #endregion

  }

}