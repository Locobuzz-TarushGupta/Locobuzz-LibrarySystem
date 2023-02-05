using library_management_system.Controllers.Logic;
using library_management_system.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Net;

namespace library_management_system.DatabaseLayer
{
    public class DbStudent
    {
        private string connectionString = "Data Source=\"(localdb)\\Library system\";Initial Catalog=\"Library Management\";Integrated Security=True";
        public static int RentalId = 1;

        [HttpGet]
        public string GetAllBooksStudentDb()
        {
            try
            {
                string sqlQuery = "SELECT * from BookDetail where Quantity > 0";
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        con.Close();
              /*          var books = dt.AsEnumerable().Select(item => new BookDetails()
                        {
                            BookId = item[0].ToString(),
                            BookTitle = item[1].ToString(),
                            BookDescription = item[2].ToString(),
                            Author = item[3].ToString(),
                            Medium = item[4].ToString(),
                            Stream = item[5].ToString(),
                            Quantity = (int)Convert.ToDouble(item[6].ToString()),
                            RentPrice = (float)Convert.ToDouble(item[7].ToString()),
                            Status = item[8].ToString()
                        }).ToList();
                  *///      return books;
                        string result = string.Empty;
                        result = JsonConvert.SerializeObject(dt);
                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public DataTable GetAllLogs()
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM LogTable", con))
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    con.Close();
                }
            }
            return dt;
        }

        [HttpPost]
        public string UpdateStudentDb(string sName, string sAddress,string sEmail, string sPhone)
        {
            try
            {
                string sqlQuery = "Update StudentDetails Set StudentName = @StudentName WHERE (@StudentName IS NULL OR Stream = @StudentName); Update StudentDetails Set StudentAddress = @StudentAddress WHERE (@StudentAddress IS NULL OR StudentAddress = @StudentAddress); Update StudentDetails Set StudentEmail = @StudentEmail WHERE (@StudentEmail IS NULL OR Stream = @StudentEmail); Update StudentDetails Set StudentPhone = @StudentPhone WHERE (@StudentPhone IS NULL OR Stream = @StudentPhone)";
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@StudentName", sName.Trim() ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@StudentAddress", sAddress.Trim() ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@StudentEmail", sEmail.Trim() ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@StudentPhone", sPhone.Trim() ?? (object)DBNull.Value);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        con.Close();
                        string result = string.Empty;
                        result = JsonConvert.SerializeObject(dt);
                        return result;
                    }
                }

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [HttpPost]
        public string SearchBookDb(string Stream, string Medium, string BookTitle)
        {
            try
            {
                string sqlQuery = "SELECT * FROM BookDetail WHERE (@stream IS NULL OR Stream = @stream) AND (@bookTitle  IS NULL OR BookTitle = @bookTitle) AND (@medium  IS NULL OR Medium = @medium)";
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@stream", Stream.Trim() ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@medium", Medium.Trim() ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@bookTitle", BookTitle.Trim() ?? (object)DBNull.Value);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        con.Close();
                        string result = string.Empty;
                        result = JsonConvert.SerializeObject(dt);
                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [HttpGet]
        DataTable GetBookDetails(string bookId)
        {
           
                DataTable dt = new DataTable();
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM BookDetail WHERE BookId = @bookId", con))
                    {
                        cmd.Parameters.AddWithValue("@bookId", bookId.Trim());
                        con.Open();
                        cmd.ExecuteNonQuery();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dt);
                        con.Close();
                    }
                }
                return dt;
        }

        public DataTable GetStudentDetailsAsyncDb(string StudentId)
        {
           // StudentDetails student = new StudentDetails();
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sqlQuery = "SELECT * from StudentDetails WHERE StudentID = @studentId";
                using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                {
                    cmd.Parameters.AddWithValue("@studentId", StudentId);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
          //          DataTable dt = new DataTable();
                    da.Fill(dt);
                    con.Close();
               //     string result = string.Empty;
               //     result = JsonConvert.SerializeObject(dt);
               //     student = JsonConvert.DeserializeObject<StudentDetails>(dt);
                    return dt;
                }
            }
            return dt;
        }

        [HttpGet]
        DataTable GetLogEntry(int rentalId)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sqlQuery = "Select * from LogTable where RentalId = @rentalId";
                using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                {
                    cmd.Parameters.AddWithValue("@rentalId", rentalId);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    con.Close();
                }
            }
            return dt;
        }

        [HttpGet, HttpPost]
        public string LogTableEntryDb(int rentalId, IssueBookModel data)
        {
            try
            {
                DataTable books = GetBookDetails(data.BookId);
                float RentPrice = (float)(double)books.Rows[0][6];
                DateTime date1 = data.IssueDate;
                DateTime date2 = data.ReturnDate;
                TimeSpan time = date2.Subtract(date1);
                int RentDays = (int)time.TotalDays;
                string sqlQuery = "INSERT INTO LogTable (RentalId,BookId,StudentId,IssueDate,ReturnDate,RentTotal,HasReturned,Penalty)    VALUES (@RentalId,@BookId,@StudentId,@IssueDate,@ReturnDate,@RentTotal,@HasReturned,@Penalty)";
                
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@RentalId", rentalId);
                        cmd.Parameters.AddWithValue("@BookId", data.BookId.Trim());
                        cmd.Parameters.AddWithValue("@StudentId", data.StudentId.Trim());
                        cmd.Parameters.AddWithValue("@IssueDate", data.IssueDate);
                        cmd.Parameters.AddWithValue("@ReturnDate", data.ReturnDate);
                        cmd.Parameters.AddWithValue("@RentTotal", RentDays * RentPrice);
                        cmd.Parameters.AddWithValue("@HasReturned", 0);
                        cmd.Parameters.AddWithValue("@Penalty", 0);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        return "Log added successfully.";
                    }
                }

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        [HttpGet, HttpPost]
        public string LogTableEntryDb1(int rentalId, IssueBook data, StudentDetails studentDetails)
        {
            try
            {
                DataTable books = GetBookDetails(data.BookId);
                float RentPrice = (float)Convert.ToDouble(books.Rows[0][6]);
                DateTime date1 = DateTime.Today.Date;
                DateTime date2 = data.ReturnDate;
                TimeSpan time = date2.Subtract(date1);
                int RentDays = (int)time.TotalDays;
                string sqlQuery = "INSERT INTO LogTable (RentalId,BookId,StudentId,IssueDate,ReturnDate,RentTotal,HasReturned,Penalty)    VALUES (@RentalId,@BookId,@StudentId,@IssueDate,@ReturnDate,@RentTotal,@HasReturned,@Penalty)";

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@RentalId", rentalId);
                        cmd.Parameters.AddWithValue("@BookId", data.BookId.Trim());
                        cmd.Parameters.AddWithValue("@StudentId", studentDetails.StudentId.Trim());
                        cmd.Parameters.AddWithValue("@IssueDate", date1);
                        cmd.Parameters.AddWithValue("@ReturnDate", data.ReturnDate);
                        cmd.Parameters.AddWithValue("@RentTotal", RentDays * RentPrice);
                        cmd.Parameters.AddWithValue("@HasReturned", 0);
                        cmd.Parameters.AddWithValue("@Penalty", 0);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        return "Log added successfully.";
                    }
                }

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [HttpGet,HttpPost,HttpPut]
        public string IssueBookDb(string BookId, string StudentId, DateTime ReturnDate,int RentalID)
        {

            DataTable dt = GetBookDetails(BookId);
            int quantity = 0;
            if (dt.Rows.Count == 0 || (int)dt.Rows[0][5] == 0) return "Book is Not-Available";
            string sqlQuery = "SELECT Quantity FROM BookDetail WHERE BookId = @bookId";
            quantity = (int)dt.Rows[0][5];
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                sqlQuery = String.Format("UPDATE BookDetail SET Quantity = {0} WHERE BookId = @bookId",quantity - 1);
                using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                {
                    cmd.Parameters.AddWithValue("@BookId", BookId.Trim());
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    IssueBookModel issueBookDetails = new IssueBookModel()
                    {
                        BookId = BookId,
                        StudentId = StudentId,
                        IssueDate = DateTime.Now,
                        ReturnDate = ReturnDate,
                        RentPrice = float.Parse((dt.Rows[0][5]).ToString(),System.Globalization.CultureInfo.InvariantCulture)
                    };
                  //  LogTableEntryDb1(RentalID, issueBookDetails);
                    return "Book Issued successfully. Rental ID: " + (RentalID);
                }
            }
        }



        [HttpPost]
        public string ReturnBookDb(string BookRentalId, string studentId)
        {
            try
            {
                int RentalId = Int32.Parse(BookRentalId);
                DataTable logEntry = GetLogEntry(RentalId);
                string dataStudentID = logEntry.Rows[0][2].ToString();
                dataStudentID = String.Concat(dataStudentID.Where(c => !Char.IsWhiteSpace(c)));
                if (studentId != dataStudentID)
                {
                    return "Current User is not same as who Issued the book.";
                }
                string bookId = logEntry.Rows[0][1].ToString();
                DataTable book = GetBookDetails(bookId);
                DateTime ReturnDate = (DateTime)logEntry.Rows[0][5];
                DateTime IssueDate = (DateTime)logEntry.Rows[0][4];
                float penalty = 0.0f;

                if(DateTime.Today > ReturnDate)
                {
                    TimeSpan time = DateTime.Today.Subtract(ReturnDate);
                    int RentDays = (int)time.TotalDays;
                    penalty = RentDays * (float)book.Rows[0][6];
                }
                int quantity = (int)book.Rows[0][5];
                string sqlQuery = "UPDATE BookDetail SET BookDetail.Quantity = @quantity WHERE BookDetail.BookId = @bookId; UPDATE LogTable SET LogTable.HasReturned = 1,LogTable.Penalty = @penalty WHERE LogTable.RentalId = @RentalId";
                
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@bookId", bookId.Trim());
                        cmd.Parameters.AddWithValue("@quantity", quantity+1);
                        cmd.Parameters.AddWithValue("@RentalId", BookRentalId);
                        cmd.Parameters.AddWithValue("@penalty", penalty);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        con.Close();
                    }
                }

                if (penalty == 0.0f)
                {
                    return "Thankyou for the return.";
                } 
                else
                {
                    return "Please pay fine rupees" + penalty.ToString();
                }
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }

    }
}
