using ASPMVC_CURDApplication.Models.Domain;
using Google.Protobuf.WellKnownTypes;
using Microsoft.EntityFrameworkCore;

namespace ASPMVC_CURDApplication.Data
{
    //*** the below class(DbContext) is inherit from the Microsoft.EntityFrameworkCore class library
    public class MVCDemoDBClass : DbContext
    {
        //*** constructor. options parameter so that we can pass the options back to the base class.
        public MVCDemoDBClass(DbContextOptions options) : base(options)
        {
        }
        //*** for property shortcut: prop and then press tab twice 
        //*** property type is DbSet<>
        //*** Employees will be the Table name in DB.
        public DbSet<Employee> Employees { get; set; }
    }
}
