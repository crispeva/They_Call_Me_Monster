using UnityEngine;

public enum WeaponType
{
    Dagger,
    Axe,
    Arrow_enemy
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

    [Header("Audio")]
    public AudioClip attackSFX;
    [Range(0f, 0.5f)] public float pitchVariation = 0.1f;

    [Header("Projectile (solo si aplica)")]
    public bool usesProjectile;
    public float projectileSpeed;
    public int chargerSize;
    [Header("Melee (solo si aplica)")]
    public float meleeRadius = 1.5f;
    [Header("Scaling")]
    public float damageMultiplierPerLevel = 1.2f;
    public float money = 20;
    #endregion

}
