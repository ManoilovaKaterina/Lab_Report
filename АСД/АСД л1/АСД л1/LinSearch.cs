using System;
using System.Collections.Generic;
using System.Text;

namespace АСД_л1
{
    class LinSearch
    {
        public int LinS(int[] Array, int SearchVal)
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

        public int LinS(LinkedList<int> list, int SearchVal)
        {
            int i = 0, Pos = 0;
            bool Found = false;

            LinkedListNode<int> Val = list.First;

            while (Val != null && !Found)
            {
                if (Val.Value == SearchVal)
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
