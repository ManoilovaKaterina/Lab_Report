using System;
using System.Collections.Generic;
using System.Text;

namespace АСД_л1
{
    class BinSearch
    {
        static double Divider;

        public static double divider
        {
            get 
            { return Divider; }
            set 
            { Divider = value; }
        }

        public int BinS(int[] Array, int SearchVal)
        {
            int Left = 0, Right = Array.Length - 1;
            while (Left <= Right)
            {
                int Mid = (int)(Left + (Right - Left) / Divider);

                if (Array[Mid] == SearchVal)
                {
                    return Mid;
                }

                if (Array[Mid] < SearchVal)
                {
                    Left = Mid + 1;
                }
                else
                {
                    Right = Mid - 1;
                }
            }

            return -1;
        }

        private LinkedListNode<int> FindEl(LinkedList<int> list, int Index)
        {
            LinkedListNode<int> Node = list.First;
            for (int i = 0; i < Index; i++)
            {
                Node = Node.Next;
            }
            return Node;
        }

        public int BinS(LinkedList<int> list, int SearchVal)
        {
            int Left = 0, Right = list.Count - 1;
            while (Left <= Right)
            {
                int Mid = (int)(Left + (Right - Left) / Divider);

                if (FindEl(list, Mid).Value == SearchVal)
                {
                    return Mid;
                }

                if (FindEl(list, Mid).Value < SearchVal)
                {
                    Left = Mid + 1;
                }
                else
                {
                    Right = Mid - 1;
                }
            }

            return -1;
        }
    }
}
