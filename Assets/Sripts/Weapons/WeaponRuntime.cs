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

            case WeaponType.Axe:
                FireAxe(owner);
                break;
        }
    }

    void FireDagger(Transform owner, Vector2 aimPosition)
    {
        Vector2 dir = (aimPosition - (Vector2)owner.position).normalized;

        GameObject proj = Object.Instantiate(
            data.weaponPrefab,
            owner.position,
            Quaternion.identity
        );

        var dagger = proj.GetComponent<DaggerProjectile>();
        dagger.speed = data.projectileSpeed;
        dagger.damage = data.damage;
        dagger.Init(owner.gameObject, dir);
    }

    // --- AXE (nueva arma) ---
    void FireAxe(Transform owner)
    {
        //Collider2D[] hits = Physics2D.OverlapCircleAll(
        //    owner.position,
        //    data.meleeRadius
        //);

        //foreach (var hit in hits)
        //{
        //    if (hit.CompareTag("Player")) continue;

        //    IDamageable dmg = hit.GetComponent<IDamageable>();
        //    if (dmg != null)
        //    {
        //        dmg.TakeDamage(data.damage);
        //    }
        //}
        GameObject axeObj = Object.Instantiate(
            data.weaponPrefab,
            owner.position,
            Quaternion.identity
        );

        var axe = axeObj.GetComponent<AxeSwing>();
        axe.Init(owner, data.damage, data.meleeRadius);
    }
    #endregion

}
