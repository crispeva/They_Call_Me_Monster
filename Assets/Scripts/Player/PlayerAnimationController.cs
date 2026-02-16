using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;

public class PlayerAnima : MonoBehaviour
{
    #region Properties
    #endregion
    [SerializeField] private Animator _goblinAnimator;
    #region Fields
    private WeaponController _weaponController;
    #endregion

    #region Unity Callbacks
    private void Awake()
    {
        _weaponController = GetComponent<WeaponController>();
    }
    void Start()
    {
        _weaponController.OnShoot += () => _goblinAnimator.SetTrigger("Shoot");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    #endregion

    #region Public Methods
    private void Idle() {
        _goblinAnimator.SetBool("isRunning", false);
    }
    #endregion

    #region Private Methods
    #endregion

}
