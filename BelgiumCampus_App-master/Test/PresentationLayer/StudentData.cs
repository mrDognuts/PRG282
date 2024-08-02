using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Test.BusinessLogicLayer;
using Test.DataLayer;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Test
{
    public partial class StudentData : Form
    {
        string imagePath = "";
        DataHandler handler = new DataHandler();
        public StudentData()
        {
            InitializeComponent();
        }

        private void btn_BackStudent_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnStudentCreate_Click(object sender, EventArgs e)
        {
            StudentAdded studentAdded = new StudentAdded();
            studentAdded.Show();

            Students student = new Students(txtName.Text,txtSurname.Text,txtGender.Text,txtPhone.Text, int.Parse(txtModules.Text), DateTime.Parse(txtDOB.Text));
            handler.AddStudent(student);
            handler.AddStudentAdress(txtStreet.Text, txtCity.Text, txtState.Text, txtCountry.Text, txtPostalCode.Text);
            int id = handler.getStudentID();
            if (imagePath != "")
            {
                handler.UpdateImage(imagePath, id);
            }
            dgvStudents.DataSource = handler.getStudents();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                picbxStudent.Image = new Bitmap(openFileDialog.FileName);
                imagePath = openFileDialog.FileName;
            }
        }

        public void LoadImage(int Id)
        {
            DataHandler handler = new DataHandler();
            // Get the image data from the database
            foreach (DataGridViewRow row in dgvStudents.SelectedRows)
            {
                if (row.Cells[6].Value.ToString() != "")
                {
                    byte[] imageData = handler.GetImageData(Id);


                    // Convert the image data into a MemoryStream
                    using (MemoryStream ms = new MemoryStream(imageData))
                    {
                        // Load the image from the MemoryStream into the PictureBox
                        picbxStudent.Image = Image.FromStream(ms);
                    }
                }
            }
        }

        private void dgvStudents_SelectionChanged(object sender, EventArgs e)
        {
            foreach(DataGridViewRow row in dgvStudents.SelectedRows)
            {
                txtStudentID.Text = row.Cells[0].Value.ToString();
                txtName.Text = row.Cells[1].Value.ToString();
                txtSurname.Text = row.Cells[2].Value.ToString();
                txtDOB.Text = row.Cells[3].Value.ToString();
                txtGender.Text = row.Cells[4].Value.ToString();
                txtPhone.Text = row.Cells[5].Value.ToString();
                if (row.Cells[6].Value.ToString() != "")
                {
                    LoadImage(int.Parse(txtStudentID.Text));
                }else
                {
                    picbxStudent.Image = picbxStudent.InitialImage;
                }

                
                txtStreet.Text = row.Cells[7].Value.ToString();
                txtCity.Text = row.Cells[8].Value.ToString();
                txtState.Text = row.Cells[9].Value.ToString();
                txtCountry.Text = row.Cells[10].Value.ToString();
                txtPostalCode.Text = row.Cells[11].Value.ToString();

                txtModules.Text = row.Cells[12].Value.ToString();
            }
        }

        private void StudentData_Load(object sender, EventArgs e)
        {

            dgvStudents.DataSource =  handler.getStudents();
            foreach(DataGridViewColumn column in dgvStudents.Columns)
            {
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            DataGridViewImageColumn imageCol = (DataGridViewImageColumn)dgvStudents.Columns[6];
            imageCol.ImageLayout = DataGridViewImageCellLayout.Zoom;
                
            

        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            handler.UpdateStudentsData(int.Parse(txtStudentID.Text), txtName.Text, txtSurname.Text, DateTime.Parse(txtDOB.Text), txtGender.Text, txtPhone.Text, int.Parse(txtModules.Text));
            handler.UpdateStudentAddress(int.Parse(txtStudentID.Text),txtStreet.Text, txtCity.Text, txtState.Text, txtCountry.Text, txtPostalCode.Text);
            if (imagePath != "")
            {
                handler.UpdateImage(imagePath, int.Parse(txtStudentID.Text));
            }
            dgvStudents.DataSource = handler.getStudents();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            handler.DeleteStudent(int.Parse(txtStudentID.Text));
            dgvStudents.DataSource = handler.getStudents();
            txtDOB.Clear();
            txtGender.Clear();
            txtName.Clear();
            txtPhone.Clear();
            txtStudentID.Clear();
            txtSurname.Clear();
            txtCity.Clear();
            txtCountry.Clear();
            txtState.Clear();
            txtStreet.Clear();
            txtModules.Clear();
            txtPostalCode.Clear();
            picbxStudent.Image = picbxStudent.InitialImage;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtDOB.Clear();
            txtGender.Clear();
            txtName.Clear();
            txtPhone.Clear();
            txtStudentID.Clear();
            txtSurname.Clear();
            txtCity.Clear();
            txtCountry.Clear();
            txtState.Clear();
            txtStreet.Clear();
            txtModules.Clear();
            txtPostalCode.Clear();
            picbxStudent.Image = picbxStudent.InitialImage;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {



        }

        private void picBoxSearch_Click(object sender, EventArgs e)
        {
            // Call the SearchStudent method and get the result
            DataTable dt = handler.SearchStudent(int.Parse(txtStudentID.Text));

            // Check if any rows were returned
            if (dt.Rows.Count > 0)
            {
                // Get the first row
                DataRow row = dt.Rows[0];

                // Set the text boxes with the values from the row
                txtStudentID.Text = row["StudentID"].ToString();
                txtName.Text = row["FirstName"].ToString();
                txtSurname.Text = row["Surname"].ToString();
                txtDOB.Text = row["DOB"].ToString();
                txtGender.Text = row["Gender"].ToString();
                txtPhone.Text = row["Phone"].ToString();
                txtStreet.Text = row["Street"].ToString();
                txtState.Text = row["State"].ToString();
                txtCity.Text = row["City"].ToString();
                txtCountry.Text = row["Country"].ToString();
                txtPostalCode.Text = row["PostalCode"].ToString();
                txtModules.Text = row["CourseCode"].ToString();
                if (row["StudentImage"].ToString() != "")
                {
                    byte[] imageData = handler.GetImageData(int.Parse(txtStudentID.Text));


                    // Convert the image data into a MemoryStream
                    using (MemoryStream ms = new MemoryStream(imageData))
                    {
                        // Load the image from the MemoryStream into the PictureBox
                        picbxStudent.Image = Image.FromStream(ms);
                    }
                }
                else
                {
                    picbxStudent.Image = picbxStudent.InitialImage;
                }
            }


        }
    }
}
