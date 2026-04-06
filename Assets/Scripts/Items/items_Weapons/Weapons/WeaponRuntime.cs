using System.Collections;
using System.Collections.Generic;
using Controllers;
using UnityEngine;
namespace Weapons
{
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
        // usar pool en lugar de Instantiate
        GameObject proj = PoolManager.Instance != null
            ? PoolManager.Instance.SpawnFromPool(data.weaponPrefab, owner.position, Quaternion.Euler(0f, 0f, angle))
            : Object.Instantiate(data.weaponPrefab, owner.position, Quaternion.Euler(0f, 0f, angle));

        PlayAttackSFX();
        Projectile dagger = proj.GetComponent<Projectile>();
        dagger.Init(owner.gameObject, dir);
    }

    //AXE
    void FireAxe(Transform owner, Vector2 aimPosition)
    {

        GameObject axeObj = Object.Instantiate(
            data.weaponPrefab,
            owner.position,
            Quaternion.identity
        );
        PlayAttackSFX();
        var axe = axeObj.GetComponent<AxeSwing>();
        axe.Init(owner, data.damage, data.meleeRadius, aimPosition);
    }
    void PlayAttackSFX()
    {
        AudioManager.Instance?.PlaySFX(
            data.attackSFX,
           data.pitchVariation
        );
    }
    #endregion

}
}
