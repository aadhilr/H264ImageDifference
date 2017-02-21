using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H264Images
{
    public class Node
    {
        public int x;
        public int y;
        public Color col;
        public Node next;

        public Node(int i, int j, Color c)
        {
            x = i;
            y = j;
            col = c;
            next = null; 
        }

        public void AddToEnd(int i, int j, Color c)
        {
            if (next == null)
            {
                next = new Node(i, j, c);
            }
            else
            {
                next.AddToEnd(i, j, c);
            }
        }
    }


}
