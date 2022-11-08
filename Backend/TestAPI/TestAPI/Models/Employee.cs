namespace TestAPI.Models
{
    public class Employee
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public virtual Department Department { get; set; }
        public Guid DepartmentId { get; set; }

        public DateTime DateOfJoining { get; set; }
        public string img { get; set; }


    }
}
