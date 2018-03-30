using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TourismAgency.Views
{
    public partial class PaymentForm : Form
    {
        public PaymentForm(int total, int paid)
        {
            InitializeComponent();

            this.numericUpDown1.Value = total;
            this.numericUpDown2.Value = total - paid;
            this.numericUpDown3.Maximum = total - paid;

            this.Closing += PaymentForm_Closing;
        }

        private void PaymentForm_Closing(object sender, CancelEventArgs e)
        {
            this.Tag = (int)numericUpDown3.Value;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            this.numericUpDown2.Value = this.numericUpDown1.Value - this.numericUpDown3.Value;
        }
    }
}
