﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace geometry
{
    public partial class mainForm : Form
    {
        //xd
        protected void mainFunction()
        {
            if (textBox1.Text == "")
            {

            }
            else
            {
                    string textBoxInput = textBox1.Text;
                    ParsingAndCalculation parserObject = new ParsingAndCalculation(textBoxInput);
                    string result = parserObject.matchDecide();
                    result = result.Replace("b", "/");
                    MessageBox.Show(result.ToString());
            }
        }

        public mainForm()
        {
            InitializeComponent();
            KeyPreview = true;
            this.Activate();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }


        private void button1_Click(object sender, EventArgs e)
        {
            mainFunction();
        }

        private void mainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                mainFunction();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                Application.Exit();
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            // + - / cos sin * =
            //need to add if I missed
            char T = e.KeyChar;

            if (Char.IsDigit(T) || T == '+' || T == '-' || T == '*' || T == '/' || T == '=' || T == 'c' || T == 'o' || T == 's' || T == 'i' || T == 'n' || T == 'p' || T == 'i'|| T == ',' || e.KeyChar == '\b' || e.KeyChar == Convert.ToChar(",") || T == '^' || T == '(' || T == ')')
            {
               
            }
            else
            {
                e.Handled = true;              
            }

        }
    }
}
