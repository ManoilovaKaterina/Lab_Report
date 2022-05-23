using System;
using System.Text;

namespace АСД_л1
{
    public class Node<T>
    {
        public Node(T data)
        {
            Data = data;
        }
        public T Data;
        public Node<T> Next;
    }
    class LinkedList<T>
    {
        public Node<T> head;
        public Node<T> tail;
        int count;

        public void Add(T data)
        {
            Node<T> node = new Node<T>(data);

            if (head == null)
                head = node;
            else
                tail.Next = node;
            tail = node;

            count++;
        }

        public int Count { get { return count; } }

        public void Clear()
        {
            head = null;
            tail = null;
            count = 0;
        }
    }
}

