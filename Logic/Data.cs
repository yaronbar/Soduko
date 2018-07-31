using System;
using System.Collections.Generic;
using System.Text;

namespace Logic
{
    public class Data
    {
        public int[,] m_BucketLines;
        public int[,] m_BucketRows;
        public int[,] m_BucketBoxes;

        public Data()
        {
            m_BucketLines = new int[9, 9];
            m_BucketRows = new int[9, 9];
            m_BucketBoxes = new int[9, 9]; // the first is the number of the box (1-9) and the second is the number of the button in the box
        }
    }
}
