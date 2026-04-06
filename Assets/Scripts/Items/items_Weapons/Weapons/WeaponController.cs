using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;
namespace Weapons { 
[RequireComponent(typeof(InputController))]
public class WeaponController : MonoBehaviour
{
    #region Properties
    #endregion

    #region Fields
    //Refact porque tiene caracteristicas solo de disparo de la daga
    [SerializeField] private WeaponData startingWeapon;
    InputController input;
    WeaponRuntime currentWeapon;
    public event Action OnShoot;
        #endregion

        #region Unity Callbacks

        // Update is called once per frame

        void Awake()
    {
        input = GetComponent<InputController>();
        currentWeapon = new WeaponRuntime(startingWeapon);

        // precargar pool si el arma usa proyectiles
        if (startingWeapon != null && startingWeapon.usesProjectile)
        {
            
            if (PoolManager.Instance != null)
            {
                PoolManager.Instance.WarmPool(startingWeapon.weaponPrefab, startingWeapon.chargerSize);
            }
            else
            {
                // crear instancia del PoolManager en escena si no existe
                var go = new GameObject("PoolManager");
                go.AddComponent<PoolManager>();
                PoolManager.Instance.WarmPool(startingWeapon.weaponPrefab, startingWeapon.chargerSize);
            }
        }
    }

    void Update()
    {
        if (!input.FirePressed) return;

        currentWeapon.Fire(transform, input.AimPosition);
            OnShoot?.Invoke();
        }

    public void EquipWeapon(WeaponData newWeapon)
    {
        currentWeapon = new WeaponRuntime(newWeapon);
        if (newWeapon != null && newWeapon.usesProjectile)
        {
            PoolManager.Instance?.WarmPool(newWeapon.weaponPrefab, 10);
        }
    }
    #endregion

}
}
