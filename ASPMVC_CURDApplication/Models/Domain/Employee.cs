namespace ASPMVC_CURDApplication.Models.Domain
{
    //*** we're use this Domain Model (Employee) inside our DBClass 
    public class Employee
    {
        //*** GUID: because it have very low probability of being duplicated as it is 128-bit
        //integer(16 bytes) which allow to use GUID across all databse and computer without data
        //collision
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public long Salary { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Department { get; set; }

    }
}
