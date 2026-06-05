using HRMS.DTOs.Department;
using HRMS.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.Intrinsics.Arm;


namespace HRMS.Controllers
{

    [Route("api/[controller]")]
    [ApiController]

    public class DepartmentController : ControllerBase
    {

        

        public static List<Department> departments = new List<Department>()
    {
        new Department() {Id = 1 , Name = "AI_Depatment", Description = "Ai department for build and train AI models ", FloorNumber = 1 } ,
        new Department() {Id = 2 , Name = "HR_Depatment", Description = "HR department for build and train AI models ", FloorNumber = 2 } ,
        new Department() {Id = 3 , Name = "Front_Depatment", Description = "Front department for build and train AI models ", FloorNumber = 3 } ,
        new Department() {Id = 4 , Name = "Backend_Depatment", Description = "Ai department for build and train AI models ", FloorNumber = 4 } ,
       
    };








        [HttpGet("GetByCriteria")]
        public IActionResult GetByCriteria(string? DName, int? DFloorNumber)
        {
            var data = from dep in departments
                       where (DName == null || dep.Name.ToUpper().Contains( DName.ToUpper()) )     && (DFloorNumber == null || dep.FloorNumber == DFloorNumber)          // for string to solve the upper\Lower miss match we can use .ToUpper() for both , of we can use StringComarison.OrdinalIgnoreCase    dep.Name.Contains( DName ,StringComarison.OrdinalIgnoreCase  ) )   . and we can ues Contain to accept any  value form the word
                       orderby dep.Id
                       select new SaveDepartmentsDTO
                       {
                           Id = dep.Id,
                           Name = dep.Name,
                           Description = dep.Description,
                           FloorNumber = dep.FloorNumber,
                       };

            return Ok(data);
        }













        [HttpGet("{Id}")]
        public IActionResult GetByID(long Id)
        { 
            var department = departments.Select(x => new DepartmentDTO
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                FloorNumber = x.FloorNumber,

            }).FirstOrDefault(x => x.Id == Id);

            if (department == null)
            {
                return NotFound("No Department ");
            }

            return Ok(department);

        }





        [HttpPost]

        public IActionResult Add( SaveDepartmentsDTO department )
        {
            var newdepartment = new Department()
            {
                Id = (departments.LastOrDefault()?.Id ?? 0) + 1,
                Name = department.Name,
                Description = department.Description,
                FloorNumber = department.FloorNumber,
            };
          
            return Ok(newdepartment.Id);
        }




        [HttpPut]
        public IActionResult Update (SaveDepartmentsDTO  Pdepartment )
        {
            var department = departments.FirstOrDefault(x => x.Id == Pdepartment.Id);
            if( department == null)
            {
                return NotFound("NO DEPARTMENT ");
            };

            department.Name = Pdepartment.Name;
            department.Description = Pdepartment.Description;
            department.FloorNumber = Pdepartment.FloorNumber;

            return Ok();
        }




        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var department = departments.FirstOrDefault(x => x.Id == id);
            if (department == null)
            {
                return NotFound("NO DEPARTMENT ");
            };

            departments.Remove(department);
            return Ok();
            
        }








        }

    }
