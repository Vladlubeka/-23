using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace MemoryGame
{
    public partial class Form1 : Form
    {
        private Button firstClicked = null;
        private Button secondClicked = null;
        private Random random = new Random();
        private List<string> icons = new List<string>()
        {
            "!", "!", "N", "N", ",", ",", "k", "k",
            "b", "b", "v", "v", "w", "w", "z", "z"
        };

        public Form1()
        {
            InitializeComponent();
            AssignIconsToSquares();
        }

        private void AssignIconsToSquares()
        {
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Button button = control as Button;
                if (button != null)
                {
                    int randomNumber = random.Next(icons.Count);
                    button.Tag = icons[randomNumber];
                    button.ForeColor = button.BackColor;
                    icons.RemoveAt(randomNumber);
                }
            }
        }

        private void button_Click(object sender, EventArgs e)
        {
            if (firstClicked != null && secondClicked != null)
                return;

            Button clickedButton = sender as Button;

            if (clickedButton == null)
                return;

            if (clickedButton.ForeColor == Color.Black)
                return;

            if (firstClicked == null)
            {
                firstClicked = clickedButton;
                firstClicked.ForeColor = Color.Black;
                return;
            }

            secondClicked = clickedButton;
            secondClicked.ForeColor = Color.Black;

            CheckForWinner();

            if (firstClicked.Tag.ToString() == secondClicked.Tag.ToString())
            {
                firstClicked = null;
                secondClicked = null;
                return;
            }

            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();

            firstClicked.ForeColor = firstClicked.BackColor;
            secondClicked.ForeColor = secondClicked.BackColor;

            firstClicked = null;
            secondClicked = null;
        }

        private void CheckForWinner()
        {
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Button button = control as Button;

                if (button != null && button.ForeColor == button.BackColor)
                    return;
            }

            MessageBox.Show("Вы нашли все пары!", "Поздравляю");
            Close();
        }
    }
}