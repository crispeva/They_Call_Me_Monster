using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;
[RequireComponent(typeof(InputController))]
public class WeaponController : MonoBehaviour
{
    #region Properties
    #endregion

    #region Fields
    //Refact porque tiene caracteristicas solo de disparo de la daga
    public WeaponData startingWeapon;
    InputController input;
    WeaponRuntime currentWeapon;
    #endregion

    #region Unity Callbacks

    // Update is called once per frame

    void Awake()
    {
        input = GetComponent<InputController>();
        currentWeapon = new WeaponRuntime(startingWeapon);
    }

    void Update()
    {
        if (!input.FirePressed) return;

        currentWeapon.Fire(transform, input.AimPosition);
    }

    public void EquipWeapon(WeaponData newWeapon)
    {
        currentWeapon = new WeaponRuntime(newWeapon);
    }
    #endregion

}
