using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CInventoryList<T>
{
    private T _item;
    public T item
    {
        get { return _item; }
        set { _item = value; }
    }
    public CInventoryList()
    {
    }

    public void SetItem(T newItem)
    {
        _item = newItem;
    }
}
