using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;
[CreateAssetMenu(menuName = "Items/Consumable")]
public class Consumable : Items
{
    public int healAmount;

    public override void Use(GameObject user)
    {
        HealthSystem controller = user.GetComponent<HealthSystem>();
        controller.Heal(healAmount);
    }
}
