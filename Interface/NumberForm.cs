using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Interface
{
    public class NumberForm : Form
    {
        //// Data members:
        private int m_ChosenNumber;

        private Button[] m_ButtonsGuessesPins;

        //// Properties:
        public int ChosenNumber
        {
            get
            {
                return m_ChosenNumber;
            }
        }

        //// C'tor:
        public NumberForm()
        {
            //// Set the "Choose Color" form's details.
            this.Text = "Pick A Number:";
            this.BackColor = Color.LightSeaGreen;
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            this.Size = new Size(200, 220);
            this.StartPosition = FormStartPosition.CenterScreen;
            m_ButtonsGuessesPins = new Button[9];
            SetNumbers();
        }

        //// Methods
        private void SetNumbers()
        {
            //// This method sets the form's colored buttons.
            for (int currentButton = 0; currentButton < 9; currentButton++)
            {
                //// Set the current button size, and add it:
                m_ButtonsGuessesPins[currentButton] = new Button();
                m_ButtonsGuessesPins[currentButton].Size = new Size(50, 50);
                m_ButtonsGuessesPins[currentButton].BackColor = Color.LightGray;
                m_ButtonsGuessesPins[currentButton].Text = (currentButton + 1).ToString();
                this.Controls.Add(m_ButtonsGuessesPins[currentButton]);

                //// Set the current button position on the form:
                if (currentButton == 0)
                {
                    //// Incase the current button is in the first row:
                    m_ButtonsGuessesPins[currentButton].Top = 8;
                    m_ButtonsGuessesPins[currentButton].Left = 8;
                }
                else if (currentButton == 3)
                {
                    //// Incase the current button is the first one in the second row:
                    m_ButtonsGuessesPins[currentButton].Top = m_ButtonsGuessesPins[0].Bottom + 8;
                    m_ButtonsGuessesPins[currentButton].Left = m_ButtonsGuessesPins[0].Left;
                }
                else if (currentButton == 6)
                {
                    m_ButtonsGuessesPins[currentButton].Top = m_ButtonsGuessesPins[3].Bottom + 8;
                    m_ButtonsGuessesPins[currentButton].Left = m_ButtonsGuessesPins[3].Left;
                }
                else
                {
                    //// Incase the current button is one of the left buttons, in the second row:
                    m_ButtonsGuessesPins[currentButton].Top = m_ButtonsGuessesPins[currentButton - 1].Top;
                    m_ButtonsGuessesPins[currentButton].Left = m_ButtonsGuessesPins[currentButton - 1].Right + 8;
                }

                //// Delegate the current button's event:
                m_ButtonsGuessesPins[currentButton].Click += new EventHandler(ButtonsGuessesPins_Click);
            }
        }

        private void ButtonsGuessesPins_Click(object sender, EventArgs e)
        {
            //// This method is the event activated when a color button is pressed.
            //// Update the chosen color data member of the "Choose color" form, and complete the dialog:
            m_ChosenNumber = int.Parse((sender as Button).Text);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // NumberForm
            // 
            this.ClientSize = new System.Drawing.Size(636, 490);
            this.Name = "NumberForm";
            this.ResumeLayout(false);

        }
    }
}