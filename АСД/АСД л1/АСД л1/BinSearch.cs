using System;
using System.Text;

namespace АСД_л1
{
    static class BinSearch
    {
        static double Divider;
        public static int BinS(int[] Array, int SearchVal, bool ifGold)
        {
            if (ifGold)
                Divider = (Math.Sqrt(5) + 1) / 2;
            else
                Divider = 2.0;

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

        private static Node<int> FindEl(LinkedList<int> list, int Index)
        {
            Node<int> Node = list.head;
            for (int i = 0; i < Index; i++)
            {
                Node = Node.Next;
            }
            return Node;
        }

        public static int BinS(LinkedList<int> list, int SearchVal, bool ifGold)
        {
            if (ifGold)
                Divider = (Math.Sqrt(5) + 1) / 2;
            else
                Divider = 2.0;

            int Left = 0, Right = list.Count - 1;
            while (Left <= Right)
            {
                int Mid = (int)(Left + (Right - Left) / Divider);

                if (FindEl(list, Mid).Data == SearchVal)
                {
                    return Mid;
                }

                if (FindEl(list, Mid).Data < SearchVal)
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
