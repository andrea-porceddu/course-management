using System.Collections.Generic;

namespace CourseManagement.Model.Data
{
    public interface IRepository
    {
      Course AddCourse(Course c);
      List<Course> GetCourses();
      CourseEdition AddCourseEdition(CourseEdition e);
      List<CourseEdition> GetCourseEditions();
    }
}