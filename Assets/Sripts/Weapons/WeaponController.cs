using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;
[RequireComponent(typeof(InputController))]
public class WeaponController : MonoBehaviour
{
    #region Properties
    #endregion

    #region Fields
    //Refact porque tiene caracteristicas solo de disparo de la daga
    InputController input;
    [SerializeField] GameObject daggerPrefab;
    [SerializeField] float fireRate = 3f;
    [SerializeField] int rotationOffset = -90;
    #endregion

    #region Unity Callbacks

    // Update is called once per frame
    float lastFireTime;

    void Awake()
    {
        input = GetComponent<InputController>();
    }

    void Update()
    {
        if (!input.FirePressed) return;
        if (!CanShoot()) return;

        Shoot(input.AimPosition);
    }

    bool CanShoot()
    {
        return Time.time >= lastFireTime + (1f / fireRate);
    }

    void Shoot(Vector2 aimPosition)
    {
        lastFireTime = Time.time;

        Vector2 dir = (aimPosition - (Vector2)transform.position).normalized;
        float baseAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        float angle = baseAngle + rotationOffset;// Si la punta de la sprite apunta hacia arriba, probar -90 o +90
        GameObject dagger = Instantiate(
            daggerPrefab,
            transform.position,
            Quaternion.Euler(0f, 0f, angle)
        );

        dagger.GetComponent<DaggerProjectile>().Init(gameObject, dir);
    }
    #endregion

}
