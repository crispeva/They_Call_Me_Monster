using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Enemies
{
    public class Paladin_Action : EnemyController
{
        #region Properties
        #endregion

        #region Fields
        #endregion

        #region Unity Callbacks
        void Awake()
    {
        base.Awake();
    }

    // Update is called once per frame
    void Update()
    {
            base.Update();
     }
        private void Start()
        {
            base.Start();
        }
        #endregion

        #region Movement
        protected override void EnemyMovement()
        {
            if (Vector2.Distance(transform.position, _target.position) > _enemyData.attackRange)
            {
                // Movimiento hacia el jugador
                base.EnemyMovement();

                //Animations
                Walk_anim(true);

            }
            else
            {
                Walk_anim(false);
            }
        }

        #endregion



    }
}
