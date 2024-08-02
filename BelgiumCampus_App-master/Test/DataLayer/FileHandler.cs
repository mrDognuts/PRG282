﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Test.BusinessLogicLayer; //Imported layer to use 'Login' class
using System.Windows.Forms;

namespace Test.DataLayer
{
    internal class FileHandler
    {
        string filePath = @"LoginData.txt";

        //Create a list of Login class to compare textfile with verified login info with entered Username & Password on Login Form
        public List<Login> GetLogins()
        {
            List<Login> loginList = new List<Login>();
            string[] lines = File.ReadAllLines(filePath);


            foreach (string line in lines)
            {
                string[] items = line.Split(',');
                if (items.Length == 2)
                {
                    foreach (string item in items)
                    {
                        loginList.Add(new Login(items[0], items[1]));
                    }
                }
            }

            return loginList;
        }

        public void Register(string username, string password)
        {
            string filePath = @"LoginData.txt";

            try
            {
                if (File.Exists(filePath))
                {
                    using (StreamWriter writer = new StreamWriter(filePath, true)) // Pass true to append to the file
                    {
                        writer.WriteLine(username + "," + password);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show("Textfile does not exist");
            }
        }

    }
}
