namespace DemoAPI.Models.DTO
{
    public class AddEmployeeRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public long Salary { get; set; }
        public string Department { get; set; }
    }
}
