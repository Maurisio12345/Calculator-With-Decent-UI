using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Calculator_With_Decent_UI
{
    public partial class Form1: Form
    {
        // public variables for numbers and operators!
        public int one, two, three, four, five, six, seven, eight, nine, zero;
        // boolean for delete button things
        bool is_pressed = false;
        // variables for minus and etc...
        public string minus, plus, divide, multiply, comma;

        int checkervalue123 = 0;

        int height, width, side, answer, filter;

        double radius, answerous;

        // now we start here the currency changer first we make an array of currencies

        string[] currencies = { "EUR", "USD", "GBP", "JPY", "CHF", "AUD", "CAD", "SEK", "NOK", "DKK", "PLN", "MXN", "CZK", "HUF", "TRY", "ZAR", "HKD", "SGD", "BRL", "INR", "KRW", "CNY" };
        // here we do an array of shapes we want to use in our calculator..
        string[] shapes = { "Square", "Rectangle", "Triangle", "Circle"};
        //  adding later
        //  "Cube", "Cuboid", "Sphere", "Cylinder", "Cone", "Pyramid" 
        public Form1()
        {
            InitializeComponent();
            comboChanger.Items.AddRange(currencies);
            comboChangeTo.Items.AddRange(currencies);
            comboChanger.SelectedIndex = 0;
            comboChangeTo.SelectedIndex = 0;
            comboShape.Items.AddRange(shapes);
            comboShape.SelectedIndex = -1;

            Thread countingThread = new Thread(ThreadedCountingShapes);
            countingThread.Start();

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

        private async void btnChange_Click(object sender, EventArgs e)
        {
            try
            {
                // we check if the number is bigger than 0 if yes we dont stay here if its not we return
                if (!decimal.TryParse(txtChanger.Text, out decimal amount) || amount <= 0)
                    return;

                string selectedCurrency = comboChanger.SelectedItem.ToString();
                string selectedChange = comboChangeTo.SelectedItem.ToString();

                decimal convertedAmount = await ConvertCurrency(amount, selectedCurrency, selectedChange);
                // here we show case our new currency
                txtChanged.Text = convertedAmount.ToString("0.00");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        static async Task<decimal> ConvertCurrency(decimal amount, string from, string to)
        {
         
            // here we get our currency data from api frankfurter.app
            using (HttpClient client = new HttpClient())
            {
                string url = $"https://api.frankfurter.app/latest?amount={amount}&from={from}&to={to}";
                var response = await client.GetStringAsync(url);

                JObject data = JObject.Parse(response);
                decimal result = (decimal)data["rates"][to];

                return result;

            }
        }

        private void btnShape_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboShape.SelectedIndex == 0)
                {
                    ThreadedCountingShapes();
                }
                if (comboShape.SelectedIndex == 1)
                {
                    ThreadedCountingShapes();
                }
                if (comboShape.SelectedIndex == 2)
                {
                    ThreadedCountingShapes();
                }
                if (comboShape.SelectedIndex == 3)
                {
                    ThreadedCountingShapes();
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        public void ThreadedCountingShapes()
        {// wanted to experiment threading so this feature will be seperate thread now...
           

            try // catch an error but we aint here playin around bro
            {
                side = int.Parse(txtAnswer.Text);
                if (comboShape.SelectedIndex == 0)
                {// square //
                    // counting the area size by multiply
                    answer = side * side;
                    // here we show our answer in txtbox
                    txtAnswer.Text = answer.ToString();
                }
                else if (comboShape.SelectedIndex == 1)
                {// rectangle //
                    // goes height x width

                    if (int.TryParse(txtAnswer.Text, out int number))
                    {
                        if (checkervalue123 == 0)
                        {
                            // here we save the value of the number to the variable Height 
                            // also we add 1 to checkervalue to give the greenlight to switch to the width inserting and counting finaly the answer
                            height = number;
                            checkervalue123 = 1;
                            txtAnswer.Clear();
                            MessageBox.Show("Height saved now insert a width!");

                        }
                        else if (checkervalue123 == 1)
                        {// we execute this part when the checker value is 1
                            // here we do the same thing we did with height
                            // and we remove one from checkervalue to count new stuff
                            // we also print the stuff to the answer box :)))
                            width = number;
                            answer = height * width;
                            checkervalue123 = 0;
                            txtAnswer.Text = answer.ToString();
                        }
                    }




                }
                else if (comboShape.SelectedIndex == 2)
                {
                    if (int.TryParse(txtAnswer.Text, out int number))
                    {//triangle

                        if (checkervalue123 == 0)
                        {
                            // here we do the get the values in the number integer
                            width = number;
                            checkervalue123 = 1;
                            txtAnswer.Clear();
                            MessageBox.Show("Width saved now insert the Height!");
                        }
                        else if (checkervalue123 == 1)
                        {// here we do the actual counting but first we need to get the last value / height
                            // then we count so height x width divided by 2 is the answer
                            // the we print it to the txtAnswer
                            // simple and good :)))
                            height = number;
                            checkervalue123 = 0;
                            filter = width * height;
                            answer = filter / 2;
                            txtAnswer.Text = answer.ToString();
                        }
                    }
                }
                else if (comboShape.SelectedIndex == 3) // circle
                {
                    if (double.TryParse(txtAnswer.Text, out double number))
                    {// circle // u count it by radius x radius x PI
                        radius = number;
                        // writing a first step of counting the area size
                        answerous = radius * radius;
                        // here we count the rest of the area size
                        double answer = answerous * Math.PI;
                        // here we again show the answer
                        txtAnswer.Text = answer.ToString();
                    }
                }


            }
            catch (Exception ex)
            {
                //nothing here 
                return;
            }

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
            if (is_pressed) // checker value
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
        // take a wild guess
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
