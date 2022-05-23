using System;
using System.Text;

namespace АСД_л1
{
    static class LinSearch
    {
        public static int LinS(int[] Array, int SearchVal)
        {
            int i = 0, Pos = 0;
            bool Found = false;

            while (i < Array.Length && !Found)
            {
                if (Array[i] == SearchVal)
                {
                    Pos = i;
                    Found = true;
                }
                i++;
            }


            if (Found)
                return Pos;
            else
                return -1;
        }

        public static int LinS(LinkedList<int> list, int SearchVal)
        {
            int i = 0, Pos = 0;
            bool Found = false;

            Node<int> Val = list.head;

            while (Val != null && !Found)
            {
                if (Val.Data == SearchVal)
                {
                    Pos = i;
                    Found = true;
                }
                i++;
                Val = Val.Next;
            }

            if (Found)
                return Pos;
            else
                return -1;
        }
    }
}
