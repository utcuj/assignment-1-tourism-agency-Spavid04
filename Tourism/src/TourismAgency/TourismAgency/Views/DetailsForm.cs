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
    public partial class DetailsForm : Form
    {
        public DetailsForm(string text)
        {
            InitializeComponent();

            textBox1.Text = text;

            this.Closing += DetailsForm_Closing;
        }

        private void DetailsForm_Closing(object sender, CancelEventArgs e)
        {
            this.Tag = textBox1.Text;
        }
    }
}
