using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Daryl___Hangman
{
    public partial class Formloading : Form
    {
        public Timer timer = new Timer();
        public Formloading()
        {
            InitializeComponent();
            InitializeTimer();
        }

        public void InitializeTimer()
        {
            timer.Enabled = true;
            timer.Interval = 12300;
            timer.Tick += Timer_Tick;
            timer.Start();

        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();
            ShowNewForm();
        }

        private void ShowNewForm()
        {
            Form1 newForm = new Form1();
            this.Hide();
            newForm.Show();

        }
    }
}
