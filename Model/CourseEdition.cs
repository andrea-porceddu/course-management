using NodaTime;

namespace CourseManagement.Model
{
    public class CourseEdition
    {
        public long Id { get; set; }

        public string Code { get; set; }

        public LocalDate Start { get; set; }

        public LocalDate End { get; set; }

        public int Capacity { get; set; }

        public decimal Price { get; set; }

        public bool IsVirtual { get; set; }

        public Course Course { get; set; }

        public long CourseId { get; set; }

        public CourseEdition(long id, long courseId, string code, LocalDate start, LocalDate end, int capacity, decimal price, bool isVirtual)
        {
            Id = id;
            CourseId = courseId;
            Code = code;
            Start = start;
            End = end;
            Capacity = capacity;
            Price = price;
            IsVirtual = isVirtual;
        }

        public override string ToString()
        {
        return $@"
            ID: {Id}
            Course ID: {CourseId}
            Code: {Code}
            Capacity: {Capacity}
            Price: {Price}
            Is Virtual: {IsVirtual}
        ";
        }
    }
}
