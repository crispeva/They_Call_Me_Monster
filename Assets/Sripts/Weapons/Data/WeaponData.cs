using UnityEngine;

[CreateAssetMenu(menuName = "Weapons/Weapon Data")]
public class WeaponData : ScriptableObject
{
    #region Properties
    public string weaponName;

    [Header("Combat")]
    public int damage;
    public float attackRate;
    public float range;

    [Header("Projectile")]
    public bool usesProjectile;
    public GameObject projectilePrefab;
    public float projectileSpeed;

    [Header("Scaling")]
    public float damageMultiplierPerLevel = 1.2f;
    #endregion

    #region Fields
    #endregion

    #region Unity Callbacks
    #endregion

    #region Public Methods
    #endregion

    #region Private Methods
    #endregion

}
