using System;
using System.Collections;
using System.Collections.Generic;
using Controllers;
using Enemies;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;
namespace Recolectables
{
    public enum RecolectableType
{
    Coin,
    Exp
}
    public class Recolectable : MonoBehaviour
    {
        [field: SerializeField] public int Count { get; set; }
        [field: SerializeField] public RecolectableType Type { get; set; }
        PooledObject pooledObject;
        GameObject originPrefab;
        AudioSource _audio;
        private void Start()
        {
            _audio = GetComponent<AudioSource>();
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.CompareTag("Player")) return;

            Inventory inventory = collision.GetComponent<Inventory>();

            if (inventory != null)
            {
                inventory.AddRecolectable(Type, Count);
                AudioManager.Instance?.PlaySFX(_audio.clip,2);
            }

            ReturnToPool();
        }

        private void ReturnToPool()
        {
            pooledObject = GetComponent<PooledObject>(); 
            if (pooledObject != null) { 
                originPrefab = pooledObject.Prefab; 
                PoolManager.Instance?.ReturnToPool(originPrefab, gameObject); 
            }
        }
    }
}

