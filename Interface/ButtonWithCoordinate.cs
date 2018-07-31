using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Interface
{
    class ButtonWithCoordinate : Button
    {
        private Point m_Coordination;
        private bool m_v_IsStarter;
        private int m_PreviousNumber;
        public Point Coordination
        {
            get
            {
                return m_Coordination;
            }
            set
            {
                m_Coordination = value;
            }
        }
        public bool Starter
        {
            get
            {
                return m_v_IsStarter;
            }
            set
            {
                m_v_IsStarter = value;
            }
        }
        public int PreviousNumber
        {
            get
            {
                return m_PreviousNumber;
            }
            set
            {
                m_PreviousNumber = value;
            }
        }

    }

    class Point
    {
        private int m_Line;
        private int m_Row;

        public int Line
        {
            get
            {
                return m_Line;
            }
            set
            {
                m_Line = value;
            }
        }

        public int Row
        {
            get
            {
                return m_Row;
            }
            set
            {
                m_Row = value;
            }
        }
    }
}
