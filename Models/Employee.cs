using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRMS.Models
{
    public class Employee
    {
        [Key]
        public long Id { get; set; }

        [MaxLength(50)]
        public string FirstName { get; set; }

        [MaxLength(50)]
        public string LastName { get; set; }

        [MaxLength(50)]

        public string Email { get; set; }

        [MaxLength(50)]

        public string Postion { get; set; }
        public DateTime BirthDate { get; set; }

        [MaxLength(50)]

        public string PhoneNumber { get; set; }

        public bool IsActive { get; set; }

        public DateTime StartDate { get; set; }//Requierd 
        public DateTime? EndDate { get; set; }//? --> this data type is optional / Nullable 
        public decimal? Salary { get; set; }



        // to make the DepartmentID foreign key to Department model ( Department? )
        [ForeignKey ("Department")]
        public long? DepartmentId { get; set; }
        
        public Department? Department { get; set; } // Navigation Property 





        [ForeignKey("Manager")]

        public long? managerId { get; set; }

        public Employee? Manager { get; set; } // Navigation Property 


        //public ICollection<Employee>? Employees { get; set; }





    }
}

