using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerySimplePool<T> where T : MonoBehaviour
{
    private int _amountToCreateAtGrow = 10;

    public Queue<T> Queue;

    public delegate T PoolDelegateReturnT();
    private PoolDelegateReturnT _TCreator;

    public VerySimplePool(PoolDelegateReturnT TCreator, int amountToCreate)
    {
        Queue = new Queue<T>(1000);
        _TCreator = TCreator;
        Grow(amountToCreate);
    }

    public T Pop()
    {
        if (Queue.Count == 0)
        {
            Grow();
        }

        var item = Queue.Dequeue();
        item.gameObject.SetActive(true);
        return item;
    }

    public void Push(T item)
    {
        item.transform.parent = null;
        item.gameObject.SetActive(false);
        Queue.Enqueue(item);
    }

    public void Grow(int? amountToCreateAtGrow = null)
    {
        for (int i = 0; i < (amountToCreateAtGrow ?? _amountToCreateAtGrow); i++)
        {
            Push(_TCreator());
        }
    }

}
