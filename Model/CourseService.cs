using System;
using System.Collections.Generic;
using CourseManagement.Model.Data;

namespace CourseManagement.Model
{
  public class CourseService {

    public IRepository Repository { get; set;}

    public CourseService(IRepository repo)
    {
      Repository = repo;
    }

    public Course CreateCourse(Course c)
    {
      return Repository.AddCourse(c);
    }

    public List<Course> GetAllCourses()
    {
      return Repository.GetCourses();
    }

    public CourseEdition CreateCourseEdition(CourseEdition e)
    {
      return Repository.AddCourseEdition(e);
    }

    public List<CourseEdition> GetAllCourseEditions()
    {
      return Repository.GetCourseEditions();
    }

  }

}