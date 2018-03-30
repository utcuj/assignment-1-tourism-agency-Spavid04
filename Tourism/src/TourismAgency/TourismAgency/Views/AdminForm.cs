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
    public partial class AdminForm : Form
    {
        private int lastUserId;

        public AdminForm()
        {
            InitializeComponent();

            lastUserId = -1;
            RefreshFields();

            textBox1_TextChanged(null, null);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            foreach (var user in AdminController.Search(textBox1.Text))
            {
                listBox1.Items.Add(user);
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1 && listBox1.Items.Count > 0)
            {
                lastUserId = ((User)listBox1.Items[listBox1.SelectedIndex]).Id;

                RefreshFields();
            }
        }

        private void RefreshFields()
        {
            var u = AdminController.GetUser(lastUserId);

            textBox2.Text = u.Id.ToString();

            textBox3.Text = u.Username;
            textBox4.Text = u.Password;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            lastUserId =
                AdminController.Save(lastUserId, textBox3.Text, textBox4.Text);

            RefreshFields();

            textBox1_TextChanged(null, null);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AdminController.Delete(lastUserId);

            lastUserId = -1;
            RefreshFields();

            textBox1_TextChanged(null, null);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            lastUserId = -1;
            RefreshFields();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dateTimePicker1.Value >= dateTimePicker2.Value)
            {
                return;
            }

            AdminController.GenerateReport(lastUserId, dateTimePicker1.Value, dateTimePicker2.Value);
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 1 && lastUserId == -1)
            {
                MessageBox.Show("Select an agent in the information tab.");
                tabControl1.SelectedIndex = 0;
            }
        }
    }
}
