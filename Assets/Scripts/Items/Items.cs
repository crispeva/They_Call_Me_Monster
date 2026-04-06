using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Items : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public int price;
    public string description;
    public abstract void Use(GameObject user);
}
