using HRMS.Models;
using Microsoft.EntityFrameworkCore;


namespace HRMS.DbContexts
{
    public class HRMSContext : DbContext 
    { // start with the constructer 

        public HRMSContext(DbContextOptions <HRMSContext>  options ) :base (options)
        {
            /* 
            Options :
            1. which Database : sql server , oracle , mysql 
            2. connection string : Server name , database name , if there name or password 
            */ 

        }



        // Tables 
        //<Employee> model that has the table columns 
        //Dbset Genaric type 
        //Employees the Table name 
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments   { get; set; }

    }
}
