namespace ASPMVC_CURDApplication.Models
{
    public class AddEmployeeViewModel
    {
        //*** these properties values, are coming from the browser
        public string Name { get; set; }
        public string Email { get; set; }
        public long Salary { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Department { get; set; }
    }
}
