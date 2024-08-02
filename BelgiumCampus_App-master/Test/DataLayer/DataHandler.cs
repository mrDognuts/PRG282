using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;
using System.Windows.Forms;
using Test.BusinessLogicLayer;

namespace Test.DataLayer
{
    internal class DataHandler
    {
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataTable table = new DataTable();
        string con = "Server= .;Initial Catalog= BelgiumCampusDB_PRG282;Integrated Security=SSPI";

        public DataTable getStudents()
        {
            SqlConnection conn = new SqlConnection(con);
            adapter = new SqlDataAdapter("GetStudentInfo",conn);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            table = new DataTable();
            adapter.Fill(table);
            return table;
        }
        public DataTable getModules()
        {
            string query = $"SELECT * FROM Modules ";

            adapter = new SqlDataAdapter(query, con);
            table = new DataTable();
            adapter.Fill(table);
            return table;
        }

        public void UpdateImage(string filePath, int studentID)
        {
            // Read the file into a byte array
            byte[] imageData = File.ReadAllBytes(filePath);

            // Open a connection to the database
            using (SqlConnection conn = new SqlConnection(con))
            {
                try
                {
                

                    // Create a command to insert the image
                    SqlCommand cmd = new SqlCommand("spUpdateImage", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                        // Add the image parameter and set its value
                        cmd.Parameters.Add("@Image", imageData);
                    cmd.Parameters.Add("@StudentID", studentID);

                    // Execute the command
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    
                } catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                    MessageBox.Show("Error connecting to database");
                }finally { conn.Close(); }
            }
        }

        public int getStudentID() //Get the last studentId
        {
                using (SqlConnection conn = new SqlConnection(con))
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand("spGetStudentID", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            return reader.GetInt32(0);
                        }
                        else
                        {
                            throw new Exception("No rows returned.");
                        }
                    }
                }
            

        }

        public byte[] GetImageData(int Id)
        {
            byte[] imageData = null;

            // Open a connection to the database
            using (SqlConnection conn = new SqlConnection(con))
            {


                // Create a command to get the image
                SqlCommand cmd = new SqlCommand("spGetStudentImage", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                    // Add the StudentID parameter and set its value
                    cmd.Parameters.AddWithValue("@stdID", Id);

                // Execute the command and get the image data
                conn.Open();
                imageData = (byte[])cmd.ExecuteScalar();
                
            }

            return imageData;
        }

        public void DeleteStudent(int Id)
        {
            using (SqlConnection conn = new SqlConnection(con))
            {
                try
                {
                   

                    // Create a command to insert the image
                    SqlCommand cmd = new SqlCommand("spDeleteStudent", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@stdID", Id);
                    // Execute the command
                    conn.Open();
                    cmd.ExecuteNonQuery();
                        MessageBox.Show("Student removed successfully");
                    
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                    MessageBox.Show("Error connecting to database");
                }
                finally { conn.Close(); }
            }
        }
        public void DeleteModule(int CourseCode)
        {
            using (SqlConnection conn = new SqlConnection(con))
            {
                try
                {
                    

                    // Create a command to insert the image
                    SqlCommand cmd = new SqlCommand("spDeleteModule", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@CourseCode", CourseCode);
                    // Execute the command
                    conn.Open();
                    cmd.ExecuteNonQuery();
                        MessageBox.Show("Module removed successfully");
                    
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                    MessageBox.Show("Error connecting to database");
                }
                finally { conn.Close(); }
            }
        }
        public void AddStudent(Students student)
        {
            
            using (SqlConnection conn = new SqlConnection(con))
            {
                try
                {
                    // Create a command to insert new student
                    SqlCommand cmd = new SqlCommand("spAddStudent", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@FirstName", student.FirstName1);
                        cmd.Parameters.AddWithValue("@Surname", student.Surname1);
                        cmd.Parameters.AddWithValue("@DOB", student.DOB1);
                        cmd.Parameters.AddWithValue("@Gender", student.Gender1);
                        cmd.Parameters.AddWithValue("@Phone", student.Phone1);
                    cmd.Parameters.AddWithValue("@CourseCode", student.CourseCode1);
                    // Execute the command
                    conn.Open();
                        cmd.ExecuteNonQuery();
                        
                        MessageBox.Show("Student added successfully");
                    
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                    MessageBox.Show("Error connecting to database");
                }
                finally { conn.Close(); }
            }
        }
        public void AddStudentAdress(string street, string city, string state, string country, string postalCode)
        {
           

            using (SqlConnection conn = new SqlConnection(con))
            {
                try
                {
                    // Create a command to insert  
                    SqlCommand cmd = new SqlCommand("spAddStudentAddress", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@street", street);
                        cmd.Parameters.AddWithValue("@city", city);
                        cmd.Parameters.AddWithValue("@prov", state);
                        cmd.Parameters.AddWithValue("@country", country);
                        cmd.Parameters.AddWithValue("@postalCode", postalCode);
                        // Execute the command
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                    MessageBox.Show("Error connecting to database");
                }
                finally { conn.Close(); }
            }
        }
       
        public void AddModule(Course Courses)
        {
            

            using (SqlConnection conn = new SqlConnection(con))
            {
                try
                {
                    

                    // Create a command to insert  
                    SqlCommand cmd = new SqlCommand("spAddModule", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    
                        cmd.Parameters.AddWithValue("@courseCode", Courses.CourseCode1);
                        cmd.Parameters.AddWithValue("@moduleName", Courses.ModuleName1);
                        cmd.Parameters.AddWithValue("@moduleDesc", Courses.ModuleDesc1);
                        cmd.Parameters.AddWithValue("@link", Courses.Links1);
                    // Execute the command
                    conn.Open();
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Module added Successfully");
                    
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                    MessageBox.Show("Error connecting to database");
                }
                finally { conn.Close(); }
            }
        }
        public void UpdateModulesData(int moduleID, string newModuleName, string newModuleDescription, string Links)
        {
            using (SqlConnection conn = new SqlConnection(con))
            {
                try
                {


                    // Create a command to insert  
                    SqlCommand cmd = new SqlCommand("spUpdateModulesData", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@moduleID", moduleID);
                    cmd.Parameters.AddWithValue("@newModuleName", newModuleName);
                    cmd.Parameters.AddWithValue("@newModuleDescription", newModuleDescription);
                    cmd.Parameters.AddWithValue("@link", Links);
                    // Execute the command
                    conn.Open();
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Module updated Successfully");

                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                    MessageBox.Show("Error connecting to database");
                }
                finally { conn.Close(); }
            }
        }

        public void UpdateStudentsData(int studentID, string newFirstName, string newSurname, DateTime newDOB, string newGender, string newPhone, int coursecode)
        {

            using (SqlConnection conn = new SqlConnection(con))
            {
                try
                {
                    

                    // Create a command to update the data in the Students table
                    SqlCommand cmd = new SqlCommand("spUpdateStudent", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                        // Add parameters and set their values
                        cmd.Parameters.AddWithValue("@FirstName", newFirstName);
                        cmd.Parameters.AddWithValue("@Surname", newSurname);
                        cmd.Parameters.AddWithValue("@DOB", newDOB);
                        cmd.Parameters.AddWithValue("@Gender", newGender);
                        cmd.Parameters.AddWithValue("@Phone", newPhone);
                        cmd.Parameters.AddWithValue("@studentID", studentID);
                        cmd.Parameters.AddWithValue("@CourseCode", coursecode);

                    // Execute the command
                    conn.Open();
                    cmd.ExecuteNonQuery();
                        MessageBox.Show("Students data updated successfully");
                    
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                    MessageBox.Show("Error updating Students data");
                }
                finally { conn.Close(); }
            }
        }
        public void UpdateStudentAddress(int studentID, string street, string city, string state, string country, string postalCode)
        {
           

            using (SqlConnection conn = new SqlConnection(con))
            {
                try
                {

                    SqlCommand cmd = new SqlCommand("spUpdateStudentAddress", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@newStreet", street);
                        cmd.Parameters.AddWithValue("@newCity", city);
                        cmd.Parameters.AddWithValue("@newProvince", state);
                        cmd.Parameters.AddWithValue("@newCountry", country);
                        cmd.Parameters.AddWithValue("@newPostalCode", postalCode);
                        cmd.Parameters.AddWithValue("@stdID", studentID);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                    MessageBox.Show("Error updating student address");
                }
                finally { conn.Close(); }
            }
        }
        public DataTable SearchStudent(int stdID)
        {
            using (SqlConnection conn = new SqlConnection(con))
            {
                SqlCommand cmd = new SqlCommand("spSearchStudent", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StudentID", stdID);

                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    return dt;
                }
            }
        }

        public DataTable SearchModule (int courseID)
        {
            using (SqlConnection conn = new SqlConnection(con))
            {
                SqlCommand cmd = new SqlCommand("spSearchModule", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@courseID", courseID);

                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    return dt;
                }
            }
        }

       
    }
}
