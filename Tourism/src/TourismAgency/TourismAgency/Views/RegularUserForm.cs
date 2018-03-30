using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using TourismAgency.Controllers;
using TourismAgency.Models;

namespace TourismAgency.Views
{
    public partial class RegularUserForm : Form
    {
        private int userId;

        private int lastClientId;
        private int lastHolidayId;

        public RegularUserForm(int userId)
        {
            this.userId = userId;

            InitializeComponent();

            lastClientId = -1;
            lastHolidayId = -1;
            RefreshTab1Fields();

            textBox1_TextChanged(null, null);
        }

        #region Tab1

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            foreach (var client in RegularUserController.SearchClients(textBox1.Text))
            {
                listBox1.Items.Add(client);
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1 && listBox1.Items.Count > 0)
            {
                lastClientId = ((Client)listBox1.Items[listBox1.SelectedIndex]).Id;

                RefreshTab1Fields();
            }
        }

        private void RefreshTab1Fields()
        {
            var u = RegularUserController.GetClient(lastClientId);

            textBox2.Text = u.Id.ToString();

            textBox3.Text = u.Name;
            textBox4.Text = u.ICN;
            textBox5.Text = u.PNC;
            textBox6.Text = u.Address;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!Regex.IsMatch(textBox4.Text, @"^[a-zA-Z]{2}\d{6}$"))
            {
                MessageBox.Show("PNC must be 2 letters followed by 6 digits");
                return;
            }
            if (!Regex.IsMatch(textBox5.Text, @"^\d{8,12}$"))
            {
                MessageBox.Show("PNC must have 8-12 digits!");
                return;
            }

            lastClientId =
                RegularUserController.SaveClient(lastClientId, textBox3.Text, textBox4.Text, textBox5.Text, textBox6.Text);

            RefreshTab1Fields();

            textBox1_TextChanged(null, null);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            lastClientId = -1;
            RefreshTab1Fields();
        }

        #endregion

        #region Tab2

        private void RefreshTab2Fields()
        {
            listBox2.Items.Clear();
            foreach (var reservation in RegularUserController.GetReservationsFor(lastClientId))
            {
                listBox2.Items.Add(reservation);
            }

            var r = RegularUserController.GetReservation(lastClientId);

            if (r == null)
                return;

            textBox7.Text = r.Id.ToString();
            
            textBox8.Text = r.Destination;
            textBox9.Text = r.HotelName;
            numericUpDown1.Value = r.PersonCount;
            label13.Text = r.Details;
            numericUpDown2.Value = r.TotalPrice;
            numericUpDown3.Value = r.PaidAmount;
            dateTimePicker1.Value = r.FinalPaymentDate;
            dateTimePicker2.Value = r.ReservationDate;
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox2.SelectedIndex != -1 && listBox2.Items.Count > 0)
            {
                lastHolidayId = ((Reservation)listBox2.Items[listBox2.SelectedIndex]).Id;

                RefreshTab2Fields();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dateTimePicker1.Value < DateTime.Now)
            {
                MessageBox.Show("No further changes are allowed to this reservation.");
                return;
            }

            if (dateTimePicker2.Value < DateTime.Now)
            {
                MessageBox.Show("Cannot set reservation day in the past.");
                return;
            }

            lastHolidayId =
                RegularUserController.SaveReservation(lastHolidayId, lastClientId, userId, textBox8.Text, textBox9.Text, (int)numericUpDown1.Value, label13.Text, (int)numericUpDown2.Value, (int)numericUpDown3.Value, dateTimePicker1.Value, dateTimePicker2.Value);

            RefreshTab2Fields();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dateTimePicker1.Value < DateTime.Now)
            {
                MessageBox.Show("No further changes are allowed to this reservation.");
                return;
            }

            RegularUserController.DeleteReservation(lastHolidayId);

            lastHolidayId = -1;
            RefreshTab2Fields();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            lastHolidayId = -1;
            RefreshTab2Fields();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DetailsForm details = new DetailsForm(label13.Text);

            details.ShowDialog();

            label13.Text = details.Tag.ToString();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            PaymentForm payment = new PaymentForm((int) numericUpDown2.Value, (int) numericUpDown3.Value);

            payment.ShowDialog();

            int pay = (int) payment.Tag;
            this.numericUpDown3.Value += pay;
        }

        #endregion

        #region Tab3

        private void RefreshTab3Fields()
        {
            listBox3.Items.Clear();
            foreach (var reservation in RegularUserController.GetAllMissedReservations())
            {
                listBox3.Items.Add(reservation);
            }
        }

        #endregion

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 1)
            {
                if (lastClientId == -1)
                {
                    MessageBox.Show("Select a client in the information tab.");
                    tabControl1.SelectedIndex = 0;
                }
                else
                {
                    lastHolidayId = -1;
                    RefreshTab2Fields();
                }
            }
            else if (tabControl1.SelectedIndex == 2)
            {
                RefreshTab3Fields();
            }
        }
    }
}
