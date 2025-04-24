using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator_With_Decent_UI
{
    public partial class Form1: Form
    {
        // public variables for numbers and operators!
        public int one, two, three, four, five, six, seven, eight, nine, zero;
        // boolean for delete button things
        bool is_pressed = false;
        // variables for minus and etc...
        public string minus, plus, divide, multiplym, comma;

        public Form1()
        {
            InitializeComponent();
        }
        #region cool stuff
        private void button1_Click(object sender, EventArgs e)
        {
            // error handling
            try
            {   // here we use datatables from System.data to do the funny stuff
                DataTable dt = new DataTable();
                // just counting..
                // stupid but needed because cant use , sadly but we ball tho its what we do :sunglasses:
                var answer = txtCounting.Text.Replace(",", ".");
                var finalAnswer = dt.Compute(answer, null);
                // printing / showing the answer on txtcounting...
                txtCounting.Text = finalAnswer.ToString();

                // making this so clearing and deleting will be whole lot of not so painful
                is_pressed = true;

                /*
                 havent yet included all buttons because im lazy but now when i start doing it let me leave this message here for future me hopefully u create better code than this in future :D:D:D
                 even tho this aint my worst but its not the best either 
                 */
                
            }
            catch(Exception ex) // here we catch the error
            {// shows the error message as Syntax error: Something
                MessageBox.Show("Syntax error: " + ex.Message, "Syntax Error");
            }


        }
        // making this to clear some mess for me
        #endregion cool stuff 

        #region buttons
        private void btnOne_Click(object sender, EventArgs e)
        {// this is exactly how it looks like 
            // just gonna copy and paste this to all those buttons cuz cba to write invidually and its just not smart?
            one = 1;

            txtCounting.Text += one.ToString();

        }

        private void btnSix_Click(object sender, EventArgs e)
        {
            six = 6;

            txtCounting.Text += six.ToString();

        }

        private void btnSwitch_Click(object sender, EventArgs e)
        {   // we check if theres a number to switch to negative / positive
            if (string.IsNullOrEmpty(txtCounting.Text))
                return;
            // checking if the thing is negative or positive and if its not we add the minus if it is we take it off.
            if (txtCounting.Text.StartsWith("-"))
                txtCounting.Text = txtCounting.Text.Substring(1);
            else // simple logic of adding the - idk why i even comment here
                txtCounting.Text = "-" + txtCounting.Text;
        }

        private void btnSeven_Click(object sender, EventArgs e)
        {
            seven = 7;

            txtCounting.Text += seven.ToString();

        }

        private void btnEight_Click(object sender, EventArgs e)
        {
            eight = 8;

            txtCounting.Text += eight.ToString();

        }

        private void btnNine_Click(object sender, EventArgs e)
        {
            nine = 9;

            txtCounting.Text += nine.ToString();

        }

        private void btnFive_Click(object sender, EventArgs e)
        {
            five = 5;

            txtCounting.Text += five.ToString();

        }

        private void btnFour_Click(object sender, EventArgs e)
        {
            four = 4;

            txtCounting.Text += four.ToString();

        }

        private void btnThree_Click(object sender, EventArgs e)
        {
            three = 3;

            txtCounting.Text += three.ToString();

        }

        private void btnTwo_Click(object sender, EventArgs e)
        {
            two = 2;

            txtCounting.Text += two.ToString();

        }

        private void btnDivide_Click(object sender, EventArgs e)
        {
            minus = "/";
            txtCounting.Text += minus;
        }

        private void btnMultiply_Click(object sender, EventArgs e)
        {
            minus = "*";
            txtCounting.Text += minus;
        }

        private void btnPlus_Click(object sender, EventArgs e)
        {
            minus = "+";
            txtCounting.Text += minus;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (is_pressed)
            {
                // lets set the value back to false so it will reset
                is_pressed = false;
                txtCounting.Clear();
            }
            else if (!is_pressed) // if condition is not met we go here --
            {   // we check if the condition of lenght being over 0 is met if so we can delete letters one at a time :)
                if (txtCounting.Text.Length > 0)
                    txtCounting.Text = txtCounting.Text.Substring(0, txtCounting.Text.Length - 1);
            }
        }

        private void btnComma_Click(object sender, EventArgs e)
        {
            comma = ",";
            txtCounting.Text += comma;
        }

        // button code for minus and others wont be commenting these much more they dont contain anything special
        private void bntMinus_Click(object sender, EventArgs e)
        {
            minus = "-";
            txtCounting.Text += minus;
        }
        #endregion buttons
    }
}
