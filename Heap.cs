using UnityEngine;
using System.Collections;
using System;

// Array implementation of a heap
public class Heap<T> where T : IHeapItem<T> {

    T[] items;
    int currentItemCount;

    public Heap(int maxHeapSize)
    {
        items = new T[maxHeapSize];
    }

    public void Add(T item)
    {
        item.HeapIndex = currentItemCount;
        items[currentItemCount] = item;
        SortUp(item);
        currentItemCount++;
    }

    /* Removing the item from the top of the heap,
     * and placing the last item in the heap into 
     * the vacated position.
     * @return firstItem - the item at the top of the heap, or the smallest item
     */
    public T RemoveFirst()
    {
        T firstItem = items[0];
        currentItemCount--;
        items[0] = items[currentItemCount];
        items[0].HeapIndex = 0;
        SortDown(items[0]);
        return firstItem;
    }

    /* Changes priority of an item
     * (As an example in pathfinding, if the fCost needs to be updated)
     */
    public void UpdateItem(T item)
    {
        SortUp(item);
    }

    public int Count
    {
        get
        {
            return currentItemCount;
        }
    }

    /* Checks if the heap contains
     * an item of type T
     */
    public bool Contains(T item)
    {
        return Equals(items[item.HeapIndex], item);
    }

    /* After placing an item at the top of the heap, it must be sorted
     * to its appropriate place
     * 
     */
    void SortDown(T item)
    {
        while(true)
        {
            int childIndexLeft = item.HeapIndex * 2 + 1;        // Method for getting the child's left and right children
            int childIndexRight = item.HeapIndex * 2 + 2;
            int swapIndex = 0;

            if(childIndexLeft < currentItemCount)
            {
                swapIndex = childIndexLeft;

                if(childIndexRight < currentItemCount)
                {
                    if(items[childIndexLeft].CompareTo(items[childIndexRight]) < 0)         // If childIndexLeft has a lower priority, or higher value than childIndexRight
                    {
                        swapIndex = childIndexRight;        // swapIndex equals child with highest priority
                    }
                }

                if(item.CompareTo(items[swapIndex]) < 0)
                {
                    Swap(item, items[swapIndex]);
                }
                else // If parent has higher priority than its children
                {
                    return;
                }
            }
            else // If no children
            {
                return;
            }
        }
    }

    /* Sorts items upwards in the heap if they have a higher priority (lower value) than their parent
     * @param item - the generic type item to be sorted
     */
    void SortUp(T item)
    {
        int parentIndex = (item.HeapIndex - 1) / 2;

        // Swapping smaller items with their parents as needed
        while(true)
        {
            T parentItem = items[parentIndex];
            if(item.CompareTo(parentItem) > 0)          // This means if item has a smaller value than parentItem
            {
                Swap(item, parentItem);
            }
            else
            {
                break;
            }
            parentIndex = (item.HeapIndex - 1) / 2;
        }
    }

    // Swaps itemA and itemB and their indices
    void Swap(T itemA, T itemB)
    {
        items[itemA.HeapIndex] = itemB;
        items[itemB.HeapIndex] = itemA;
        int itemAIndex = itemA.HeapIndex;
        itemA.HeapIndex = itemB.HeapIndex;
        itemB.HeapIndex = itemAIndex;
    }
}

// Interface enabling the comparable factor for heaps
public interface IHeapItem<T> : IComparable<T>
{
    int HeapIndex
    {
        get;
        set;
    }
}
