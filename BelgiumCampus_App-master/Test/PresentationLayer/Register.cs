﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Test.BusinessLogicLayer;
using Test.DataLayer;

namespace Test
{
    public partial class Register : Form
    {
        public delegate void EnableButtonsDelegate(object sender, EventArgs e);
        public event EnableButtonsDelegate EnableButtons;

        public Register()
        {
            InitializeComponent();
        }

        private void lblLogin_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            //Fetching info from textboxes
            string username = txtUsername_Register.Text;
            string password = txtPassword_Register.Text;

            //Logic Class Object
            Logic logic = new Logic();
            //DataHandler Class Object
            FileHandler handler = new FileHandler();

            //Validating password
            if (logic.ValidatePassword(password))
            {
                handler.Register(username, password);
                MessageBox.Show(Form.ActiveForm, "Registration Complete. \nPlease Login now with your credentials.");
                
                RegistrationComplete registrationComplete = new RegistrationComplete();
                registrationComplete.Show();
                
                this.Close();
                
            }
            else
            {
                MessageBox.Show("Registration Unsuccessful. \nPassword must contain at least one Uppercase letter and one Number");
                txtPassword_Register.Clear();
            }
        }   
    }
}
