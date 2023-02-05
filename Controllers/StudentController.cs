using library_management_system.Controllers.Logic;
using library_management_system.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace library_management_system.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(Roles = "student")]
    public class StudentController:ControllerBase
    {
        
        public string connectionString = "Data Source=\"(localdb)\\Library system\";Initial Catalog=\"Library Management\";Integrated Security=True";
        
        /*private readonly IConfiguration _config;
        public StudentController(IConfiguration config)
        {
            _config = config;
            connectionString = _config.GetValue<string>("ConnectionStrings:DefaultConnection");
        }*/

        LogicStudent student = new LogicStudent();
        LogicLibrarian librarian= new LogicLibrarian();

        [HttpGet]
        [AllowAnonymous]
        public string GetBooks()
        {
            try
            {
                string result = student.GetBooksLogic();
                return result;

            }
            catch (Exception ex) 
            {
                return ex.Message;
            }
        }

        [HttpPost]
        public async Task<string> IssueBook(string jsonFile)
        {
            string result = await student.IssueBookLogic(jsonFile);
            return result;
        }


        [HttpPost]
        public async Task<string> IssueBook1(IssueBook Details)
        {

            string studentId = User.FindFirstValue(ClaimTypes.Name);
            string result = await student.IssueBookLogic1(Details,studentId);
            return result;
        }

        [HttpPost]
        public async Task<string> ReturnBook(string RentalId)
        {
            string studentId = User.FindFirstValue(ClaimTypes.Name);
            string result = await student.ReturnBookLogic(RentalId,studentId);
            return result;
        }

        [HttpGet]
        public async Task<string> SearchBook(string jsonFile)
        {
            string result = await student.SearchBookLogic(jsonFile);
            return result;
        }

        /*
        [HttpPost]
        public async Task<string> UpdateStudent(string jsonFile)
        {
            string result = await student.UpdateStudentLogic(jsonFile);
            return result;
        }
        */

    }
}
