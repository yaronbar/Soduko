using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Logic;

namespace Interface
{
    public class FormGame : Form
    {
        //// Data members:
        private ButtonWithCoordinate[,] m_ButtonsGuessesPins;
        private Button m_ButtonSurrender;
        private NumberForm m_NumberForm;
        private QuitForm m_QuitForm;
        private Logic.Data m_Data;

        private bool m_CurrentButtonLegalLine;
        private bool m_CurrentButtonLegalRow;
        private Button[] m_ButtonBoundaries;
        private bool[] m_LegalLines;
        private bool[] m_LegalRows;
        private bool[] m_LegalBoxes;
        //// C'tor:
        public FormGame()
        {
            this.Text = "Soduko";
            this.BackColor = Color.LightGray;
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            this.StartPosition = FormStartPosition.CenterScreen;
       
        }

        //// Methods:
        protected override void OnLoad(EventArgs e)
        {
            ///// This method is overrides the base.Onload method of the form in order to add more details.
            //// Decleration of variables: 
            this.Size = new Size(670, 570);
            SetFormGameButtons();
            m_NumberForm = new NumberForm();
            m_QuitForm = new QuitForm();
                     
        }


        private void SetFormGameButtons()
        {
            //// This method sets the FormGame buttons.
            //// Creat all the buttons array:

            m_ButtonsGuessesPins = new ButtonWithCoordinate[9, 9];
            m_ButtonSurrender = new Button();
            m_ButtonBoundaries = new Button[4];
            m_LegalLines = new bool[9];
            m_LegalRows = new bool[9];
            m_LegalBoxes = new bool[9];
            m_Data = new Logic.Data();

            //// Set each array:
            SetBoolians();
            SetBuckets();
            SetGuessesPinsButtons();
            SetSurrenderButton();
            SetBoldBoundaries();
            Random rnd = new Random();
            SetStartingButtons(rnd.Next(1, 6));
        }

        private void SetBoolians()
        {
            for (int index = 0; index < 9; index++)
            {
                m_LegalBoxes[index] = true;
                m_LegalLines[index] = true;
                m_LegalRows[index] = true;
            }
        }

        private void SetBuckets()
        {
            for (int index = 0; index < 9; index++)
            {
                for (int index1 = 0; index1< 9; index1++)
                {
                    m_Data.m_BucketBoxes[index, index1] = 0;
                    m_Data.m_BucketLines[index, index1] = 0;
                    m_Data.m_BucketRows[index, index1] = 0;
                }
            }
        }

        private void SetGuessesPinsButtons()
        {
            //// This method sets the four guess pins buttons in each line, according to the number of chances chosen by the user.
            for (int line = 0; line < 9; line++)
            {
                for (int row = 0; row < 9; row++)
                {
                    m_ButtonsGuessesPins[row, line] = new ButtonWithCoordinate();
                    m_ButtonsGuessesPins[row, line].Coordination = new Point();
                    m_ButtonsGuessesPins[row, line].Enabled = true; // each button can be chosen to get a number
                    m_ButtonsGuessesPins[row, line].Coordination.Line = line;
                    m_ButtonsGuessesPins[row, line].Coordination.Row = row;
                    m_ButtonsGuessesPins[row, line].ForeColor = Color.LightGray;
                    m_ButtonsGuessesPins[row, line].Text = "-1";
                    m_ButtonsGuessesPins[row, line].PreviousNumber = -1;
                    m_ButtonsGuessesPins[row, line].Starter = false;


                    //// Set the current button's details and add it to the form:
                    m_ButtonsGuessesPins[row, line].BackColor = Color.LightGray;
                    //// Set the button's event:
                    m_ButtonsGuessesPins[row, line].Click += new EventHandler(ButtonGuess_Click);
                    m_ButtonsGuessesPins[row, line].Size = new Size(50, 50);
                    if (line == 0)
                    {
                        m_ButtonsGuessesPins[row, line].Top = 8;
                    }
                    else
                    {
                        m_ButtonsGuessesPins[row, line].Top = m_ButtonsGuessesPins[row, line - 1].Bottom + 8;
                    }
                    if (row == 0)
                    {
                        m_ButtonsGuessesPins[row, line].Left = 8;
                    }
                    else
                    {
                        m_ButtonsGuessesPins[row, line].Left = m_ButtonsGuessesPins[row - 1, line].Right + 8;
                    }
                    this.Controls.Add(m_ButtonsGuessesPins[row, line]);
                }
            }

        }

        private void SetStartingButtons(int i_StartingSettings)
        {
            if (i_StartingSettings == 1)
            {
                m_ButtonsGuessesPins[2, 7].Text = "4";
                m_ButtonsGuessesPins[2, 7].Enabled = false;
                m_ButtonsGuessesPins[2, 7].BackColor = Color.LightSteelBlue;
                m_ButtonsGuessesPins[2, 7].ForeColor = Color.Transparent;
                m_ButtonsGuessesPins[2, 7].Starter = true;
                m_Data.m_BucketLines[3,7]++;
                m_Data.m_BucketRows[3,2]++;
                m_Data.m_BucketBoxes[3, 6]++;

                m_ButtonsGuessesPins[5, 7].Text = "2";
                m_ButtonsGuessesPins[5, 7].Enabled = false;
                m_ButtonsGuessesPins[5, 7].BackColor = Color.LightSteelBlue;
                m_ButtonsGuessesPins[5, 7].ForeColor = Color.Transparent;
                m_ButtonsGuessesPins[5, 7].Starter = true;
                m_Data.m_BucketLines[1, 7]++;
                m_Data.m_BucketRows[1,5]++;
                m_Data.m_BucketBoxes[1,7]++;

                m_ButtonsGuessesPins[0, 6].Text = "9";
                m_ButtonsGuessesPins[0, 6].Enabled = false;
                m_ButtonsGuessesPins[0, 6].BackColor = Color.LightSteelBlue;
                m_ButtonsGuessesPins[0, 6].ForeColor = Color.Transparent;
                m_ButtonsGuessesPins[0, 6].Starter = true;
                m_Data.m_BucketLines[8,6]++;
                m_Data.m_BucketRows[8,0]++;
                m_Data.m_BucketBoxes[8,6]++;

                m_ButtonsGuessesPins[3, 8].Text = "4";
                m_ButtonsGuessesPins[3, 8].Enabled = false;
                m_ButtonsGuessesPins[3, 8].BackColor = Color.LightSteelBlue;
                m_ButtonsGuessesPins[3, 8].ForeColor = Color.Transparent;
                m_ButtonsGuessesPins[3, 8].Starter = true;
                m_Data.m_BucketLines[3,8]++;
                m_Data.m_BucketRows[3,3]++;
                m_Data.m_BucketBoxes[3,7]++;

                m_ButtonsGuessesPins[1, 3].Text = "6";
                m_ButtonsGuessesPins[1, 3].Enabled = false;
                m_ButtonsGuessesPins[1, 3].BackColor = Color.LightSteelBlue;
                m_ButtonsGuessesPins[1, 3].ForeColor = Color.Transparent;
                m_ButtonsGuessesPins[1, 3].Starter = true;
                m_Data.m_BucketLines[5,3]++;
                m_Data.m_BucketRows[5,1]++;
                m_Data.m_BucketBoxes[5,3]++;

                m_ButtonsGuessesPins[4, 0].Text = "6";
                m_ButtonsGuessesPins[4, 0].Enabled = false;
                m_ButtonsGuessesPins[4, 0].BackColor = Color.LightSteelBlue;
                m_ButtonsGuessesPins[4, 0].ForeColor = Color.Transparent;
                m_ButtonsGuessesPins[4, 0].Starter = true;
                m_Data.m_BucketLines[5,0]++;
                m_Data.m_BucketRows[5,4]++;
                m_Data.m_BucketBoxes[5,1]++;

                m_ButtonsGuessesPins[8, 5].Text = "3";
                m_ButtonsGuessesPins[8, 5].Enabled = false;
                m_ButtonsGuessesPins[8, 5].BackColor = Color.LightSteelBlue;
                m_ButtonsGuessesPins[8, 5].ForeColor = Color.Transparent;
                m_ButtonsGuessesPins[8, 5].Starter = true;
                m_Data.m_BucketLines[2,5]++;
                m_Data.m_BucketRows[2,8]++;
                m_Data.m_BucketBoxes[2,5]++;

                m_ButtonsGuessesPins[7, 4].Text = "1";
                m_ButtonsGuessesPins[7, 4].Enabled = false;
                m_ButtonsGuessesPins[7, 4].BackColor = Color.LightSteelBlue;
                m_ButtonsGuessesPins[7, 4].ForeColor = Color.Transparent;
                m_ButtonsGuessesPins[7, 4].Starter = true;
                m_Data.m_BucketLines[0,4]++;
                m_Data.m_BucketRows[0,7]++;
                m_Data.m_BucketBoxes[0,5]++;

                m_ButtonsGuessesPins[6, 5].Text = "7";
                m_ButtonsGuessesPins[6, 5].Enabled = false;
                m_ButtonsGuessesPins[6, 5].BackColor = Color.LightSteelBlue;
                m_ButtonsGuessesPins[6, 5].ForeColor = Color.Transparent;
                m_ButtonsGuessesPins[6, 5].Starter = true;
                m_Data.m_BucketLines[6,5]++;
                m_Data.m_BucketRows[6,6]++;
                m_Data.m_BucketBoxes[6,5]++;

            }
            else if (i_StartingSettings == 2)
            {
                m_ButtonsGuessesPins[0, 0].Text = "6";
                m_ButtonsGuessesPins[0, 0].Enabled = false;
                m_ButtonsGuessesPins[0, 0].BackColor = Color.LightSteelBlue;
                m_ButtonsGuessesPins[0, 0].ForeColor = Color.Transparent;
                m_ButtonsGuessesPins[0, 0].Starter = true;
                m_Data.m_BucketLines[5,0]++;
                m_Data.m_BucketRows[5,0]++;
                m_Data.m_BucketBoxes[5,0]++;

                m_ButtonsGuessesPins[1, 3].Text = "6";
                m_ButtonsGuessesPins[1, 3].Enabled = false;
                m_ButtonsGuessesPins[1, 3].BackColor = Color.LightSteelBlue;
                m_ButtonsGuessesPins[1, 3].ForeColor = Color.Transparent;
                m_ButtonsGuessesPins[1, 3].Starter = true;
                m_Data.m_BucketLines[5,3]++;
                m_Data.m_BucketRows[5,1]++;
                m_Data.m_BucketBoxes[5,3]++;

                m_ButtonsGuessesPins[2, 6].Text = "9";
                m_ButtonsGuessesPins[2, 6].Enabled = false;
                m_ButtonsGuessesPins[2, 6].BackColor = Color.LightSteelBlue;
                m_ButtonsGuessesPins[2, 6].ForeColor = Color.Transparent;
                m_ButtonsGuessesPins[2, 6].Starter = true;
                m_Data.m_BucketLines[8,6]++;
                m_Data.m_BucketRows[8,2]++;
                m_Data.m_BucketBoxes[8,6]++;

                m_ButtonsGuessesPins[8, 8].Text = "4";
                m_ButtonsGuessesPins[8, 8].Enabled = false;
                m_ButtonsGuessesPins[8, 8].BackColor = Color.LightSteelBlue;
                m_ButtonsGuessesPins[8, 8].ForeColor = Color.Transparent;
                m_ButtonsGuessesPins[8, 8].Starter = true;
                m_Data.m_BucketLines[3,8]++;
                m_Data.m_BucketRows[3,8]++;
                m_Data.m_BucketBoxes[3,8]++;

                m_ButtonsGuessesPins[7, 4].Text = "1";
                m_ButtonsGuessesPins[7, 4].Enabled = false;
                m_ButtonsGuessesPins[7, 4].BackColor = Color.LightSteelBlue;
                m_ButtonsGuessesPins[7, 4].ForeColor = Color.Transparent;
                m_ButtonsGuessesPins[7, 4].Starter = true;
                m_Data.m_BucketLines[0,4]++;
                m_Data.m_BucketRows[0,7]++;
                m_Data.m_BucketBoxes[0,5]++;

                m_ButtonsGuessesPins[4, 0].Text = "3";
                m_ButtonsGuessesPins[4, 0].Enabled = false;
                m_ButtonsGuessesPins[4, 0].BackColor = Color.LightSteelBlue;
                m_ButtonsGuessesPins[4, 0].ForeColor = Color.Transparent;
                m_ButtonsGuessesPins[4, 0].Starter = true;
                m_Data.m_BucketLines[2,0]++;
                m_Data.m_BucketRows[2,4]++;
                m_Data.m_BucketBoxes[2,1]++;

                m_ButtonsGuessesPins[5, 5].Text = "2";
                m_ButtonsGuessesPins[5, 5].Enabled = false;
                m_ButtonsGuessesPins[5, 5].BackColor = Color.LightSteelBlue;
                m_ButtonsGuessesPins[5, 5].ForeColor = Color.Transparent;
                m_ButtonsGuessesPins[5, 5].Starter = true;
                m_Data.m_BucketLines[1,5]++;
                m_Data.m_BucketRows[1,5]++;
                m_Data.m_BucketBoxes[1,4]++;

                m_ButtonsGuessesPins[1, 4].Text = "5";
                m_ButtonsGuessesPins[1, 4].Enabled = false;
                m_ButtonsGuessesPins[1, 4].BackColor = Color.LightSteelBlue;
                m_ButtonsGuessesPins[1, 4].ForeColor = Color.Transparent;
                m_ButtonsGuessesPins[1, 4].Starter = true;
                m_Data.m_BucketLines[4,4]++;
                m_Data.m_BucketRows[4,1]++;
                m_Data.m_BucketBoxes[4,3]++;

                m_ButtonsGuessesPins[6, 5].Text = "7";
                m_ButtonsGuessesPins[6, 5].Enabled = false;
                m_ButtonsGuessesPins[6, 5].BackColor = Color.LightSteelBlue;
                m_ButtonsGuessesPins[6, 5].ForeColor = Color.Transparent;
                m_ButtonsGuessesPins[6, 5].Starter = true;
                m_Data.m_BucketLines[6,5]++;
                m_Data.m_BucketRows[6,6]++;
                m_Data.m_BucketBoxes[6,5]++;

            }
            else if (i_StartingSettings == 3)
            {
                m_ButtonsGuessesPins[2, 7].Text = "3";
                m_ButtonsGuessesPins[2, 7].Enabled = false;
                m_ButtonsGuessesPins[2, 7].BackColor = Color.LightSteelBlue;
                m_ButtonsGuessesPins[2, 7].ForeColor = Color.Transparent;
                m_ButtonsGuessesPins[2, 7].Starter = true;
                m_Data.m_BucketLines[2, 7]++;
                m_Data.m_BucketRows[2, 2]++;
                m_Data.m_BucketBoxes[2, 6]++;


                m_ButtonsGuessesPins[5, 7].Text = "1";
                m_ButtonsGuessesPins[5, 7].Enabled = false;
                m_ButtonsGuessesPins[5, 7].BackColor = Color.LightSteelBlue;
                m_ButtonsGuessesPins[5, 7].ForeColor = Color.Transparent;
                m_ButtonsGuessesPins[5, 7].Starter = true;
                m_Data.m_BucketLines[0, 7]++;
                m_Data.m_BucketRows[0, 5]++;
                m_Data.m_BucketBoxes[0, 7]++;


                m_ButtonsGuessesPins[0, 6].Text = "9";
                m_ButtonsGuessesPins[0, 6].Enabled = false;
                m_ButtonsGuessesPins[0, 6].BackColor = Color.LightSteelBlue;
                m_ButtonsGuessesPins[0, 6].ForeColor = Color.Transparent;
                m_ButtonsGuessesPins[0, 6].Starter = true;
                m_Data.m_BucketLines[8,6]++;
                m_Data.m_BucketRows[8,0]++;
                m_Data.m_BucketBoxes[8,6]++;

                m_ButtonsGuessesPins[3, 8].Text = "8";
                m_ButtonsGuessesPins[3, 8].Enabled = false;
                m_ButtonsGuessesPins[3, 8].BackColor = Color.LightSteelBlue;
                m_ButtonsGuessesPins[3, 8].ForeColor = Color.Transparent;
                m_ButtonsGuessesPins[3, 8].Starter = true;
                m_Data.m_BucketLines[7,8]++;
                m_Data.m_BucketRows[7,3]++;
                m_Data.m_BucketBoxes[7,7]++;

                m_ButtonsGuessesPins[1, 3].Text = "8";
                m_ButtonsGuessesPins[1, 3].Enabled = false;
                m_ButtonsGuessesPins[1, 3].BackColor = Color.LightSteelBlue;
                m_ButtonsGuessesPins[1, 3].ForeColor = Color.Transparent;
                m_ButtonsGuessesPins[1, 3].Starter = true;
                m_Data.m_BucketLines[7,3]++;
                m_Data.m_BucketRows[7,1]++;
                m_Data.m_BucketBoxes[7,3]++;

                m_ButtonsGuessesPins[4, 0].Text = "6";
                m_ButtonsGuessesPins[4, 0].Enabled = false;
                m_ButtonsGuessesPins[4, 0].BackColor = Color.LightSteelBlue;
                m_ButtonsGuessesPins[4, 0].ForeColor = Color.Transparent;
                m_ButtonsGuessesPins[4, 0].Starter = true;
                m_Data.m_BucketLines[5,0]++;
                m_Data.m_BucketRows[5,4]++;
                m_Data.m_BucketBoxes[5,1]++;

                m_ButtonsGuessesPins[8, 5].Text = "2";
                m_ButtonsGuessesPins[8, 5].Enabled = false;
                m_ButtonsGuessesPins[8, 5].BackColor = Color.LightSteelBlue;
                m_ButtonsGuessesPins[8, 5].ForeColor = Color.Transparent;
                m_ButtonsGuessesPins[8, 5].Starter = true;
                m_Data.m_BucketLines[1,5]++;
                m_Data.m_BucketRows[1,8]++;
                m_Data.m_BucketBoxes[1,5]++;

                m_ButtonsGuessesPins[7, 4].Text = "1";
                m_ButtonsGuessesPins[7, 4].Enabled = false;
                m_ButtonsGuessesPins[7, 4].BackColor = Color.LightSteelBlue;
                m_ButtonsGuessesPins[7, 4].ForeColor = Color.Transparent;
                m_ButtonsGuessesPins[7, 4].Starter = true;
                m_Data.m_BucketLines[0,4]++;
                m_Data.m_BucketRows[0,7]++;
                m_Data.m_BucketBoxes[0,5]++;

                m_ButtonsGuessesPins[6, 5].Text = "5";
                m_ButtonsGuessesPins[6, 5].Enabled = false;
                m_ButtonsGuessesPins[6, 5].BackColor = Color.LightSteelBlue;
                m_ButtonsGuessesPins[6, 5].ForeColor = Color.Transparent;
                m_ButtonsGuessesPins[6, 5].Starter = true;
                m_Data.m_BucketLines[4,5]++;
                m_Data.m_BucketRows[4,6]++;
                m_Data.m_BucketBoxes[4,5]++;

            }
            else if (i_StartingSettings == 4)
            {
                m_ButtonsGuessesPins[1, 7].Text = "4";
                m_ButtonsGuessesPins[1, 7].Enabled = false;
                m_ButtonsGuessesPins[1, 7].BackColor = Color.LightSteelBlue;
                m_ButtonsGuessesPins[1, 7].ForeColor = Color.Transparent;
                m_ButtonsGuessesPins[1, 7].Starter = true;
                m_Data.m_BucketLines[3,7]++;
                m_Data.m_BucketRows[3,1]++;
                m_Data.m_BucketBoxes[3,6]++;

                m_ButtonsGuessesPins[5, 7].Text = "2";
                m_ButtonsGuessesPins[5, 7].Enabled = false;
                m_ButtonsGuessesPins[5, 7].BackColor = Color.LightSteelBlue;
                m_ButtonsGuessesPins[5, 7].ForeColor = Color.Transparent;
                m_ButtonsGuessesPins[5, 7].Starter = true;
                m_Data.m_BucketLines[1,7]++;
                m_Data.m_BucketRows[1,5]++;
                m_Data.m_BucketBoxes[1,7]++;

                m_ButtonsGuessesPins[0, 2].Text = "9";
                m_ButtonsGuessesPins[0, 2].Enabled = false;
                m_ButtonsGuessesPins[0, 2].BackColor = Color.LightSteelBlue;
                m_ButtonsGuessesPins[0, 2].ForeColor = Color.Transparent;
                m_ButtonsGuessesPins[0, 2].Starter = true;
                m_Data.m_BucketLines[8,2]++;
                m_Data.m_BucketRows[8,0]++;
                m_Data.m_BucketBoxes[8,0]++;

                m_ButtonsGuessesPins[3, 8].Text = "5";
                m_ButtonsGuessesPins[3, 8].Enabled = false;
                m_ButtonsGuessesPins[3, 8].BackColor = Color.LightSteelBlue;
                m_ButtonsGuessesPins[3, 8].ForeColor = Color.Transparent;
                m_ButtonsGuessesPins[3, 8].Starter = true;
                m_Data.m_BucketLines[4,8]++;
                m_Data.m_BucketRows[4,3]++;
                m_Data.m_BucketBoxes[4,7]++;

                m_ButtonsGuessesPins[4, 4].Text = "6";
                m_ButtonsGuessesPins[4, 4].Enabled = false;
                m_ButtonsGuessesPins[4, 4].BackColor = Color.LightSteelBlue;
                m_ButtonsGuessesPins[4, 4].ForeColor = Color.Transparent;
                m_ButtonsGuessesPins[4, 4].Starter = true;
                m_Data.m_BucketLines[5,4]++;
                m_Data.m_BucketRows[5,4]++;
                m_Data.m_BucketBoxes[5,4]++;

                m_ButtonsGuessesPins[4, 0].Text = "1";
                m_ButtonsGuessesPins[4, 0].Enabled = false;
                m_ButtonsGuessesPins[4, 0].BackColor = Color.LightSteelBlue;
                m_ButtonsGuessesPins[4, 0].ForeColor = Color.Transparent;
                m_ButtonsGuessesPins[4, 0].Starter = true;
                m_Data.m_BucketLines[0,0]++;
                m_Data.m_BucketRows[0,4]++;
                m_Data.m_BucketBoxes[0,1]++;

                m_ButtonsGuessesPins[6, 6].Text = "3";
                m_ButtonsGuessesPins[6, 6].Enabled = false;
                m_ButtonsGuessesPins[6, 6].BackColor = Color.LightSteelBlue;
                m_ButtonsGuessesPins[6, 6].ForeColor = Color.Transparent;
                m_ButtonsGuessesPins[6, 6].Starter = true;
                m_Data.m_BucketLines[2,6]++;
                m_Data.m_BucketRows[2,6]++;
                m_Data.m_BucketBoxes[2,8]++;

                m_ButtonsGuessesPins[7, 4].Text = "1";
                m_ButtonsGuessesPins[7, 4].Enabled = false;
                m_ButtonsGuessesPins[7, 4].BackColor = Color.LightSteelBlue;
                m_ButtonsGuessesPins[7, 4].ForeColor = Color.Transparent;
                m_ButtonsGuessesPins[7, 4].Starter = true;
                m_Data.m_BucketLines[0,4]++;
                m_Data.m_BucketRows[0,7]++;
                m_Data.m_BucketBoxes[0,5]++;

                m_ButtonsGuessesPins[6, 5].Text = "7";
                m_ButtonsGuessesPins[6, 5].Enabled = false;
                m_ButtonsGuessesPins[6, 5].BackColor = Color.LightSteelBlue;
                m_ButtonsGuessesPins[6, 5].ForeColor = Color.Transparent;
                m_ButtonsGuessesPins[6, 5].Starter = true;
                m_Data.m_BucketLines[6,5]++;
                m_Data.m_BucketRows[6,6]++;
                m_Data.m_BucketBoxes[6,5]++;

            }
            else if (i_StartingSettings == 5)
            {
                m_ButtonsGuessesPins[2, 7].Text = "4";
                m_ButtonsGuessesPins[2, 7].Enabled = false;
                m_ButtonsGuessesPins[2, 7].BackColor = Color.LightSteelBlue;
                m_ButtonsGuessesPins[2, 7].ForeColor = Color.Transparent;
                m_ButtonsGuessesPins[2, 7].Starter = true;
                m_Data.m_BucketLines[3, 7]++;
                m_Data.m_BucketRows[3, 2]++;
                m_Data.m_BucketBoxes[3, 6]++;


                m_ButtonsGuessesPins[5, 7].Text = "2";
                m_ButtonsGuessesPins[5, 7].Enabled = false;
                m_ButtonsGuessesPins[5, 7].BackColor = Color.LightSteelBlue;
                m_ButtonsGuessesPins[5, 7].ForeColor = Color.Transparent;
                m_ButtonsGuessesPins[5, 7].Starter = true;
                m_Data.m_BucketLines[1, 7]++;
                m_Data.m_BucketRows[1, 5]++;
                m_Data.m_BucketBoxes[1, 7]++;


                m_ButtonsGuessesPins[0, 6].Text = "9";
                m_ButtonsGuessesPins[0, 6].Enabled = false;
                m_ButtonsGuessesPins[0, 6].BackColor = Color.LightSteelBlue;
                m_ButtonsGuessesPins[0, 6].ForeColor = Color.Transparent;
                m_ButtonsGuessesPins[0, 6].Starter = true;
                m_Data.m_BucketLines[8, 6]++;
                m_Data.m_BucketRows[8, 0]++;
                m_Data.m_BucketBoxes[8, 6]++;


                m_ButtonsGuessesPins[3, 8].Text = "4";
                m_ButtonsGuessesPins[3, 8].Enabled = false;
                m_ButtonsGuessesPins[3, 8].BackColor = Color.LightSteelBlue;
                m_ButtonsGuessesPins[3, 8].ForeColor = Color.Transparent;
                m_ButtonsGuessesPins[3, 8].Starter = true;
                m_Data.m_BucketLines[3, 8]++;
                m_Data.m_BucketRows[3, 3]++;
                m_Data.m_BucketBoxes[3, 7]++;


                m_ButtonsGuessesPins[1, 3].Text = "6";
                m_ButtonsGuessesPins[1, 3].Enabled = false;
                m_ButtonsGuessesPins[1, 3].BackColor = Color.LightSteelBlue;
                m_ButtonsGuessesPins[1, 3].ForeColor = Color.Transparent;
                m_ButtonsGuessesPins[1, 3].Starter = true;
                m_Data.m_BucketLines[5, 3]++;
                m_Data.m_BucketRows[5, 1]++;
                m_Data.m_BucketBoxes[5, 3]++;


                m_ButtonsGuessesPins[4, 0].Text = "6";
                m_ButtonsGuessesPins[4, 0].Enabled = false;
                m_ButtonsGuessesPins[4, 0].BackColor = Color.LightSteelBlue;
                m_ButtonsGuessesPins[4, 0].ForeColor = Color.Transparent;
                m_ButtonsGuessesPins[4, 0].Starter = true;
                m_Data.m_BucketLines[5, 0]++;
                m_Data.m_BucketRows[5, 4]++;
                m_Data.m_BucketBoxes[5, 1]++;


                m_ButtonsGuessesPins[8, 5].Text = "3";
                m_ButtonsGuessesPins[8, 5].Enabled = false;
                m_ButtonsGuessesPins[8, 5].BackColor = Color.LightSteelBlue;
                m_ButtonsGuessesPins[8, 5].ForeColor = Color.Transparent;
                m_ButtonsGuessesPins[8, 5].Starter = true;
                m_Data.m_BucketLines[2, 5]++;
                m_Data.m_BucketRows[2, 8]++;
                m_Data.m_BucketBoxes[2, 5]++;


                m_ButtonsGuessesPins[7, 4].Text = "1";
                m_ButtonsGuessesPins[7, 4].Enabled = false;
                m_ButtonsGuessesPins[7, 4].BackColor = Color.LightSteelBlue;
                m_ButtonsGuessesPins[7, 4].ForeColor = Color.Transparent;
                m_ButtonsGuessesPins[7, 4].Starter = true;
                m_Data.m_BucketLines[0, 4]++;
                m_Data.m_BucketRows[0, 7]++;
                m_Data.m_BucketBoxes[0, 5]++;


                m_ButtonsGuessesPins[6, 5].Text = "7";
                m_ButtonsGuessesPins[6, 5].Enabled = false;
                m_ButtonsGuessesPins[6, 5].BackColor = Color.LightSteelBlue;
                m_ButtonsGuessesPins[6, 5].ForeColor = Color.Transparent;
                m_ButtonsGuessesPins[6, 5].Starter = true;
                m_Data.m_BucketLines[6, 5]++;
                m_Data.m_BucketRows[6, 6]++;
                m_Data.m_BucketBoxes[6, 5]++;
                

            }
            else
            {
                m_ButtonsGuessesPins[2, 7].Text = "4";
                m_ButtonsGuessesPins[2, 7].Enabled = false;
                m_ButtonsGuessesPins[2, 7].BackColor = Color.LightSteelBlue;
                m_ButtonsGuessesPins[2, 7].ForeColor = Color.Transparent;
                m_ButtonsGuessesPins[2, 7].Starter = true;
                m_Data.m_BucketLines[3, 7]++;
                m_Data.m_BucketRows[3, 2]++;
                m_Data.m_BucketBoxes[3, 6]++;


                m_ButtonsGuessesPins[5, 7].Text = "2";
                m_ButtonsGuessesPins[5, 7].Enabled = false;
                m_ButtonsGuessesPins[5, 7].BackColor = Color.LightSteelBlue;
                m_ButtonsGuessesPins[5, 7].ForeColor = Color.Transparent;
                m_ButtonsGuessesPins[5, 7].Starter = true;
                m_Data.m_BucketLines[1, 7]++;
                m_Data.m_BucketRows[1, 5]++;
                m_Data.m_BucketBoxes[1, 7]++;


                m_ButtonsGuessesPins[0, 6].Text = "9";
                m_ButtonsGuessesPins[0, 6].Enabled = false;
                m_ButtonsGuessesPins[0, 6].BackColor = Color.LightSteelBlue;
                m_ButtonsGuessesPins[0, 6].ForeColor = Color.Transparent;
                m_ButtonsGuessesPins[0, 6].Starter = true;
                m_Data.m_BucketLines[8, 6]++;
                m_Data.m_BucketRows[8, 0]++;
                m_Data.m_BucketBoxes[8, 6]++;


                m_ButtonsGuessesPins[3, 8].Text = "4";
                m_ButtonsGuessesPins[3, 8].Enabled = false;
                m_ButtonsGuessesPins[3, 8].BackColor = Color.LightSteelBlue;
                m_ButtonsGuessesPins[3, 8].ForeColor = Color.Transparent;
                m_ButtonsGuessesPins[3, 8].Starter = true;
                m_Data.m_BucketLines[3, 8]++;
                m_Data.m_BucketRows[3, 3]++;
                m_Data.m_BucketBoxes[3, 7]++;


                m_ButtonsGuessesPins[1, 3].Text = "6";
                m_ButtonsGuessesPins[1, 3].Enabled = false;
                m_ButtonsGuessesPins[1, 3].BackColor = Color.LightSteelBlue;
                m_ButtonsGuessesPins[1, 3].ForeColor = Color.Transparent;
                m_ButtonsGuessesPins[1, 3].Starter = true;
                m_Data.m_BucketLines[5, 3]++;
                m_Data.m_BucketRows[5, 1]++;
                m_Data.m_BucketBoxes[5, 3]++;


                m_ButtonsGuessesPins[4, 0].Text = "6";
                m_ButtonsGuessesPins[4, 0].Enabled = false;
                m_ButtonsGuessesPins[4, 0].BackColor = Color.LightSteelBlue;
                m_ButtonsGuessesPins[4, 0].ForeColor = Color.Transparent;
                m_ButtonsGuessesPins[4, 0].Starter = true;
                m_Data.m_BucketLines[5, 0]++;
                m_Data.m_BucketRows[5, 4]++;
                m_Data.m_BucketBoxes[5, 1]++;


                m_ButtonsGuessesPins[8, 5].Text = "3";
                m_ButtonsGuessesPins[8, 5].Enabled = false;
                m_ButtonsGuessesPins[8, 5].BackColor = Color.LightSteelBlue;
                m_ButtonsGuessesPins[8, 5].ForeColor = Color.Transparent;
                m_ButtonsGuessesPins[8, 5].Starter = true;
                m_Data.m_BucketLines[2, 5]++;
                m_Data.m_BucketRows[2, 8]++;
                m_Data.m_BucketBoxes[2, 5]++;


                m_ButtonsGuessesPins[7, 4].Text = "1";
                m_ButtonsGuessesPins[7, 4].Enabled = false;
                m_ButtonsGuessesPins[7, 4].BackColor = Color.LightSteelBlue;
                m_ButtonsGuessesPins[7, 4].ForeColor = Color.Transparent;
                m_ButtonsGuessesPins[7, 4].Starter = true;
                m_Data.m_BucketLines[0, 4]++;
                m_Data.m_BucketRows[0, 7]++;
                m_Data.m_BucketBoxes[0, 5]++;
                ;

                m_ButtonsGuessesPins[6, 5].Text = "7";
                m_ButtonsGuessesPins[6, 5].Enabled = false;
                m_ButtonsGuessesPins[6, 5].BackColor = Color.LightSteelBlue;
                m_ButtonsGuessesPins[6, 5].ForeColor = Color.Transparent;
                m_ButtonsGuessesPins[6, 5].Starter = true;
                m_Data.m_BucketLines[6, 5]++;
                m_Data.m_BucketRows[6, 6]++;
                m_Data.m_BucketBoxes[6, 5]++;


            }
        }

        private void SetSurrenderButton()
        {
            //// This method sets the final choice arrow buttons, according to the chosen number of chances.
            //// Create the current button, set it's details and add it to the from:
            m_ButtonSurrender = new Button();
            m_ButtonSurrender.Text = "Surrender";
            m_ButtonSurrender.Enabled = true;
            m_ButtonSurrender.BackColor = Color.OrangeRed;
            
            //// Update it's event:
            m_ButtonSurrender.Click += new EventHandler(ButtonsSurrender_Click);
            m_ButtonSurrender.Size = new Size(100, 35);
            m_ButtonSurrender.Top = m_ButtonsGuessesPins[8, 0].Top;
            m_ButtonSurrender.Left = m_ButtonsGuessesPins[8, 0].Right + 15;
            this.Controls.Add(m_ButtonSurrender);

        }

        private void SetBoldBoundaries()
        {
            Button buttonToAdd1 = new Button();
            Button buttonToAdd2 = new Button();
            Button buttonToAdd3 = new Button();
            Button buttonToAdd4 = new Button();            
            buttonToAdd1.Enabled = false;
            buttonToAdd1.BackColor = Color.Black;
            buttonToAdd1.Size = new Size(523, 10);
            buttonToAdd2.Enabled = false;
            buttonToAdd2.BackColor = Color.Black;
            buttonToAdd2.Size = new Size(523, 10);
            buttonToAdd3.Enabled = false;
            buttonToAdd3.BackColor = Color.Black;
            buttonToAdd3.Size = new Size(10, 550);
            buttonToAdd4.Enabled = false;
            buttonToAdd4.BackColor = Color.Black;
            buttonToAdd4.Size = new Size(10, 550);

            //// Left to right:
            //TOP
            buttonToAdd1.Top = 172;
            buttonToAdd1.Left = 0;
            //BOTTOM
            buttonToAdd2.Top = 347;
            buttonToAdd2.Left = 0;

            //// Up to down:
            // LEFT
            buttonToAdd3.Top = 0;
            buttonToAdd3.Left = 172;
            //RIGHT
            buttonToAdd4.Top = 0;
            buttonToAdd4.Left = 346;

            this.Controls.Add(buttonToAdd1);
            this.Controls.Add(buttonToAdd2);
            this.Controls.Add(buttonToAdd3);
            this.Controls.Add(buttonToAdd4);

        }

        private void ButtonGuess_Click(object sender, EventArgs e)
        {
            //// This method is the event triggered by clicking the guess button.
            //// Show the ColorForm, to choose a color:
            if (m_NumberForm.ShowDialog() == DialogResult.OK)
            {
                //// Update as the chosen color:
                int senderRow = (sender as ButtonWithCoordinate).Coordination.Row;
                int senderLine = (sender as ButtonWithCoordinate).Coordination.Line;
                int senderBox;
                int senderValue;
                int previousValue;
                (sender as ButtonWithCoordinate).PreviousNumber = int.Parse((sender as ButtonWithCoordinate).Text);
                (sender as ButtonWithCoordinate).Text = m_NumberForm.ChosenNumber.ToString();
                (sender as ButtonWithCoordinate).BackColor = Color.LightSteelBlue;
                (sender as ButtonWithCoordinate).ForeColor = Color.Transparent;
                senderValue = int.Parse((sender as ButtonWithCoordinate).Text);
                previousValue = (sender as ButtonWithCoordinate).PreviousNumber;
                if (senderLine < 3)
                {
                    if (senderRow < 3)
                    {
                        senderBox = 0;
                    }
                    else if (senderRow < 6)
                    {
                        senderBox = 1;
                    }
                    else
                    {
                        senderBox = 2;
                    }
                }
                else if (senderLine < 6)
                {
                    if (senderRow < 3)
                    {
                        senderBox = 3;
                    }
                    else if (senderRow < 6)
                    {
                        senderBox = 4;
                    }
                    else
                    {
                        senderBox = 5;
                    }
                }
                else
                {
                    if (senderRow < 3)
                    {
                        senderBox = 6;
                    }
                    else if (senderRow < 6)
                    {
                        senderBox = 7;
                    }
                    else
                    {
                        senderBox = 8;
                    }
                }
                int linemodule = senderLine % 3;
                int rowmodule = senderRow % 3;
                if (previousValue != senderValue) // only if the user didn't chose the same number
                {
                    m_Data.m_BucketLines[senderValue - 1, senderLine]++;
                    m_Data.m_BucketRows[senderValue - 1, senderRow]++;
                    m_Data.m_BucketBoxes[senderValue - 1, senderBox]++;
                }
                
                if (previousValue != -1 && previousValue != senderValue) // only if it's not the first pick for this button
                {
                    m_Data.m_BucketLines[previousValue - 1, senderLine]--;
                    m_Data.m_BucketRows[previousValue - 1, senderRow]--;
                    m_Data.m_BucketBoxes[previousValue - 1,senderBox]--;
                }
                

                CheckChosenLine(senderLine, senderValue);
                CheckChosenRow(senderRow, senderLine, senderValue);
                CheckChosenBox(senderBox, senderValue, senderRow, senderLine);

                CheckWinning();
            }
        }

        private void CheckChosenLine(int i_LineNumber, int senderValue)
        {
            bool v_Legal = true;
            m_CurrentButtonLegalLine = true;
            for (int index = 0; index < 9; index++)
            {
                if (m_Data.m_BucketLines[index, i_LineNumber] > 1)
                {
                    v_Legal = false;
                }
            }
            m_LegalLines[i_LineNumber] = v_Legal;
            m_CurrentButtonLegalLine = v_Legal;
            if (!v_Legal) // illegal - paint the line with RED
            {
                for (int index = 0; index < 9; index++)
                {
                    m_ButtonsGuessesPins[index, i_LineNumber].BackColor = Color.PaleVioletRed;
                    if (int.Parse(m_ButtonsGuessesPins[index, i_LineNumber].Text) == -1 && !m_ButtonsGuessesPins[index, i_LineNumber].Starter)
                    {
                        m_ButtonsGuessesPins[index, i_LineNumber].ForeColor = Color.PaleVioletRed;
                    }
                    else
                    {
                        m_ButtonsGuessesPins[index, i_LineNumber].ForeColor = Color.Transparent;
                    }
                }
            }
            else // legal - paint the buttons with gray and steel blue
            {
                for (int index = 0; index < 9; index++)
                {
                    if (m_LegalRows[index])
                    {

                        if (int.Parse(m_ButtonsGuessesPins[index, i_LineNumber].Text) == -1 && !m_ButtonsGuessesPins[index, i_LineNumber].Starter)
                        {
                            m_ButtonsGuessesPins[index, i_LineNumber].BackColor = Color.LightGray;
                            m_ButtonsGuessesPins[index, i_LineNumber].ForeColor = Color.LightGray;
                        }
                        else
                        {
                            m_ButtonsGuessesPins[index, i_LineNumber].BackColor = Color.LightSteelBlue;
                            m_ButtonsGuessesPins[index, i_LineNumber].ForeColor = Color.Transparent;
                        }
                    }
                }
            }
        }

        private void CheckChosenRow(int i_RowNumber, int i_LineNumber, int senderValue)
        {
            bool v_Legal = true;
            for (int index = 0; index < 9; index++)
            {
                if (m_Data.m_BucketRows[index, i_RowNumber] > 1)
                {
                    v_Legal = false;
                }
            }
            m_CurrentButtonLegalRow = v_Legal;
            m_LegalRows[i_RowNumber] = v_Legal;
            if (!v_Legal) // illegal - paint the line with RED
            {
                for (int index = 0; index < 9; index++)
                {
                    m_ButtonsGuessesPins[i_RowNumber, index].BackColor = Color.PaleVioletRed;
                    if (int.Parse(m_ButtonsGuessesPins[i_RowNumber, index].Text) == -1 && !m_ButtonsGuessesPins[i_RowNumber, index].Starter)
                    {
                        m_ButtonsGuessesPins[i_RowNumber, index].ForeColor = Color.PaleVioletRed;
                    }
                    else
                    {
                        m_ButtonsGuessesPins[i_RowNumber, index].ForeColor = Color.Transparent;
                    }
                }
            }
            else // legal - paint the buttons with gray and steel blue (LEAVE RED BUTTONS IF THEIR LINE IS ILLEGAL)
            {
                for (int index = 0; index < 3; index++)
                {
                    if (i_RowNumber < 3)
                    {
                        if (m_LegalBoxes[0])
                        {
                            if (index == i_LineNumber)
                            {
                                if (m_ButtonsGuessesPins[i_RowNumber, index].BackColor != Color.PaleVioletRed)
                                {
                                    m_ButtonsGuessesPins[i_RowNumber, index].BackColor = Color.PaleVioletRed;
                                    m_ButtonsGuessesPins[i_RowNumber, index].ForeColor = Color.Transparent;
                                }
                            }
                            else
                            {

                                {
                                    if (int.Parse(m_ButtonsGuessesPins[i_RowNumber, index].Text) == -1 && !m_ButtonsGuessesPins[i_RowNumber, index].Starter)
                                    {
                                        m_ButtonsGuessesPins[i_RowNumber, index].BackColor = Color.LightGray;
                                        m_ButtonsGuessesPins[i_RowNumber, index].ForeColor = Color.LightGray;
                                    }
                                    else
                                    {
                                        m_ButtonsGuessesPins[i_RowNumber, index].BackColor = Color.LightSteelBlue;
                                        m_ButtonsGuessesPins[i_RowNumber, index].ForeColor = Color.Transparent;
                                    }
                                }
                            }

                            //EVERYTHING
                        }
                    }
                    else if (i_RowNumber < 6)
                    {
                        if (m_LegalBoxes[1])
                        {
                            if (index == i_LineNumber)
                            {
                                if (m_ButtonsGuessesPins[i_RowNumber, index].BackColor != Color.PaleVioletRed)
                                {
                                    m_ButtonsGuessesPins[i_RowNumber, index].BackColor = Color.PaleVioletRed;
                                    m_ButtonsGuessesPins[i_RowNumber, index].ForeColor = Color.Transparent;
                                }
                            }
                            else
                            {

                                {
                                    if (int.Parse(m_ButtonsGuessesPins[i_RowNumber, index].Text) == -1 && !m_ButtonsGuessesPins[i_RowNumber, index].Starter)
                                    {
                                        m_ButtonsGuessesPins[i_RowNumber, index].BackColor = Color.LightGray;
                                        m_ButtonsGuessesPins[i_RowNumber, index].ForeColor = Color.LightGray;
                                    }
                                    else
                                    {
                                        m_ButtonsGuessesPins[i_RowNumber, index].BackColor = Color.LightSteelBlue;
                                        m_ButtonsGuessesPins[i_RowNumber, index].ForeColor = Color.Transparent;
                                    }
                                }
                            }

                            //EVERYTHING
                        }
                    }
                    else
                    {
                        if (m_LegalBoxes[2])
                        {
                            if (index == i_LineNumber)
                            {
                                if (m_ButtonsGuessesPins[i_RowNumber, index].BackColor != Color.PaleVioletRed)
                                {
                                    m_ButtonsGuessesPins[i_RowNumber, index].BackColor = Color.PaleVioletRed;
                                    m_ButtonsGuessesPins[i_RowNumber, index].ForeColor = Color.Transparent;
                                }
                            }
                            else
                            {

                                {
                                    if (int.Parse(m_ButtonsGuessesPins[i_RowNumber, index].Text) == -1 && !m_ButtonsGuessesPins[i_RowNumber, index].Starter)
                                    {
                                        m_ButtonsGuessesPins[i_RowNumber, index].BackColor = Color.LightGray;
                                        m_ButtonsGuessesPins[i_RowNumber, index].ForeColor = Color.LightGray;
                                    }
                                    else
                                    {
                                        m_ButtonsGuessesPins[i_RowNumber, index].BackColor = Color.LightSteelBlue;
                                        m_ButtonsGuessesPins[i_RowNumber, index].ForeColor = Color.Transparent;
                                    }
                                }
                            }

                            //EVERYTHING
                        }
                    }
                }

                for (int index = 3; index < 6; index++)
                {
                    if (i_RowNumber < 3)
                    {
                        if (m_LegalBoxes[3])
                        {
                            if (index == i_LineNumber)
                            {
                                if (m_ButtonsGuessesPins[i_RowNumber, index].BackColor != Color.PaleVioletRed)
                                {
                                    m_ButtonsGuessesPins[i_RowNumber, index].BackColor = Color.PaleVioletRed;
                                    m_ButtonsGuessesPins[i_RowNumber, index].ForeColor = Color.Transparent;
                                }
                            }
                            else
                            {

                                {
                                    if (int.Parse(m_ButtonsGuessesPins[i_RowNumber, index].Text) == -1 && !m_ButtonsGuessesPins[i_RowNumber, index].Starter)
                                    {
                                        m_ButtonsGuessesPins[i_RowNumber, index].BackColor = Color.LightGray;
                                        m_ButtonsGuessesPins[i_RowNumber, index].ForeColor = Color.LightGray;
                                    }
                                    else
                                    {
                                        m_ButtonsGuessesPins[i_RowNumber, index].BackColor = Color.LightSteelBlue;
                                        m_ButtonsGuessesPins[i_RowNumber, index].ForeColor = Color.Transparent;
                                    }
                                }
                            }

                            //EVERYTHING
                        }
                    }
                    else if (i_RowNumber < 6)
                    {
                        if (m_LegalBoxes[4])
                        {
                            if (index == i_LineNumber)
                            {
                                if (m_ButtonsGuessesPins[i_RowNumber, index].BackColor != Color.PaleVioletRed)
                                {
                                    m_ButtonsGuessesPins[i_RowNumber, index].BackColor = Color.PaleVioletRed;
                                    m_ButtonsGuessesPins[i_RowNumber, index].ForeColor = Color.Transparent;
                                }
                            }
                            else
                            {

                                {
                                    if (int.Parse(m_ButtonsGuessesPins[i_RowNumber, index].Text) == -1 && !m_ButtonsGuessesPins[i_RowNumber, index].Starter)
                                    {
                                        m_ButtonsGuessesPins[i_RowNumber, index].BackColor = Color.LightGray;
                                        m_ButtonsGuessesPins[i_RowNumber, index].ForeColor = Color.LightGray;
                                    }
                                    else
                                    {
                                        m_ButtonsGuessesPins[i_RowNumber, index].BackColor = Color.LightSteelBlue;
                                        m_ButtonsGuessesPins[i_RowNumber, index].ForeColor = Color.Transparent;
                                    }
                                }
                            }

                            //EVERYTHING
                        }
                    }
                    else
                    {
                        if (m_LegalBoxes[5])
                        {
                            if (index == i_LineNumber)
                            {
                                if (m_ButtonsGuessesPins[i_RowNumber, index].BackColor != Color.PaleVioletRed)
                                {
                                    m_ButtonsGuessesPins[i_RowNumber, index].BackColor = Color.PaleVioletRed;
                                    m_ButtonsGuessesPins[i_RowNumber, index].ForeColor = Color.Transparent;
                                }
                            }
                            else
                            {

                                {
                                    if (int.Parse(m_ButtonsGuessesPins[i_RowNumber, index].Text) == -1 && !m_ButtonsGuessesPins[i_RowNumber, index].Starter)
                                    {
                                        m_ButtonsGuessesPins[i_RowNumber, index].BackColor = Color.LightGray;
                                        m_ButtonsGuessesPins[i_RowNumber, index].ForeColor = Color.LightGray;
                                    }
                                    else
                                    {
                                        m_ButtonsGuessesPins[i_RowNumber, index].BackColor = Color.LightSteelBlue;
                                        m_ButtonsGuessesPins[i_RowNumber, index].ForeColor = Color.Transparent;
                                    }
                                }
                            }

                            //EVERYTHING
                        }
                    }
                }

                for (int index = 6; index < 9; index++)
                {
                    if (i_RowNumber < 3)
                    {
                        if (m_LegalBoxes[6])
                        {
                            if (index == i_LineNumber)
                            {
                                if (m_ButtonsGuessesPins[i_RowNumber, index].BackColor != Color.PaleVioletRed)
                                {
                                    m_ButtonsGuessesPins[i_RowNumber, index].BackColor = Color.PaleVioletRed;
                                    m_ButtonsGuessesPins[i_RowNumber, index].ForeColor = Color.Transparent;
                                }
                            }
                            else
                            {

                                {
                                    if (int.Parse(m_ButtonsGuessesPins[i_RowNumber, index].Text) == -1 && !m_ButtonsGuessesPins[i_RowNumber, index].Starter)
                                    {
                                        m_ButtonsGuessesPins[i_RowNumber, index].BackColor = Color.LightGray;
                                        m_ButtonsGuessesPins[i_RowNumber, index].ForeColor = Color.LightGray;
                                    }
                                    else
                                    {
                                        m_ButtonsGuessesPins[i_RowNumber, index].BackColor = Color.LightSteelBlue;
                                        m_ButtonsGuessesPins[i_RowNumber, index].ForeColor = Color.Transparent;
                                    }
                                }
                            }

                            //EVERYTHING
                        }
                    }
                    else if (i_RowNumber < 6)
                    {
                        if (m_LegalBoxes[7])
                        {
                            if (index == i_LineNumber)
                            {
                                if (m_ButtonsGuessesPins[i_RowNumber, index].BackColor != Color.PaleVioletRed)
                                {
                                    m_ButtonsGuessesPins[i_RowNumber, index].BackColor = Color.PaleVioletRed;
                                    m_ButtonsGuessesPins[i_RowNumber, index].ForeColor = Color.Transparent;
                                }
                            }
                            else
                            {

                                {
                                    if (int.Parse(m_ButtonsGuessesPins[i_RowNumber, index].Text) == -1 && !m_ButtonsGuessesPins[i_RowNumber, index].Starter)
                                    {
                                        m_ButtonsGuessesPins[i_RowNumber, index].BackColor = Color.LightGray;
                                        m_ButtonsGuessesPins[i_RowNumber, index].ForeColor = Color.LightGray;
                                    }
                                    else
                                    {
                                        m_ButtonsGuessesPins[i_RowNumber, index].BackColor = Color.LightSteelBlue;
                                        m_ButtonsGuessesPins[i_RowNumber, index].ForeColor = Color.Transparent;
                                    }
                                }
                            }

                            //EVERYTHING
                        }
                    }
                    else
                    {
                        if (m_LegalBoxes[8])
                        {
                            if (index == i_LineNumber)
                            {
                                if (m_ButtonsGuessesPins[i_RowNumber, index].BackColor != Color.PaleVioletRed)
                                {
                                    m_ButtonsGuessesPins[i_RowNumber, index].BackColor = Color.PaleVioletRed;
                                    m_ButtonsGuessesPins[i_RowNumber, index].ForeColor = Color.Transparent;
                                }
                            }
                            else
                            {

                                {
                                    if (int.Parse(m_ButtonsGuessesPins[i_RowNumber, index].Text) == -1 && !m_ButtonsGuessesPins[i_RowNumber, index].Starter)
                                    {
                                        m_ButtonsGuessesPins[i_RowNumber, index].BackColor = Color.LightGray;
                                        m_ButtonsGuessesPins[i_RowNumber, index].ForeColor = Color.LightGray;
                                    }
                                    else
                                    {
                                        m_ButtonsGuessesPins[i_RowNumber, index].BackColor = Color.LightSteelBlue;
                                        m_ButtonsGuessesPins[i_RowNumber, index].ForeColor = Color.Transparent;
                                    }
                                }
                            }

                            //EVERYTHING
                        }
                    }
                }

                /*(for (int index = 0; index < 9; index++)
                {
                    
                    if (index == i_LineNumber)
                    {
                        if (m_ButtonsGuessesPins[i_RowNumber, index].BackColor != Color.PaleVioletRed)
                        {
                            m_ButtonsGuessesPins[i_RowNumber, index].BackColor = Color.PaleVioletRed;
                            m_ButtonsGuessesPins[i_RowNumber, index].ForeColor = Color.Transparent;
                        }
                    }
                    else
                    {
                     
                        {
                            if (int.Parse(m_ButtonsGuessesPins[i_RowNumber, index].Text) == -1 && !m_ButtonsGuessesPins[i_RowNumber, index].Starter)
                            {
                                m_ButtonsGuessesPins[i_RowNumber, index].BackColor = Color.LightGray;
                                m_ButtonsGuessesPins[i_RowNumber, index].ForeColor = Color.LightGray;
                            }
                            else
                            {
                                m_ButtonsGuessesPins[i_RowNumber, index].BackColor = Color.LightSteelBlue;
                                m_ButtonsGuessesPins[i_RowNumber, index].ForeColor = Color.Transparent;
                            }
                        }
                    }
                }*/
            }

        }

        private void CheckChosenBox(int i_BoxNumber, int senderValue, int i_RowNumber, int i_LineNumber)
        {
            bool v_Legal = true;
            if (i_BoxNumber == 0)
            {
                for (int index = 0; index < 9; index++)
                {
                    if (m_Data.m_BucketBoxes[index, i_BoxNumber] > 1)
                    {
                        v_Legal = false;
                    }
                }

                if (!v_Legal) // the current box is illegal - paint it with red
                {
                    for (int line = 0; line < 3; line++)
                    {
                        for (int row = 0; row < 3; row++)
                        {
                            m_ButtonsGuessesPins[row, line].BackColor = Color.PaleVioletRed;
                            if (int.Parse(m_ButtonsGuessesPins[row, line].Text) == -1 && !m_ButtonsGuessesPins[row, line].Starter)
                            {
                                m_ButtonsGuessesPins[row, line].ForeColor = Color.PaleVioletRed;
                            }
                            else
                            {
                                m_ButtonsGuessesPins[row, line].ForeColor = Color.Transparent;
                            }
                        }
                    }
                }
                else // the box is legal after the move
                {
                    for (int line = 0; line < 3; line++)
                    {
                        for (int row = 0; row < 3; row++)
                        {
                            if (m_LegalLines[line] && m_LegalRows[row])
                            { 
                     
                            
                                if (int.Parse(m_ButtonsGuessesPins[row, line].Text) == -1 && !m_ButtonsGuessesPins[row, line].Starter)
                                {
                                    m_ButtonsGuessesPins[row, line].BackColor = Color.LightGray;
                                    m_ButtonsGuessesPins[row, line].ForeColor = Color.LightGray;
                                }
                                else
                                {
                                    m_ButtonsGuessesPins[row, line].BackColor = Color.LightSteelBlue;
                                    m_ButtonsGuessesPins[row, line].ForeColor = Color.Transparent;
                                }
                            }
                            else
                            {

                            }
                        }
                    }
                }
            }

            else if (i_BoxNumber == 1)
            {
                for (int index = 0; index < 9; index++)
                {
                    if (m_Data.m_BucketBoxes[index, i_BoxNumber] > 1)
                    {
                        v_Legal = false;
                    }
                }

                if (!v_Legal) // the current box is illegal - paint it with red
                {
                    for (int line = 0; line < 3; line++)
                    {
                        for (int row = 3; row < 6; row++)
                        {
                            m_ButtonsGuessesPins[row, line].BackColor = Color.PaleVioletRed;
                            if (int.Parse(m_ButtonsGuessesPins[row, line].Text) == -1 && !m_ButtonsGuessesPins[row, line].Starter)
                            {
                                m_ButtonsGuessesPins[row, line].ForeColor = Color.PaleVioletRed;
                            }
                            else
                            {
                                m_ButtonsGuessesPins[row, line].ForeColor = Color.Transparent;
                            }
                        }
                    }
                }
                else // the box is legal after the move
                {
                    for (int line = 0; line < 3; line++)
                    {
                        for (int row = 3; row < 6; row++)
                        {
                            if (m_LegalLines[line] && m_LegalRows[row])
                            {
                                if (int.Parse(m_ButtonsGuessesPins[row, line].Text) == -1 && !m_ButtonsGuessesPins[row, line].Starter)
                                {
                                    m_ButtonsGuessesPins[row, line].BackColor = Color.LightGray;
                                    m_ButtonsGuessesPins[row, line].ForeColor = Color.LightGray;
                                }
                                else
                                {
                                    m_ButtonsGuessesPins[row, line].BackColor = Color.LightSteelBlue;
                                    m_ButtonsGuessesPins[row, line].ForeColor = Color.Transparent;
                                }
                            }
                        }
                    }
                }
            }

            else if (i_BoxNumber == 2)
            {
                for (int index = 0; index < 9; index++)
                {
                    if (m_Data.m_BucketBoxes[index, i_BoxNumber] > 1)
                    {
                        v_Legal = false;
                    }
                }

                if (!v_Legal) // the current box is illegal - paint it with red
                {
                    for (int line = 0; line < 3; line++)
                    {
                        for (int row = 6; row < 9; row++)
                        {
                            m_ButtonsGuessesPins[row, line].BackColor = Color.PaleVioletRed;
                            if (int.Parse(m_ButtonsGuessesPins[row, line].Text) == -1 && !m_ButtonsGuessesPins[row, line].Starter)
                            {
                                m_ButtonsGuessesPins[row, line].ForeColor = Color.PaleVioletRed;
                            }
                            else
                            {
                                m_ButtonsGuessesPins[row, line].ForeColor = Color.Transparent;
                            }
                        }
                    }
                }
                else // the box is legal after the move
                {
                    for (int line = 0; line < 3; line++)
                    {
                        for (int row = 6; row < 9; row++)
                        {
                            if (m_LegalLines[line] && m_LegalRows[row])
                            {
                                if (int.Parse(m_ButtonsGuessesPins[row, line].Text) == -1 && !m_ButtonsGuessesPins[row, line].Starter)
                                {
                                    m_ButtonsGuessesPins[row, line].BackColor = Color.LightGray;
                                    m_ButtonsGuessesPins[row, line].ForeColor = Color.LightGray;
                                }
                                else
                                {
                                    m_ButtonsGuessesPins[row, line].BackColor = Color.LightSteelBlue;
                                    m_ButtonsGuessesPins[row, line].ForeColor = Color.Transparent;
                                }
                            }
                        }
                    }
                }
            }

            else if (i_BoxNumber == 3)
            {
                for (int index = 0; index < 9; index++)
                {
                    if (m_Data.m_BucketBoxes[index, i_BoxNumber] > 1)
                    {
                        v_Legal = false;
                    }
                }

                if (!v_Legal) // the current box is illegal - paint it with red
                {
                    for (int line = 3; line < 6; line++)
                    {
                        for (int row = 0; row < 3; row++)
                        {
                            m_ButtonsGuessesPins[row, line].BackColor = Color.PaleVioletRed;
                            if (int.Parse(m_ButtonsGuessesPins[row, line].Text) == -1 && !m_ButtonsGuessesPins[row, line].Starter)
                            {
                                m_ButtonsGuessesPins[row, line].ForeColor = Color.PaleVioletRed;
                            }
                            else
                            {
                                m_ButtonsGuessesPins[row, line].ForeColor = Color.Transparent;
                            }
                        }
                    }
                }
                else // the box is legal after the move
                {
                    for (int line = 3; line < 6; line++)
                    {
                        for (int row = 0; row < 3; row++)
                        {
                            if (m_LegalLines[line] && m_LegalRows[row])
                            {
                                if (int.Parse(m_ButtonsGuessesPins[row, line].Text) == -1 && !m_ButtonsGuessesPins[row, line].Starter)
                                {
                                    m_ButtonsGuessesPins[row, line].BackColor = Color.LightGray;
                                    m_ButtonsGuessesPins[row, line].ForeColor = Color.LightGray;
                                }
                                else
                                {
                                    m_ButtonsGuessesPins[row, line].BackColor = Color.LightSteelBlue;
                                    m_ButtonsGuessesPins[row, line].ForeColor = Color.Transparent;
                                }
                            }
                        }
                    }
                }
            }

            else if (i_BoxNumber == 4)
            {
                for (int index = 0; index < 9; index++)
                {
                    if (m_Data.m_BucketBoxes[index, i_BoxNumber] > 1)
                    {
                        v_Legal = false;
                    }
                }

                if (!v_Legal) // the current box is illegal - paint it with red
                {
                    for (int line = 3; line < 6; line++)
                    {
                        for (int row = 3; row < 6; row++)
                        {
                            m_ButtonsGuessesPins[row, line].BackColor = Color.PaleVioletRed;
                            if (int.Parse(m_ButtonsGuessesPins[row, line].Text) == -1 && !m_ButtonsGuessesPins[row, line].Starter)
                            {
                                m_ButtonsGuessesPins[row, line].ForeColor = Color.PaleVioletRed;
                            }
                            else
                            {
                                m_ButtonsGuessesPins[row, line].ForeColor = Color.Transparent;
                            }
                        }
                    }
                }
                else // the box is legal after the move
                {
                    for (int line = 3; line < 6; line++)
                    {
                        for (int row = 3; row < 6; row++)
                        {
                            if (m_LegalLines[line] && m_LegalRows[row])
                            {
                                if (int.Parse(m_ButtonsGuessesPins[row, line].Text) == -1 && !m_ButtonsGuessesPins[row, line].Starter)
                                {
                                    m_ButtonsGuessesPins[row, line].BackColor = Color.LightGray;
                                    m_ButtonsGuessesPins[row, line].ForeColor = Color.LightGray;
                                }
                                else
                                {
                                    m_ButtonsGuessesPins[row, line].BackColor = Color.LightSteelBlue;
                                    m_ButtonsGuessesPins[row, line].ForeColor = Color.Transparent;
                                }
                            }
                        }
                    }
                }
            }

            else if (i_BoxNumber == 5)
            {
                for (int index = 0; index < 9; index++)
                {
                    if (m_Data.m_BucketBoxes[index, i_BoxNumber] > 1)
                    {
                        v_Legal = false;
                    }
                }
                m_LegalBoxes[i_BoxNumber] = v_Legal;

                if (!v_Legal) // the current box is illegal - paint it with red
                {
                    for (int line = 3; line < 6; line++)
                    {
                        for (int row = 6; row < 9; row++)
                        {
                            m_ButtonsGuessesPins[row, line].BackColor = Color.PaleVioletRed;
                            if (int.Parse(m_ButtonsGuessesPins[row, line].Text) == -1 && !m_ButtonsGuessesPins[row, line].Starter)
                            {
                                m_ButtonsGuessesPins[row, line].ForeColor = Color.PaleVioletRed;
                            }
                            else
                            {
                                m_ButtonsGuessesPins[row, line].ForeColor = Color.Transparent;
                            }
                        }
                    }
                }
                else // the box is legal after the move
                {
                    for (int line = 3; line < 6; line++)
                    {
                        for (int row = 6; row < 9; row++)
                        {
                            if (m_LegalLines[line] && m_LegalRows[row])
                            {
                                if (int.Parse(m_ButtonsGuessesPins[row, line].Text) == -1 && !m_ButtonsGuessesPins[row, line].Starter)
                                {
                                    m_ButtonsGuessesPins[row, line].BackColor = Color.LightGray;
                                    m_ButtonsGuessesPins[row, line].ForeColor = Color.LightGray;
                                }
                                else
                                {
                                    m_ButtonsGuessesPins[row, line].BackColor = Color.LightSteelBlue;
                                    m_ButtonsGuessesPins[row, line].ForeColor = Color.Transparent;
                                }
                            }
                        }
                    }
                }
            }

            else if (i_BoxNumber == 6)
            {
                for (int index = 0; index < 9; index++)
                {
                    if (m_Data.m_BucketBoxes[index, i_BoxNumber] > 1)
                    {
                        v_Legal = false;
                    }
                }

                if (!v_Legal) // the current box is illegal - paint it with red
                {
                    for (int line = 6; line < 9; line++)
                    {
                        for (int row = 0; row < 3; row++)
                        {
                            m_ButtonsGuessesPins[row, line].BackColor = Color.PaleVioletRed;
                            if (int.Parse(m_ButtonsGuessesPins[row, line].Text) == -1 && !m_ButtonsGuessesPins[row, line].Starter)
                            {
                                m_ButtonsGuessesPins[row, line].ForeColor = Color.PaleVioletRed;
                            }
                            else
                            {
                                m_ButtonsGuessesPins[row, line].ForeColor = Color.Transparent;
                            }
                        }
                    }
                }
                else // the box is legal after the move
                {
                    for (int line = 6; line < 9; line++)
                    {
                        for (int row = 0; row < 3; row++)
                        {
                            if (m_LegalLines[line] && m_LegalRows[row])
                            {
                                if (int.Parse(m_ButtonsGuessesPins[row, line].Text) == -1 && !m_ButtonsGuessesPins[row, line].Starter)
                                {
                                    m_ButtonsGuessesPins[row, line].BackColor = Color.LightGray;
                                    m_ButtonsGuessesPins[row, line].ForeColor = Color.LightGray;
                                }
                                else
                                {
                                    m_ButtonsGuessesPins[row, line].BackColor = Color.LightSteelBlue;
                                    m_ButtonsGuessesPins[row, line].ForeColor = Color.Transparent;
                                }
                            }
                        }
                    }
                }
            }

            else if (i_BoxNumber == 7)
            {
                for (int index = 0; index < 9; index++)
                {
                    if (m_Data.m_BucketBoxes[index, i_BoxNumber] > 1)
                    {
                        v_Legal = false;
                    }
                }

                if (!v_Legal) // the current box is illegal - paint it with red
                {
                    for (int line = 6; line < 9; line++)
                    {
                        for (int row = 3; row < 6; row++)
                        {
                            m_ButtonsGuessesPins[row, line].BackColor = Color.PaleVioletRed;
                            if (int.Parse(m_ButtonsGuessesPins[row, line].Text) == -1 && !m_ButtonsGuessesPins[row, line].Starter)
                            {
                                m_ButtonsGuessesPins[row, line].ForeColor = Color.PaleVioletRed;
                            }
                            else
                            {
                                m_ButtonsGuessesPins[row, line].ForeColor = Color.Transparent;
                            }
                        }
                    }
                }
                else // the box is legal after the move
                {
                    for (int line = 6; line < 9; line++)
                    {
                        for (int row = 3; row < 6; row++)
                        {
                            if (m_LegalLines[line] && m_LegalRows[row])
                            {
                                if (int.Parse(m_ButtonsGuessesPins[row, line].Text) == -1 && !m_ButtonsGuessesPins[row, line].Starter)
                                {
                                    m_ButtonsGuessesPins[row, line].BackColor = Color.LightGray;
                                    m_ButtonsGuessesPins[row, line].ForeColor = Color.LightGray;
                                }
                                else
                                {
                                    m_ButtonsGuessesPins[row, line].BackColor = Color.LightSteelBlue;
                                    m_ButtonsGuessesPins[row, line].ForeColor = Color.Transparent;
                                }
                            }
                        }
                    }
                }
            }

            else // BOXNUMBER IS 8
            {
                for (int index = 0; index < 9; index++)
                {
                    if (m_Data.m_BucketBoxes[index, i_BoxNumber] > 1)
                    {
                        v_Legal = false;
                    }
                }

                if (!v_Legal) // the current box is illegal - paint it with red
                {
                    for (int line = 6; line < 9; line++)
                    {
                        for (int row = 6; row < 9; row++)
                        {
                            m_ButtonsGuessesPins[row, line].BackColor = Color.PaleVioletRed;
                            if (int.Parse(m_ButtonsGuessesPins[row, line].Text) == -1 && !m_ButtonsGuessesPins[row, line].Starter)
                            {
                                m_ButtonsGuessesPins[row, line].ForeColor = Color.PaleVioletRed;
                            }
                            else
                            {
                                m_ButtonsGuessesPins[row, line].ForeColor = Color.Transparent;
                            }
                        }
                    }
                }
                else // the box is legal after the move
                {
                    for (int line = 6; line < 9; line++)
                    {
                        for (int row = 6; row < 9; row++)
                        {
                            if (m_LegalLines[line] && m_LegalRows[row])
                            {
                                if (int.Parse(m_ButtonsGuessesPins[row, line].Text) == -1 && !m_ButtonsGuessesPins[row, line].Starter)
                                {
                                    m_ButtonsGuessesPins[row, line].BackColor = Color.LightGray;
                                    m_ButtonsGuessesPins[row, line].ForeColor = Color.LightGray;
                                }
                                else
                                {
                                    m_ButtonsGuessesPins[row, line].BackColor = Color.LightSteelBlue;
                                    m_ButtonsGuessesPins[row, line].ForeColor = Color.Transparent;
                                }
                            }
                        }
                    }
                }
            }

        }
        // checks if the line is legal after the user chose a number and paint the board RED/GRAY if illegal or legal 

        private void CheckWinning()
        {
            bool v_Win = true;
            for (int line = 0; line < 9; line++)
            {
                for (int row = 0; row < 9; row++)
                {
                    if ((int.Parse(m_ButtonsGuessesPins[row, line].Text) == -1))
                    {
                        v_Win = false;
                    }
                }
            }
            if (v_Win)
            {
                for (int index = 0; index < 9; index++)
                {
                    if (!(m_LegalLines[index] && m_LegalRows[index] && m_LegalBoxes[index]))
                    {
                        v_Win = false;
                    }
                }
            }
            if (v_Win) // END OF THE GAME
            {
                for (int line = 0; line < 9; line++)
                {
                    for (int row = 0; row < 9; row++)
                    {
                        m_ButtonsGuessesPins[row, line].BackColor = Color.DarkSeaGreen;
                        m_ButtonsGuessesPins[row, line].Enabled = false;
                    }
                }
            }
        }

        private void ButtonsSurrender_Click(object sender, EventArgs e)
        {
            if (m_QuitForm.ShowDialog() == DialogResult.OK)
            {
                if (m_QuitForm.WantsToRestart) // user wants to restart
                {
                    ResetButtons();
                }
                else // user wants to quit
                {
                    this.Close();
                }
            }
        }

        private void ResetButtons()
        {
            for (int line = 0; line < 9; line++)
            {
                for (int row = 0; row < 9; row++)
                {
                    m_ButtonsGuessesPins[row, line].Text = "-1";
                    m_ButtonsGuessesPins[row, line].BackColor = Color.LightGray;
                    m_ButtonsGuessesPins[row, line].ForeColor = Color.LightGray;
                    m_ButtonsGuessesPins[row, line].Starter = false;
                    m_ButtonsGuessesPins[row, line].Enabled = true;
                }
            }
            for (int index = 0; index < 9; index++)
            {
                for (int index1 = 0; index1 < 9; index1++)
                {
                    m_Data.m_BucketLines[index, index1] = 0;
                    m_Data.m_BucketRows[index, index1] = 0;
                    m_Data.m_BucketBoxes[index, index1] = 0;
                }
            }
            Random rnd = new Random();
            SetStartingButtons(rnd.Next(1, 6));
        }
    }
}