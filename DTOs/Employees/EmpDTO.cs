namespace HRMS.DTOs.Employees
{
    public class EmpDTO
    {
        public long Id { get; set; }
        public string Name { get; set; } 

        public string Postion { get; set; }

        public DateTime  BirthDate { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        


    }
}
