using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRuntime : MonoBehaviour
{
    #region Properties
    WeaponData data;
    float lastFireTime;
    #endregion

    #region Fields
    #endregion

    #region Unity Callbacks
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    #endregion

    #region Public Methods
    public WeaponRuntime(WeaponData data)
    {
        this.data = data;
    }
    public bool CanFire()
    {
        return Time.time >= lastFireTime + (1f / data.fireRate);
    }
    #endregion

    #region Private Methods

    public void Fire(Transform owner, Vector2 aimPosition)
    {
        if (!CanFire()) return;

        lastFireTime = Time.time;

        switch (data.weaponType)
        {
            case WeaponType.Dagger:
                FireDagger(owner, aimPosition);
                break;

                // luego:
                // case WeaponType.Bow:
                // case WeaponType.Staff:
        }
    }

    void FireDagger(Transform owner, Vector2 aimPosition)
    {
        Vector2 dir = (aimPosition - (Vector2)owner.position).normalized;

        GameObject proj = Object.Instantiate(
            data.projectilePrefab,
            owner.position,
            Quaternion.identity
        );

        var dagger = proj.GetComponent<DaggerProjectile>();
        dagger.speed = data.projectileSpeed;
        dagger.damage = data.damage;
        dagger.Init(owner.gameObject, dir);
    }
    #endregion

}
