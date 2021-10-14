namespace CourseManagement.Model
{
  public class Course
  {
    public long Id { get; set; }
    public string Title { get; set; }
    public int TotalHours { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }

    public Course(long id, string title, int totalHours, string description, decimal price)
    {
      Id = id;
      Title = title;
      TotalHours = totalHours;
      Description = description;
      Price = price;
    }

    public override string ToString()
    {
      return $@"
        ID: {Id} 
        Title: {Title} 
        Total Hours: {TotalHours}
        Description: {Description}
        Price: {Price}
      ";
    }

  }

}