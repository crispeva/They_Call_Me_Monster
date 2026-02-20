using System;
using UnityEngine;
using Weapons;

public class PlayerAnimationController : MonoBehaviour
{
    #region Fields
    [SerializeField] private Animator _goblinAnimator;
    [SerializeField] private float _movementThreshold = 0.1f;
    
    private WeaponController _weaponController;
    private InputController _inputController;
    private float _currentMovement;
    #endregion

    #region Unity Callbacks
    private void Awake()
    {
        _weaponController = GetComponent<WeaponController>();
        _inputController = GetComponent<InputController>();
    }

    private void Start()
    {
        _weaponController.OnShoot += PlayShootAnimation;
        _inputController.OnMovement += UpdateMovement;
    }

    private void Update()
    {
        UpdateAnimationState();
    }

    private void OnDestroy()
    {
        if (_weaponController != null)
            _weaponController.OnShoot -= PlayShootAnimation;
        if (_inputController != null)
            _inputController.OnMovement -= UpdateMovement;
    }
    #endregion

    #region Private Methods
    private void UpdateMovement(float movementMagnitude)
    {
        _currentMovement = movementMagnitude;
    }

    private void UpdateAnimationState()
    {
        bool isMoving = _currentMovement > _movementThreshold;
        _goblinAnimator.SetBool("isRunning", isMoving);
    }

    private void PlayShootAnimation()
    {
        _goblinAnimator.SetTrigger("Shoot");
    }
    #endregion
}
