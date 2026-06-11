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




        //this endpoint that take the name and the foloorNuber and return the employee 

        [HttpGet("GetByCriteria")]

        public IActionResult GetbyCriteria(string? DName , int? DFloorNumer)
        {
            var Data = from dep in departments
                       where (dep.Name == null || dep.Name.ToUpper().Contains( DName.ToUpper()) )  && (dep.FloorNumber == null || dep.FloorNumber == DFloorNumer)
                       orderby dep.Id
                       select new SaveDepartmentsDTO
                       {
                           Id = dep.Id,
                           Name = dep.Name,
                           FloorNumber = dep.FloorNumber,
                           Description = dep.Description,
                       };
            return Ok(Data);
        }





        //This endpoint retuern the Employee By his Id 

        [HttpGet("{EId}")]
        public IActionResult GetbyId(long EId)
        {
            var Emp = departments.Select(x => new DepartmentDTO
            {
                Id = x.Id,
                Name=x.Name,
                Description = x.Description,
                FloorNumber=x.FloorNumber,

            }).FirstOrDefault(x => x.Id == EId );


            if (Emp == null)
            {
                return NotFound("No Employee With This ID ");
            }

            return Ok(Emp);

        }




        // This endpoint To add new Department 
        [HttpPost("AddNewDepartment")]
        public IActionResult AddDepartment( SaveDepartmentsDTO Ndep)
        {
            var department = new Department()
            {
                Id = (departments.LastOrDefault()?.Id ?? 0) + 1,
                Name = Ndep.Name,
                Description = Ndep.Description,
                FloorNumber = Ndep.FloorNumber
            };

            return Ok($"Success Add new Department with Id {department.Id}");
        }




        //This endpoint for edit Department Data 
        [HttpPut("UpdatetheDepartment")]
        public IActionResult Update( SaveDepartmentsDTO Udep)
        {
            var department = departments.FirstOrDefault(x => x.Id == Udep.Id);
            if (department == null)
            {
                return NotFound("This Department Not Found "); 

            };

            department.Name = Udep.Name;
            department.Description = Udep.Description;
            department.FloorNumber = Udep.FloorNumber;


            return Ok($"Success Update the Department whit Id {department.Id}");
        }




        //this endpoint to Delete Department 

        [HttpDelete("DeleteDepatrment")]
        public IActionResult Delete(long  DId)
        {
            var department = departments.FirstOrDefault(x => x.Id == DId);
            if (department == null)
            {
                return NotFound(" There is No department with this Id ");
            };

            departments.Remove(department);
            return Ok($" Delete {DId} successful ");
        }







        //[HttpGet("GetByCriteria")]
        //public IActionResult GetByCriteria(string? DName, int? DFloorNumber)
        //{
        //    var data = from dep in departments
        //               where (DName == null || dep.Name.ToUpper().Contains( DName.ToUpper()) )  && (DFloorNumber == null || dep.FloorNumber == DFloorNumber)          // for string to solve the upper\Lower miss match we can use .ToUpper() for both , of we can use StringComarison.OrdinalIgnoreCase    dep.Name.Contains( DName ,StringComarison.OrdinalIgnoreCase  ) )   . and we can ues Contain to accept any  value form the word
        //               orderby dep.Id
        //               select new SaveDepartmentsDTO
        //               {
        //                   Id = dep.Id,
        //                   Name = dep.Name,
        //                   Description = dep.Description,
        //                   FloorNumber = dep.FloorNumber,
        //               };

        //    return Ok(data);
        //}






        //[HttpGet("{Id}")]
        //public IActionResult GetByID(long Id)
        //{ 
        //    var department = departments.Select(x => new DepartmentDTO
        //    {
        //        Id = x.Id,
        //        Name = x.Name,
        //        Description = x.Description,
        //        FloorNumber = x.FloorNumber,

        //    }).FirstOrDefault(x => x.Id == Id);



        //    if (department == null)
        //    {
        //        return NotFound("No Department ");
        //    }

        //    return Ok(department);

        //}





        //[HttpPost]

        //public IActionResult Add( SaveDepartmentsDTO department )
        //{
        //    var newdepartment = new Department()
        //    {
        //        Id = (departments.LastOrDefault()?.Id ?? 0) + 1,
        //        Name = department.Name,
        //        Description = department.Description,
        //        FloorNumber = department.FloorNumber,
        //    };
          
        //    return Ok(newdepartment.Id);
        //}




        //[HttpPut]
        //public IActionResult Update (SaveDepartmentsDTO  Pdepartment )
        //{
        //    var department = departments.FirstOrDefault(x => x.Id == Pdepartment.Id);
        //    if( department == null)
        //    {
        //        return NotFound("NO DEPARTMENT ");
        //    };

        //    department.Name = Pdepartment.Name;
        //    department.Description = Pdepartment.Description;
        //    department.FloorNumber = Pdepartment.FloorNumber;

        //    return Ok();
        //}




        //[HttpDelete]
        //public IActionResult Delete(int id)
        //{
        //    var department = departments.FirstOrDefault(x => x.Id == id);
        //    if (department == null)
        //    {
        //        return NotFound("NO DEPARTMENT ");
        //    };

        //    departments.Remove(department);
        //    return Ok();
            
        //}





        }

    }
