using System;

namespace ADS.A1
{
    public class RingBufferQueue<T>
    {
        private T[] items;
        private int head;           
        private int count;
        private readonly bool canGrow;              //we use it the check if can make the array bigger when full

        public RingBufferQueue(int capacity, bool canGrow = false)
        {
            if (capacity < 1) {                     //check the capacity is valid
                throw new ArgumentOutOfRangeException(nameof(capacity), "Capacity must be at least 1." );
            }

            items = new T[capacity];
            this.canGrow = canGrow;
        }

        public int Count => count;                   //how many items are in the queue
            
        public int Capacity => items.Length;         //the capacity of the queue

        public bool IsFull => count == Capacity;     //is true when the queue is full


        public void Enqueue(T item)
        {
            if (count == Capacity)
            {
                if (canGrow)
                {
                    Grow();
                }
                else {
                    throw new InvalidOperationException("the queue is full");
                }
            } 
                        
            int index=(head+count)%Capacity;        //find the last index, this formula gives us the index 
            items[index] = item;                    //add the new item in the last index
            count++;                                //increase the number of items
        }

        public T Dequeue()
        {
            if (count == 0)
            {
                throw new InvalidOperationException("the queue is empty");
            }

            T value = items[head];                  //we can only remove at the first element so we find the head of the queue. We save it in value
            head = (head + 1) % Capacity;           //move the head forward
            count--;                                //decrease the numer of items 

            return value;                           //this the item we removed

        }

        public T Peek()
        {
            if (count == 0)
            {
                throw new InvalidOperationException("the queue is empty");
            }

            return items[head];                     //just look at the item in the head
            
        }

        private void Grow()
        {
            T[] bigger = new T[items.Length * 2];            //a new array two times bigger than the current one
            for (int i = 0; i < count; i++) 
            {
                bigger[i] = items[(head + i) % Capacity];   //starting form the head get the items
            }
            items = bigger;                                 // replace the old array with the new one
            head = 0;                                       //reset the head to 0 so when we dequeue or peek or enqueue we are grabbing the right item
        }
    }
}
