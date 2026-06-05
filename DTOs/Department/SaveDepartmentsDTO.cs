namespace HRMS.DTOs.Department
{
    public class SaveDepartmentsDTO
    {
        public long? Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int? FloorNumber { get; set; }
    }
}
