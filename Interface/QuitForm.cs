using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Interface
{
    public class QuitForm : Form
    {
        private Button m_ClearScreen;
        private Button m_Exit;
        private bool m_WantsToRestart = false;
        

        public QuitForm()
        {
            this.Text = "Surrender:";
            //this.BackColor = Color.White;
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            this.Size = new Size(270, 100);
            this.StartPosition = FormStartPosition.CenterScreen;
            SetButtons();
        }

        public bool WantsToRestart
        {
            get
            {
                return m_WantsToRestart;
            }
        }
        private void SetButtons()
        {
            SetClearScreenButton();
            SetExitButton();
        }

        private void SetClearScreenButton()
        {
            m_ClearScreen = new Button();
            m_ClearScreen.BackColor = Color.LightSeaGreen;
            m_ClearScreen.Enabled = true;
            m_ClearScreen.Text = "Clear Screen";
            m_ClearScreen.Top = 20;
            m_ClearScreen.Left = 20;
            m_ClearScreen.Size = new Size(85, 30);
            m_ClearScreen.Click += new EventHandler(ButtonClearScreen_Click);
            this.Controls.Add(m_ClearScreen);

        }

        private void SetExitButton()
        {
            m_Exit = new Button();
            m_Exit.BackColor = Color.OrangeRed;
            m_Exit.Enabled = true;
            m_Exit.Text = "Exit";
            m_Exit.Top = 20;
            m_Exit.Left = 150;
            m_Exit.Size = new Size(70, 30);
            m_Exit.Click += new EventHandler(ButtonExit_Click);
            this.Controls.Add(m_Exit);
        }

        private void ButtonClearScreen_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            m_WantsToRestart = true;
            this.Close();
        }

        private void ButtonExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            m_WantsToRestart = false;
            this.Close();
        }
    }
}
