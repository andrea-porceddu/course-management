using System.Collections.Generic;

namespace CourseManagement.Model.Data
{
  public class LocalRepository : IRepository
  {
    private List<Course> courses = new List<Course>();

    private List<CourseEdition> editions = new List<CourseEdition>();

    public Course AddCourse(Course c)
    {
      courses.Add(c);
      return c;
    }

    public List<Course> GetCourses()
    {
      return courses;
    }

    public CourseEdition AddCourseEdition(CourseEdition e)
    {
      editions.Add(e);
      return e;
    }

    public List<CourseEdition> GetCourseEditions()
    {
      return editions;
    }
  }
}
