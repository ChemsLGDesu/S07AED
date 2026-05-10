using System;
using System.Collections.Generic;
using UnityEngine;

public class PriorityQueue<T>
{
    #region Properties/Privates
    private QueueNode<T> head;
    //private QueueNode<T> tail;
    private int count;

    private Func<T, T, bool> hasHigherPriority;
    #endregion

    public PriorityQueue(Func<T, T, bool> rule)
    {
        hasHigherPriority = rule;
    }

    #region Public Methods
    //-> O(1)
    public void Enqueue(T value)//->O(1) O(n)
    {
        QueueNode<T> newNode = new(value);
        count++;

        if (head == null)
        {
            head = newNode;
            return;
        }
        //-> [10]
        //-> [15][10]     
        //-> [15][10][2] 
        //-> [15][10][2][1] 
        //-> [15][10][4][2][2] [1] [1] [1] [1] [1] [1] [1] [1] [1] 
        //->O1
        if (hasHigherPriority(value, head.Value))
        {
            newNode.SetNext(head);
            head = newNode;
            return;
        }
        QueueNode<T> evaluator = head;

        while (evaluator.Next != null && !hasHigherPriority(value, evaluator.Next.Value))
        {
            evaluator = evaluator.Next;
        }

        newNode.SetNext(evaluator.Next);
        evaluator.SetNext(newNode);

        /*tail.SetNext(newNode);
        tail = newNode;*/
    }

    public void Comparation(Func<T, T, bool> newRule)
    {
        hasHigherPriority = newRule;
        Rebuild();
    }

    private void Rebuild()
    {
        var elements = ToList();
        Clear();

        foreach (var e in elements)
        {
            Enqueue(e);
        }
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
        if (head == null)
        {
            Clear();
            throw new System.Exception("Queue Empty");
        }


        return head.Value;
    }
    public List<T> ToList()
    {
        var list = new List<T>();
        var current = head;

        while (current != null)
        {
            list.Add(current.Value);
            current = current.Next;
        }

        return list;
    }
    public void Clear()
    {
        head = null;
        count = 0;
    }
    #endregion

    #region Getters
    public int Count => count;
    #endregion
}
