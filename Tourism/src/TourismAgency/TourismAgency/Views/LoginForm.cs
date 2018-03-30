using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TourismAgency.Controllers;
using TourismAgency.Models;

namespace TourismAgency.Views
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UserLevel level;
            int id;
            if ((id = LoginController.TryLogin(textBox1.Text, textBox2.Text, out level)) < 0)
            {
                MessageBox.Show("Wrong credentials!");
            }
            else
            {
                this.Visible = false;
                LoginController.Login(level, id);
            }
        }
    }
}
