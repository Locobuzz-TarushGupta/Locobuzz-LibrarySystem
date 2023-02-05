using library_management_system.DatabaseLayer;
using library_management_system.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Security.Claims;

namespace library_management_system.Controllers.Logic
{
    public class LogicStudent
    {
        DbStudent student = new DbStudent();
        public static int rentalId = 1;
        public string GetBooksLogic()
        {
            string result = student.GetAllBooksStudentDb();
            return result;
        }

        [HttpGet, HttpPost]
        public async Task<string> SearchBookLogic(string jsonFile)
        {
            dynamic data = JsonConvert.DeserializeObject<dynamic>(jsonFile);
            string Stream = data["Stream"];
            string Medium = data["Medium"];
            string BookName = data["Name"];
            string result = student.SearchBookDb(Stream, Medium, BookName);
            return result;
        }

        [HttpPost]
        public async Task<string> UpdateStudentLogic(string jsonFile)
        {
            dynamic data = JsonConvert.DeserializeObject<dynamic>(jsonFile);
            string StudentName = data["Name"];
            string StudentAddress = data["Address"];
            string StudentEmail = data["Email"];
            string StudentPhone = data["Phone"];
            string result = student.UpdateStudentDb(StudentName, StudentAddress, StudentEmail, StudentPhone);
            return result;
        }



        [HttpPost, HttpPut]
        public async Task<string> IssueBookLogic(string jsonFile) 
        {
            StudentDetails studentDetails = new StudentDetails();
            dynamic data = JsonConvert.DeserializeObject<dynamic>(jsonFile);
            string bookID = data["BookId"];
            //string studentID = data["StudentId"];
            string studentID = studentDetails.StudentId;
            DateTime returnDate = (DateTime)data["ReturnDate"];
            int rentalID = 0;
            string result = student.IssueBookDb(bookID, studentID, returnDate,rentalID);
            if (result != "Book is Not-Available")
            {
                return result.ToString();
            }
            string rentalDetails= student.LogTableEntryDb(rentalId,data);
            rentalId++;
            return rentalDetails;
        }

        [HttpPost, HttpPut]
        public async Task<string> IssueBookLogic1(IssueBook Details,string studentID)
        {
            DataTable studentDt = student.GetStudentDetailsAsyncDb(studentID);
            string bookID = Details.BookId;
            int RentalID = 0;
            RentalID = student.GetAllLogs().Rows.Count+1;
            DateTime returnDate = (DateTime)Details.ReturnDate.Date;
            string result = student.IssueBookDb(bookID, studentID, returnDate,RentalID);
            if (result == "Book is Not-Available")
            {
                return result.ToString();
            }
            StudentDetails studentDetail = new StudentDetails()
            {
                StudentName = studentDt.Rows[0][0].ToString(),
                StudentId = studentDt.Rows[0][1].ToString(),
                StudentAddress = studentDt.Rows[0][2].ToString(),
                StudentEmail = studentDt.Rows[0][3].ToString(),
                StudentPhone = studentDt.Rows[0][4].ToString(),
                PenaltyDue = (float)Convert.ToDouble(studentDt.Rows[0][5]),
                RentDue = (float)Convert.ToDouble(studentDt.Rows[0][6])
            };
            string rentalDetails = student.LogTableEntryDb1(RentalID, Details, studentDetail);
         //   rentalId++;
            return result.ToString();
        }

        [HttpPost]
        public async Task<string> ReturnBookLogic(string RentalId, string studentId)
        {
            string result = student.ReturnBookDb(RentalId, studentId);
            return result;
        }
    }
}
