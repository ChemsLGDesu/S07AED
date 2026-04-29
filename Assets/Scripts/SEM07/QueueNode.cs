using UnityEngine;

public class QueueNode<T>
{
    #region Propperties
    private T value;
    private QueueNode<T> next;
    #endregion

    #region Methods

    public QueueNode(T value)
    {
        this.value = value;
        this.next = null;
    }

    public void SetNext(QueueNode<T> next)
    {
        this.next = next;
    }
    #endregion
    #region Getters
    public T Value => value;
    public QueueNode<T> Next => next;
    #endregion
}
