using System.Threading;
using UnityEngine;

public class MyQueue<T>
{
    #region Variables y Nodos
    private QueueNode<T> head;
    private QueueNode<T> tail;
    private int count;
    #endregion
    // -> 0(1)
    #region Enqueue, Dequeue , Peek y Clear
    public void Enqueue(T value)
    {
        QueueNode<T> newNode = new(value);
        count++;

        if (head == null && tail == null)
        {
            head = newNode;
            tail = newNode;
            return;
        }

        tail.SetNext(newNode);
        tail = newNode;
    }

    public T Dequeue()
    {
        if (head == null)
        {
            Clear();
            throw new System.Exception("Queue Empty");
        }

        T value = head.Value;
        head = head.Next;

        count--;
        return value;
    }

    public T Peek()
    {
        if(head == null)
            throw new System.Exception("Queue Empty");
        T value = head.Value;
        return head.Value;
    }

    public void Clear()
    {
        head = null; 
        tail = null; 
        count = 0;
    }
    #endregion

    #region Getters
    public int Count => count;
    #endregion

}
