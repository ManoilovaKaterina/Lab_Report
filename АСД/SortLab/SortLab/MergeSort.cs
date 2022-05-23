using System;
using System.Collections.Generic;
using System.Text;

namespace SortLab
{
    static class MS
    {
        ////////////////////////// масив //////////////////////////
        static void Merge(int[] Arr, int lowI, int midI, int highI)
        {
            var left = lowI;
            var right = midI + 1;
            var tempArr = new int[highI - lowI + 1];
            var index = 0;

            while ((left <= midI) && (right <= highI))
            {
                if (Arr[left] < Arr[right])
                {
                    tempArr[index] = Arr[left];
                    left++;
                }
                else
                {
                    tempArr[index] = Arr[right];
                    right++;
                }

                index++;
            }

            for (var i = left; i <= midI; i++)
            {
                tempArr[index] = Arr[i];
                index++;
            }

            for (var i = right; i <= highI; i++)
            {
                tempArr[index] = Arr[i];
                index++;
            }

            for (var i = 0; i < tempArr.Length; i++)
            {
                Arr[lowI + i] = tempArr[i];
            }
        }

        static int[] MergeSort(int[] Arr, int lowI, int highI)
        {
            if (lowI < highI)
            {
                int midI = (lowI + highI) / 2;
                MergeSort(Arr, lowI, midI);
                MergeSort(Arr, midI + 1, highI);
                Merge(Arr, lowI, midI, highI);
            }

            return Arr;
        }

        public static int[] MergeSort(int[] Arr)
        {
            return MergeSort(Arr, 0, Arr.Length - 1);
        }

        ////////////////////////// список //////////////////////////

        static Node Merge(Node a, Node b)
        {
            Node result = null;

            if (a == null)
            {
                return b;
            }
            if (b == null)
            {
                return a;
            }

            if (a.Data <= b.Data)
            {
                result = a;
                result.Next = Merge(a.Next, b);
            }
            else
            {
                result = b;
                result.Next = Merge(a, b.Next);
            }

            return result;
        }

        public static Node mergeSort(Node h)
        {
            if (h == null || h.Next == null)
            {
                return h;
            }

            Node Mid = LinkedList.findMid(h);
            Node NextMid = Mid.Next;

            Mid.Next = null;

            Node left = mergeSort(h);

            Node right = mergeSort(NextMid);

            Node sortedlist = Merge(left, right);
            return sortedlist;
        }

        public static LinkedList MergeSort(LinkedList List)
        {
            List.head = mergeSort(List.head);
            return List;
        }
    }
}
