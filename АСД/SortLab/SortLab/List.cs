using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortLab
{
    public class Node
    {
        public Node(int data, Node Next)
        {
            Data = data;
            Next = Next;
        }
        public Node(int data)
        {
            Data = data;
        }
        public int Data;
        public Node Next;
    }
    class LinkedList
    {
        public Node head;
        public Node tail;
        int count;

        public void Add(int data)
        {
            Node Node = new Node(data, null);

            if (head == null)
                head = Node;
            else
                tail.Next = Node;
            tail = Node;

            count++;
        }

        public Node GetIndex(int Index)
        {
            Node Node = this.head;
            for (int i = 0; i < Index; i++)
            {
                Node = Node.Next;
            }
            return Node;
        }

        public static Node findMid(Node h)
        {
            Node slow = h, fast = h.Next;
            while (fast != null && fast.Next != null)
            {
                slow = slow.Next;
                fast = fast.Next.Next;
            }
            return slow;
        }

        public int Count 
        {
            get
            { return count; }
            set
            { count = value; }
        }

        public void Clear()
        {
            head = null;
            tail = null;
            count = 0;
        }
    }
}
