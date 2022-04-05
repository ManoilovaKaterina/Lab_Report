using System;
using System.Collections.Generic;
using System.Text;

namespace АСД_л1
{
    class BarSearch
    {
        public int BarS(int[] Array, int SearchVal)
        {
            int i = 0;
            int[] tempArr = new int[Array.Length + 1];

            while (i < Array.Length)
            {
                tempArr[i] = Array[i];
                i++;
            }

            tempArr[i] = SearchVal;
            i = 0;

            while (tempArr[i] != SearchVal)
            {
                i++;
            }

            if (i == Array.Length)
                return -1;
            else
                return i;
        }

        public int BarS(LinkedList<int> list, int SearchVal)
        {
            int i = 0;
            list.AddLast(SearchVal);

            LinkedListNode<int> Val = list.First;

            while (Val.Value != SearchVal)
            {
                i++;
                Val = Val.Next;
            }

            list.RemoveLast();

            if (i == list.Count)
                return -1;
            else
                return i;
        }
    }
}
