using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(InputController))]
public class WeaponController : MonoBehaviour
{
    #region Properties
    InputController input;
    List<WeaponInstance> weapons = new();
    #endregion

    #region Fields
    #endregion

    #region Unity Callbacks
    void Awake()
    {
        input = GetComponent<InputController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!input.FirePressed) return;

        foreach (var weapon in weapons)
        {
            //weapon.TryAttack(transform, input.AimPosition);
        }
    }
    #endregion

    #region Public Methods
    #endregion

    #region Private Methods
    private void AddWeapon(WeaponData data)
    {
       // weapons.Add(new WeaponInstance(data));
    }
    #endregion

}
