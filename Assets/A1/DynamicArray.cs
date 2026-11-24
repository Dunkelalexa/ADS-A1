using System;
using System.Collections;
using System.Collections.Generic;

namespace ADS.A1
{
    public class DynamicArray<T> : IEnumerable<T>
    {
        private const int StartingCapacity = 4;   

        private T[] items;                        
        private int count;                        // how many items are actually stored

        public DynamicArray()                      //constructor
        {
            items = new T[StartingCapacity];
            count = 0;
        }

        public int Count => count;

        public int Capacity => items.Length;

        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= count)
                {
                    throw new ArgumentOutOfRangeException(nameof(index), $"index {index} is outside 0 .. {count - 1}");
                }
                return items[index];
            }
            set
            {
                if (index < 0 || index >= count)
                {
                    throw new ArgumentOutOfRangeException(nameof(index), $"index {index} is outside 0 .. {count - 1}");
                }
                items[index] = value;
            }
        }

        public void Add(T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            if (count == items.Length)            // no more capacity
            {
                Grow();
            }

            items[count] = item;
            count++;
        }

        public void Insert(int index, T item)
        {
            if (index < 0 || index > count)
            {
                throw new ArgumentOutOfRangeException(nameof(index), $"index {index} is outside 0 .. {count}");
            }

            if (count == items.Length)
            {
                Grow();
            }

            for (int i = count; i > index; i--)
            {
                items[i] = items[i - 1];          // shift to the right
            }

            items[index] = item;
            count++;
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index >= count)
            {
                throw new ArgumentOutOfRangeException(nameof(index), $"index {index} is outside 0 .. {count - 1}");
            }

            for (int i = index; i < count - 1; i++)
            {
                items[i] = items[i + 1];          // shift to the left
            }

            count--;
            items[count] = default;               // clear the last slot
        }

        public void Clear()
        {
            for (int i = 0; i < count; i++)
            {
                items[i] = default;
            }
            count = 0;
        }

        public bool Contains(T item)
        {
            EqualityComparer<T> comparer = EqualityComparer<T>.Default;
            for (int i = 0; i < count; i++)
            {
                if (comparer.Equals(items[i], item))
                    return true;
            }
            return false;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < count; i++)
            {
                yield return items[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private void Grow()
        {
            T[] bigger = new T[items.Length * 2];
            for (int i = 0; i < count; i++)
            {
                bigger[i] = items[i];
            }

            items = bigger;
        }
    }
}