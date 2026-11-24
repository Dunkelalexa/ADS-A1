using System;

namespace ADS.A1
{
    public class SimpleStack<T>
    {
        private const int StartingCapacity = 6;

        private T[] items;                //saves the items
        private int count;                // how many items are actually stored

        public SimpleStack()              //constructor
        {
            items = new T[StartingCapacity]; 
            count = 0;
        }

        public int Count => count;        //returns what the private variable count is

        public void Push(T item)
        {
            if (count == items.Length)      
            {
                int newCapacity = items.Length == 0 ? StartingCapacity : items.Length * 2;          //double the size of the stack
                T[] newItems = new T[newCapacity];

                for (int i = 0; i < count; i++)
                {
                    newItems[i] = items[i];                 // copy the current items in the stack in the same order
                }

                items = newItems;                           //save the new stack
            }

            items[count] = item;                            //add the new item at the top of the stack
            count++;                                        //increase the count
        }

        public T Pop()
        {
            if (count == 0)
            {
                throw new InvalidOperationException("The stack is empty");
            }

            T item = items[count -1];           //save the top item before we clear it
            items[count - 1] = default;         //clear the top slot
            count--;
            return item;
        }

        public T Peek()
        {
            if (count == 0)
            {
                throw new InvalidOperationException("The stack is empty");
            }

            return items[count - 1];            //find the top item of the stack and just look at it
        }
    }
}
