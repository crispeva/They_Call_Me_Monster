using System.Collections;
using System.Collections.Generic;
using Controllers;
using UnityEngine;
namespace Enemies
{
    public class EnemyMovement : MonoBehaviour
{
    #region Properties
    [SerializeField] private EnemyData data;
     private Transform target;

    #endregion

    #region Fields
    #endregion

    #region Unity Callbacks
    void Start()
    {
        target = GameController.Instance.WeaponController.transform;
    }

    // Update is called once per frame
    void Update()
    {
      //if (GameController.Instance.CurrentState != GameState.Playing)
      //      return;

        Vector2 dir = (target.position - transform.position).normalized;
        transform.position += (Vector3)dir * data.moveSpeed * Time.deltaTime;
    }
#endregion

    #region Public Methods
    #endregion

    #region Private Methods
    #endregion
  
}
}
