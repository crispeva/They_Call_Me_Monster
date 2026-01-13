using UnityEngine;

public enum WeaponType
{
    Dagger,
    Axe
}
[CreateAssetMenu(menuName = "Weapons/Weapon Data")]
public class WeaponData : ScriptableObject
{
    #region Properties
    public string weaponName;
    public WeaponType weaponType;
    public GameObject weaponPrefab;
    [Header("Combat")]
    public int damage;
    public float fireRate;
    public float range;

    [Header("Projectile (solo si aplica)")]
    public bool usesProjectile;
    public float projectileSpeed;
    [Header("Melee (solo si aplica)")]
    public float meleeRadius = 1.5f;
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
