using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRuntime : MonoBehaviour
{
    #region Properties
    WeaponData data;
    float lastFireTime;
    [SerializeField] int rotationOffset = -90;
    #endregion

    #region Fields
    #endregion

    #region Unity Callbacks
    #endregion

    #region Public Methods
    public WeaponRuntime(WeaponData data)
    {
        this.data = data;
    }
    public bool CanFire()
    {
        Debug.Log("CanFire check for weapon: " + data.weaponType);
        return Time.time >= lastFireTime + (1f / data.fireRate);
    }
    #endregion

    #region Private Methods

    public void Fire(Transform owner, Vector2 aimPosition)
    {
        if (!CanFire()) return;

        lastFireTime = Time.time;
        Debug.Log("Firing weapon: " + data.weaponType);
        switch (data.weaponType)
        {
            case WeaponType.Dagger:
                FireDagger(owner, aimPosition);
                break;

            case WeaponType.Axe:
                FireAxe(owner, aimPosition);
                break;
        }
    }

    void FireDagger(Transform owner, Vector2 aimPosition)
    {
        Vector2 dir = (aimPosition - (Vector2)owner.position).normalized;
        float baseAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        float angle = baseAngle + rotationOffset;
        GameObject proj = Object.Instantiate(
            data.weaponPrefab,
            owner.position,
            Quaternion.Euler(0f,0f,angle)
        );

        var dagger = proj.GetComponent<DaggerProjectile>();
        dagger.speed = data.projectileSpeed;
        dagger.damage = data.damage;
        dagger.Init(owner.gameObject, dir);
    }

    // --- AXE (nueva arma) ---
    void FireAxe(Transform owner, Vector2 aimPosition)
    {

        GameObject axeObj = Object.Instantiate(
            data.weaponPrefab,
            owner.position,
            Quaternion.identity
        );

        var axe = axeObj.GetComponent<AxeSwing>();
        axe.Init(owner, data.damage, data.meleeRadius, aimPosition);
    }
    #endregion

}
