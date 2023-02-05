using library_management_system.Controllers;
using library_management_system.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
//using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlClient;

namespace library_management_system.DatabaseLayer
{
    public class DbAuth
    {
        private string connectionString = "Data Source=\"(localdb)\\Library system\";Initial Catalog=\"Library Management\";Integrated Security=True";

        public string AddCredentials(string username, string passwordHash)
        {
            try
            {
                //string sqlQuery = "Select * from Credential where Username = @username";
                string sqlQuery = "Select * from StudCredentials where Username = @username";
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@username", username.Trim());
                        con.Open();
                        cmd.ExecuteNonQuery();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        con.Close();
                        if(dt.Rows.Count > 0)
                        {
                            return "0";
                           // return "Username already exists. Please try again with different username or try logging in";
                        }
                    }
                    sqlQuery = "Insert into StudCredentials(Username, PasswordHash) Values(@username, @passwordHash)";
                    //sqlQuery = "Insert into Credential(Username, PasswordHash) Values(@username, @passwordHash)";
                    using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@username", username.Trim());
                        cmd.Parameters.AddWithValue("@passwordHash", passwordHash); 
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        return "1";
                        //return "Student Registered successfully. Please login to continue.";
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string AddCredentials1(Userdto request, string passwordHash)
        {
            try
            {
                //string sqlQuery = "Select * from Credential where Username = @username";
                string sqlQuery = "Select * from StudCredentials where Username = @username";
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@username", request.StudentId.Trim());
                        con.Open();
                        cmd.ExecuteNonQuery();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        con.Close();
                        if (dt.Rows.Count > 0)
                        {
                            return "0";
                            // return "Username already exists. Please try again with different username or try logging in";
                        }
                    }

                    sqlQuery = "Select * from StudentDetails";
                    int number = 0;
                    using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        con.Close();
                        number = int.Parse(dt.Rows[0][0].ToString());
                    }
                    number = number + 1;
                    string studentID = "studentID" + number.ToString();

                    float rentdue = 0.0f, penaltydue = 0.0f;

                    sqlQuery = "Insert into StudentDetails(StudentName,StudentId,StudentAddress,StudentEmail,StudentPhone,PenaltyDue,RentDue) Values(@StudentName,@StudentId,@StudentAddress,@StudentEmail,@StudentPhone,@PenaltyDue,@RentDue)";
                    using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@StudentName", request.StudentName.Trim());
                        cmd.Parameters.AddWithValue("@StudentId", studentID);
                      //  cmd.Parameters.AddWithValue("@StudentId", request.StudentId.Trim());
                        cmd.Parameters.AddWithValue("@StudentAddress", request.StudentAddress.Trim());
                        cmd.Parameters.AddWithValue("@StudentEmail", request.StudentEmail.Trim());
                        cmd.Parameters.AddWithValue("@StudentPhone", request.StudentPhone.Trim());
                        cmd.Parameters.AddWithValue("@PenaltyDue", 0.0f);
                        cmd.Parameters.AddWithValue("@RentDue", 0.0f);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }

                    sqlQuery = "Insert into StudCredentials(Username, PasswordHash) Values(@username, @passwordHash)";
                    //sqlQuery = "Insert into Credential(Username, PasswordHash) Values(@username, @passwordHash)";
                    using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@username", request.StudentId.Trim());
                        cmd.Parameters.AddWithValue("@passwordHash", passwordHash);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        return studentID;
                        //return "Student Registered successfully. Please login to continue.";
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string VerifyCredentials(string username, string passwordHash)
        {
            try
            {
                DataTable dt = new DataTable();
                string sqlQuery = "Select * from StudCredentials where Username = @username AND PasswordHash = @passwordHash";
                //string sqlQuery = "Select * from Credential where Username = @username AND PasswordHash = @passwordHash";
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@username", username.Trim());
                        cmd.Parameters.AddWithValue("@passwordHash", passwordHash);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        
                        da.Fill(dt);
                        con.Close();
                        if (dt.Rows.Count == 0)
                        {
                            return "0";
                            //return "Invalid Username or Password.";
                        } else
                        {
                            return "2";
                            //return "Student Logged in successfully.";
                        }
                    }

                    return "0";
                    //return "Invalid Username or Password.";

                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
