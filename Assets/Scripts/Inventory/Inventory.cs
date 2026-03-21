using System;
using System.Collections;
using System.Collections.Generic;
using Controllers;
using Recolectables;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Properties
    public Dictionary<RecolectableType, int> InventoryStack = new Dictionary<RecolectableType, int>();
    public Action OnInventoryUpdated;
    #endregion

    public void AddRecolectable(RecolectableType type, int count)
    {
        if (InventoryStack.ContainsKey(type))
            InventoryStack[type] += count;
        else
            InventoryStack.Add(type, count);
        OnInventoryUpdated?.Invoke();

    }
    public bool UseResource(RecolectableType type, int count = 1)
    {
        if (HasResource(type, count))
        {
            InventoryStack[type] -= count;
            OnInventoryUpdated?.Invoke();
            return true;
        }

        return false;
    }

    public bool HasResource(RecolectableType type, int count = 1)
    {
        if (InventoryStack.ContainsKey(type) && InventoryStack[type] >= count)
            return true;

        return false;
    }

    public  int GetAmount(RecolectableType coin)
    {
       return InventoryStack.ContainsKey(coin) ? InventoryStack[coin] : 0;
    }
}
