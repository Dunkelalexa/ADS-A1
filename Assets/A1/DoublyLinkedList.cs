using System;
using System.Collections;
using System.Collections.Generic;

namespace ADS.A1
{
    public class DoublyNode<T>
    {
        public T Value;
        public DoublyNode<T> Next;
        public DoublyNode<T> Prev;

        public DoublyNode(T value)
        {
            Value = value;
        }
    }

    public class DoublyLinkedList<T> : IEnumerable<T>
    {
        private DoublyNode<T> head;
        private DoublyNode<T> tail;
        private int count;

        public int Count => count;

        public T this[int index]                //this next to [] is an indexer, is going to allow me access the doubly list with an index almost as we do with arrays
        {
            get                                 
            {
                if (index < 0 || index >= count)            //checks the index exist in the list
                    throw new ArgumentOutOfRangeException(nameof(index), $"index {index} is outside 0 .. {count - 1}");  //it is count-1 because the index is always 1 less than the count because it starts at 0

                var trackingNode = head;        //start at the head and move forward the number of times the index is
                for (int i = 0; i < index; i++)
                {
                    trackingNode = trackingNode.Next;          
                }
                return trackingNode.Value;
            }
            set
            {
                if (index < 0 || index >= count)
                    throw new ArgumentOutOfRangeException(nameof(index), $"index {index} is outside 0 .. {count - 1}");

                var trackingNode = head;
                for (int i = 0; i < index; i++)
                {
                    trackingNode = trackingNode.Next;
                }
                trackingNode.Value = value;         //changes the value for the new value
            }

        }

        public void AddFirst(T item)
        {
            var node = new DoublyNode<T>(item); //the new node
            
            if (head  != null)                  //check if  theres a head
            {
                node.Next = head;                //point the new node to the head of the list
                head.Prev = node;                //make the old head point back to the new node
            }
            head = node;                        //make the new node the head of the list

            if (tail == null)                   //in case theres no tail
            {
                tail = node;                    //make the new node the tail also
            }

            count++;
        }

        public void AddLast(T item)             
        {
            var node = new DoublyNode<T>(item);
            if (tail != null)                   //check if there is a tail
            { 
                node.Prev = tail;               //point the new node to the old tail
                tail.Next = node;               //make the old tail point to the new node
            }

            tail = node;                        //make the new node the tail of the list

            if (head == null)                   //if theres no head
            {
                head = node;                    //make the new node is the head also
            }
            count++;
        }


        public void Insert(int index, T item)
        {
            if (index < 0 || index > count)       //check the index is valid
            {                           
                throw new ArgumentOutOfRangeException(nameof(index), $"index {index} is outside 0 .. {count}");       
            }

            if (index == 0)                         //if the index is the head
            {
                AddFirst(item);
                return;
            }

            if (index == count)                     //in case the index is the tail
            {
                AddLast(item);
                return;
            }

            var node = new DoublyNode<T>(item);     //the new node
            var trackingNode = head;                //is going to go through the list starting from the head
            for (int i = 0; i < index; i++)
            {
                trackingNode = trackingNode.Next;   //advance to the next node until you find the index
            }

            node.Next = trackingNode;               //make the new node point to the next one 
            node.Prev = trackingNode.Prev;          //make the new node point to the previous one

            trackingNode.Prev.Next = node;          //make the previous node point to the new one
            trackingNode.Prev = node;               //make the next node point to the new one

            count++;
        }

        public T RemoveFirst()
        {
            if (head == null)                   //in case there is nothing in the list, meaning there is no head, meaning there is nothing to remove
            {
                throw new InvalidOperationException("The list is empty");
            }
            
            var value = head.Value;         //saves the current head value
            head = head.Next;               //the next node becomes the new head

            if (head != null)               //if there is a head
            {
                head.Prev = null;           //make the new head point to nothing
            }
            else
            {
                tail = null;                //if the list is empty, tail is null 
            }
            count--;
            return value;
        }

        public T RemoveLast()
        {
            if (tail == null)           //in case there is no tail, meaning there is nothing to remove
            {
                throw new InvalidOperationException("The list is empty");
            }

            var value = tail.Value;         //saves the current tail value
            tail = tail.Prev;               //the previous node becomes the new tail

            if (tail != null)               //if there is a tail
            {
                tail.Next = null;           //make the new tail point to nothing
            }
            else
            {
                head = null;                //if the list is empty, head is null
            }

            count-- ;
            return value;
        }

        public T RemoveAt(int index)
        {
            if (index < 0 || index >= count)   //check the index is valid
            {
                throw new ArgumentOutOfRangeException(nameof(index), $"index {index} is outside 0 .. {count - 1}");
            }

            if (index == 0)             
            {
                return RemoveFirst();  //call remove head because we would be trying to delete de head
            }

            if (index == count - 1)
            {
                return RemoveLast();    //call remove tail because if the index is 1 less than the total of items in the list means we are trying to remove the tail
            }

            var trackingNode = head;      //goes through the list starting from the head
            for (int i = 0; i < index; i++)
            {
                trackingNode = trackingNode.Next;       //advance to the next node
            }

            var value = trackingNode.Value;             //saves the value of the node we are trying to delete

            trackingNode.Prev.Next = trackingNode.Next;     //make the previous node point to the node after trackingnode
            trackingNode.Next.Prev = trackingNode.Prev;     //make the node after trackingnode point to the previous node of the trackingnode

            count--;
            return value;
        }

        public bool Contains(T item)
        {
            var comparer = EqualityComparer<T>.Default;     //create the comparer

            for (var n = head; n != null; n = n.Next)       
            {
                if (comparer.Equals(n.Value, item))         //compare n.value(the values of the items in the list) with the item we are looking for
                {
                    return true;
                }
            }

            return false;
        }

        public void Clear()
        {
            head = null;        ///the list forgets its first and last node, so it is empty
            tail = null;
            count = 0;
        }

        public IEnumerator<T> GetEnumerator()       //goes through all the list and returns all he values in the list one by one till the end. it can't change values just reads
        {
            for(var n = head; n != null; n = n.Next)
                yield return n.Value;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
