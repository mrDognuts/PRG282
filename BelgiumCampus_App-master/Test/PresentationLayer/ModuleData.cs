using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Test.BusinessLogicLayer;
using Test.DataLayer;

namespace Test
{
    public partial class ModuleData : Form
    {
        DataHandler handler = new DataHandler();
        public ModuleData()
        {
            InitializeComponent();
        }

        private void btnBackModule_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnModuleCreate_Click(object sender, EventArgs e)
        {
            ModuleAdded moduleAdded = new ModuleAdded();
            moduleAdded.Show();
            Course course = new Course(txtModuleName.Text, rtxModuledesc.Text, rtxModuleLinks.Text, int.Parse(txtModuleCode.Text));
            handler.AddModule(course);
            dgvModules.DataSource = handler.getModules();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtModuleCode.Clear();
            txtModuleName.Clear();
            rtxModuledesc.Clear();
            rtxModuleLinks.Clear();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            handler.UpdateModulesData(int.Parse(txtModuleCode.Text), txtModuleName.Text, rtxModuledesc.Text, rtxModuleLinks.Text);
            dgvModules.DataSource = handler.getModules();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            handler.DeleteModule(int.Parse(txtModuleCode.Text));
            dgvModules.DataSource = handler.getModules();
            txtModuleCode.Clear();
            txtModuleName.Clear();
            rtxModuledesc.Clear();
            rtxModuleLinks.Clear();
        }

        private void picBoxSearch_Click(object sender, EventArgs e)
        {
            DataTable dt = handler.SearchModule(int.Parse(txtModuleCode.Text));

            // Check if any rows were returned
            if (dt.Rows.Count > 0)
            {
                // Get the first row
                DataRow row = dt.Rows[0];

                // Set the text boxes with the values from the row
                txtModuleCode.Text = row["CourseCode"].ToString();
                txtModuleName.Text = row["ModuleName"].ToString();
                rtxModuledesc.Text = row["ModuleDescription"].ToString();
                rtxModuleLinks.Text = row["Links"].ToString();
            }
        }

        private void ModuleData_Load(object sender, EventArgs e)
        {
            dgvModules.DataSource = handler.getModules();
            foreach(DataGridViewColumn column in dgvModules.Columns)
            {
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }

        private void dgvModules_SelectionChanged(object sender, EventArgs e)
        {
            foreach(DataGridViewRow row in dgvModules.SelectedRows)
            {
                txtModuleCode.Text = row.Cells[0].Value.ToString();
                txtModuleName.Text = row.Cells[1].Value.ToString();
                rtxModuledesc.Text = row.Cells[2].Value.ToString();
                rtxModuleLinks.Text = row.Cells[3].Value.ToString();
            }
        }
    }
}
