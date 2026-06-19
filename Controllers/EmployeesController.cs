using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HRMS.Models;
using HRMS.DTOs.Employees;
using System.Runtime.InteropServices;
using System.Collections.Frozen;
using HRMS.DbContexts;



namespace HRMS.Controllers      //Project Name → HRMS  Folder → Controllers
{
    // Data Annotations --> Extra Information 

    [Route("api/[controller]")]  // This defines the API URL  [controller] --> Employees  class name 
    [ApiController] //This tells ASP.NET: This class is a Web API controller
    public class EmployeesController : ControllerBase
    {





        // Employee Class => model ,it mean the data for the employee like name ,age ... 
        //public static List<Employee> employees = new List<Employee>()
        //{
        //     // the object 

        //    new Employee() {Id =1 , FirstName = " Rama " , LastName = " Al Jabari " ,Email = " rama@test.com ",Postion = "AI", BirthDate = new DateTime(2005, 9 , 20 ) ,IsActive = true , PhoneNumber = " + 962 781310161", StartDate= new DateTime( 2025 , 12 , 1 ) , Salary = 6000 },
        //    new Employee() {Id =2 , FirstName = " Ali " , LastName = " Al mmm " ,Email = " mmm@test.com ",Postion = "AI", BirthDate = new DateTime(2001 , 9 , 20 ) ,IsActive = true , PhoneNumber = " + 962 781310161", StartDate= new DateTime( 2025 , 12 , 1 ) , Salary = 5000 },
        //    new Employee() {Id =3 , FirstName = " sarah " , LastName = " Al ttt " ,Email = " ttt@test.com ",Postion = "frontend", BirthDate = new DateTime(1999 , 9 , 20 ) ,IsActive = true , PhoneNumber = " + 962 781310161", StartDate= new DateTime( 2025 , 12 , 1 ) , Salary = 500},
        //    new Employee() {Id =4 , FirstName = " ahmmad" , LastName = " Al sss " ,Email = " sss@test.com ",Postion = "developer", BirthDate = new DateTime(2008 , 9 , 20 ) ,IsActive = true , PhoneNumber = " + 962 781310161", StartDate= new DateTime( 2025 , 12 , 1 ) , Salary = 100}

        //};




        // to connect to the DB using DBContext we have to creat an object form it 
        // 1. the typical method  : public HRMSContext _dbContext = new HRMSContext(); 
        // 2. Dependency Injection (what we will use ) 
       
        
        
        public  readonly HRMSContext _dbContext; // first : declear the variable -->  _dbcontext  // from type HRMSContext 
        //constructor 
        public EmployeesController(HRMSContext  dbContext)
        {
            _dbContext = dbContext;
        }






        // Endpoints --> methods 

        /* CRUD  
        C ---> Create ( post endpoint )
        R ---> Read  ( Get endpoint )
        U ---> update ( Put endpoint )
        D ---> Delete (delete endpoint )
        */





        // to git the list for all employee 
        [HttpGet("GetbyCriteria")]
        public IActionResult GetbyCriteria([FromQuery]  SearchEmpDTO searchDTO) // The Postion now is Nullble [FromQuery]string? postion , string? Name 
        {// LINQ Query syntxe 

            var data = from emp in _dbContext.Employees 
                        from dep in _dbContext.Departments.Where(x => x.Id == emp.DepartmentId).DefaultIfEmpty() //left join  
                        from man in _dbContext.Employees.Where(x => x.Id  == emp.managerId).DefaultIfEmpty()
                        where (string.IsNullOrEmpty(searchDTO.Postion) || emp.Postion.ToUpper().Contains(searchDTO.Postion.ToUpper()) && (searchDTO.Name == null || emp.FirstName.ToUpper().Contains(searchDTO.Name.ToUpper())))
                       orderby emp.Id
                       select new EmpDTO  // DTO ---> Data Transfer Object
                       {
                           Id = emp.Id,
                           Name = emp.FirstName,
                           Postion = emp.Postion,
                           BirthDate = emp.BirthDate,
                           StartDate = emp.StartDate,
                           EndDate = emp.EndDate,
                           DepartmentId = emp.DepartmentId ,
                           DepartmentName = dep.Name,
                           managerId = emp.managerId ,
                           ManagerName = man.FirstName 
                       };

            return Ok(data);

        }








        [HttpGet("{id}")] // Route Parameter 
        public IActionResult GetById(long id )  // The GETByID should always return one object , not Array 
        {

            //var data = _dbContext.Employees.Join(
            //    _dbContext.Departments,
            //    employee => employee.DepartmentId,
            //    department => department.Id,
            //    (employee, department) => new EmpDTO
            //    {
            //        Id = employee.Id,
            //        Name = employee.FirstName,
            //        Postion = employee.Postion,
            //        BirthDate = employee.BirthDate,
            //        StartDate = employee.StartDate,
            //        EndDate = employee.EndDate,
            //        DepartmentId = employee.DepartmentId,
            //        DepartmentName = department.Name

            //    }
            //    );




            var data = _dbContext.Employees.Select(x => new EmpDTO
            {
                Id = x.Id,
                Name = x.FirstName,
                Postion = x.Postion,
                BirthDate = x.BirthDate,
                StartDate = x.StartDate,
                EndDate = x.EndDate,
                DepartmentId = x.DepartmentId,
                DepartmentName = "",
                managerId = x.managerId,
                ManagerName = " "
            }).FirstOrDefault(x => x.Id == id);    // First --> return the first one which fulfills the condition, but if it not get a value it return run time exeption so for safty we use FirstOrDefault function --> if the value not found it will return the Default--> Null 


            if (data == null)
            {
                return NotFound("Employee Not Found");
            }

            return Ok(data);

        }








        [HttpPost]
        public IActionResult Add(SaveEmpDTO newEmployee)
        {

            var employee = new Employee()
            {   Id = 0, // employees.LastOrDefault()?.Id ?? 0  + 1,  //?. (Null-Conditional Operator it will return NULL for the whole expression )  and  ?? -->  (If the value on the left is null, use the value on the right)
                FirstName = newEmployee.FirstName,
                LastName = newEmployee.LastName,
                Postion = newEmployee.Postion,
                BirthDate = newEmployee.BirthDate,
                StartDate = newEmployee.StartDate,
                EndDate = newEmployee.EndDate,
                Email = newEmployee.Email,
                IsActive = newEmployee.IsActive,
                PhoneNumber = newEmployee.PhoneNumber,
                Salary = newEmployee.Salary, 
                DepartmentId = newEmployee.DepartmentId,
                managerId =newEmployee.managerId
            };

            _dbContext.Add(employee);//prepare the values 
            _dbContext.SaveChanges();//Go to the DB 

            return Ok(employee.Id); 
          
        }






        [HttpPut]
        public IActionResult Update(SaveEmpDTO updateEmployee)
        {


            var employee = _dbContext.Employees.FirstOrDefault(x => x.Id == updateEmployee.Id);

            if(employee == null)
            {
                return NotFound("The Employee Not Exist ");
            }

            employee.FirstName = updateEmployee.FirstName;
            employee.LastName = updateEmployee.LastName;
            employee.Postion = updateEmployee.Postion; 
            employee.BirthDate= updateEmployee.BirthDate;
            employee.StartDate = updateEmployee.StartDate;
            employee.EndDate = updateEmployee.EndDate;
            employee.Email = updateEmployee.Email;
            employee.IsActive = updateEmployee.IsActive;
            employee.Salary = updateEmployee.Salary;
            employee.DepartmentId = updateEmployee.DepartmentId;
            employee.managerId = updateEmployee.managerId;


            _dbContext.SaveChanges();


            return Ok(); 


        }






        [HttpDelete]
         
        public IActionResult  Delete(long id)
        {
            var employee = _dbContext.Employees.FirstOrDefault(x => x.Id == id); 

            if (employee == null )
            {
                return NotFound("The Employee is Not Exist ");
            }

            _dbContext.Employees.Remove(employee);
            _dbContext.SaveChanges();
            return Ok();
            
        }


























        //[HttpGet ("GetAll") ] //  endpoint name 
        //public IActionResult Get()
        //{

        //  //return Ok ( new { Name = " Ahmad ", Age = 22 }) ;  // Http Response : Data , 200
        //   return NotFound("NO Data Found "); // Http Response : Data  , 404 
        //    //return BadRequest("Data Not loaded "); // Http Response : Data , 400 
        //    //return StatusCode(500, "An Error Occurred "); // Http Response : Data , 500 
        //}

        //[HttpGet]
        //public IActionResult GetEmployee()
        //{
        //    return Ok();
        //}


    }
}





// rule to follw ---> any thing return to the user should be DTO , not model 
//so for each request to any endpoint -->  the ASP.Net will create new object , so the object  are Isolated 








/*  Each endpoint has different ways to send data from the client
 
 
 1. Query Parameter (used in the Get endpoint )
Request URL : https://localhost:7159/api/Employees/GetbyCriteria?postion=AI

Simple DataType ==> sting , int , long ,... --->(ByDefault)it is Query Parameter 

[fromQuery]

--------------------------------------------------------------

 2. Request Body ( used in the post endpoint ) 

Request URL : https://localhost:7159/api/Employees

complex DataType ==> DTO , Model , Object ,.. ---> (ByDefault) it is  Request Body 

[fromBody]

------------------------------------------------------------
 SO we can use it and determini what to put & where 
ex : 
public IActionResult Add( [fromQuery]long Id , [fromBody] SaveEmpDTO newEmployee) 


-------------------------------------------------------------
method can use  multible Parameters of  types [fromQuery]
method can't use  multible Parameters of type [fromBody]

-------------------------------------------------------------
Http post\put : Can use Both Body\Query  , but we will only use  [FromBody]

Http  delete : can use Both Body\Query , but we will only use [FromQuery] and Rout parameter

Http Get :  can only use [FormQuery] and  Rout parameter

-----------------------------------------------------------

3. Rout Parameter  

Request URL : https://localhost:7159/api/Employees/1 
[HttpGet("{id}")]


 */ 