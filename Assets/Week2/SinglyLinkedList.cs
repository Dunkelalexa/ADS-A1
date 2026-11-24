using System;
using System.Collections;
using System.Collections.Generic;

namespace ADS.Week2
{
    public class Node<T>
    {
        public T Value;
        public Node<T> Next;

        public Node(T value)
        {
            Value = value;
        }
    }

    public class SinglyLinkedList<T> : IEnumerable<T>
    {
        private Node<T> head;
        private Node<T> tail;
        private int count;

        public int Count => count;

        public void AddFirst(T item)
        {
            var node = new Node<T>(item);
            node.Next = head;                
            head = node;                    //changes the head
            if(tail == null)                //in case the list has only 1 node, so the node is the head and the tail
            {
                tail = node;        
            }

            count++;
        }

        public void AddLast(T item)
        {
            var node = new Node<T>(item);
            if (tail == null) 
            {
                head = node;
            } else
            {
                tail.Next = node;
            }

            tail = node;
            count++;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (var n = head; n != null; n = n.Next)               //because we are using enumerator we need this for, whenever using a foreach en un linked list I need the GetEnumerator
                yield return n.Value;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();                                 //
        }
    }
}
