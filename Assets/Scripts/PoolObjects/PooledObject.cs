using UnityEngine;
using System.Collections.Generic;
public class PooledObject : MonoBehaviour
{
    #region Properties
    public GameObject Prefab { get; private set; }
    #endregion

    #region Public Methods
    public void SetPrefab(GameObject prefab)
    {
        Prefab = prefab;
    }
    #endregion
}