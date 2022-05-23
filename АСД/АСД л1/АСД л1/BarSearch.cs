using System;
using System.Text;

namespace АСД_л1
{
    static class BarSearch
    {
        public static int BarS(int[] Array, int SearchVal)
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

        public static int BarS(LinkedList<int> list, int SearchVal)
        {
            int i = 0;
            LinkedList<int> newlist = new LinkedList<int>();
            Node<int> N = list.head;
            for (int j = 0; j < list.Count; j++)
            {
                newlist.Add(N.Data);
                N = N.Next;
            }
            newlist.Add(SearchVal);

            Node<int> Val = newlist.head;

            while (Val.Data != SearchVal)
            {
                i++;
                Val = Val.Next;
            }

            if (i >= list.Count)
                return -1;
            else
                return i;
        }
    }
}
