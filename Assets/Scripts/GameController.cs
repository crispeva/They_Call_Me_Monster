 using UnityEngine;
public class GameController : MonoBehaviour
{
    #region Properties
    public static GameController Instance { get; private set; }
    public WeaponController WeaponController => _weaponcontroller;
    #endregion

    #region Fields
    [SerializeField] protected  WeaponController _weaponcontroller;
    #endregion

    #region Unity Callbacks
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    #endregion

    #region Public Methods
    #endregion

    #region Private Methods
    #endregion
}