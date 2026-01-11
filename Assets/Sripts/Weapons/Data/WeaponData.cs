using UnityEngine;

public enum WeaponType
{
    Dagger,
    Bow,
    Staff
}
[CreateAssetMenu(menuName = "Weapons/Weapon Data")]
public class WeaponData : ScriptableObject
{
    #region Properties
    public string weaponName;
    public WeaponType weaponType;

    [Header("Combat")]
    public int damage;
    public float fireRate;
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
