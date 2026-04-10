using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    #region Properties
    public static PoolManager Instance;
    PooledObject originprefab;
    private GameObject parent;
    #endregion

    #region Fields
    // Mapa de pools: Prefab -> Cola de instancias
    public Dictionary<GameObject, Queue<GameObject>> pools = new Dictionary<GameObject, Queue<GameObject>>();
    #endregion

    #region Unity Callbacks
    void Awake()
    {
        //Si ya existe una instancia, destruimos el objeto duplicado
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        //Si no existe, asignamos la instancia actual Y la marcamos para que no se destruya al cambiar de escena
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    #endregion
    #region Public Methods

    //Creamos una pool de objetos vacia
    public void WarmPool(GameObject prefab, int size)
    {
        if (prefab == null || size <= 0) return;
        if (!pools.ContainsKey(prefab))
        {
            // Si no existe la pool para este prefab, la creamos
            pools.Add(prefab, new Queue<GameObject>());
        }
        parent = GameObject.Find("Pool_" + prefab.name);
        if(parent != null)
        {
                // Si ya existe un objeto padre para esta pool, lo destruimos para evitar duplicados
                Debug.Log("Pool para " + prefab.name + " ya existe. Destruyendo el objeto padre existente.");
            parent.transform.SetParent(transform); // Desvincularlo del PoolManager antes de destruirlo
            //Destroy(_game);
        }
        else
        {
            //Creamos un objeto padre para organizar mejor la jerarquia
            parent = new GameObject("Pool_" + prefab.name);
            parent.transform.SetParent(transform);
        }

        //Instanciamos los prefabs y los desactivamos
        for (int i = 0; i < size; i++)
        {
            GameObject obj = Object.Instantiate(prefab, parent.transform);
            obj.SetActive(false);
            // Asegurar que la instancia conoce su prefab original
            originprefab = obj.GetComponent<PooledObject>() ?? obj.AddComponent<PooledObject>();
            originprefab.SetPrefab(prefab);
            //Agregar a la lista el prefab vacio
            pools[prefab].Enqueue(obj);
        }
    }

    //Obtenemos un objeto de la pool y lo activamos
    public GameObject SpawnFromPool(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        if (prefab == null) return null;

        if (!pools.ContainsKey(prefab))
        {
            // lazy create small pool
            WarmPool(prefab, 1);
        }

        GameObject obj;
        var queue = pools[prefab];
        if (queue.Count > 0)
        {
            obj = queue.Dequeue();
            obj.SetActive(true);
            obj.transform.SetPositionAndRotation(position, rotation);
        }
        else
        {
            //Si la pool está llena y se solicita otro objeto, se instancia uno nuevo
            obj = Object.Instantiate(prefab, position, rotation);
            obj.transform.SetParent(transform);
            // Asegurar que la instancia conoce su prefab original
            originprefab = obj.GetComponent<PooledObject>() ?? obj.AddComponent<PooledObject>();
            originprefab.SetPrefab(prefab);
        }

        return obj;
    }
    // Devolvemos el objeto a la pool y lo desactivamos
    public void ReturnToPool(GameObject prefabKey, GameObject instance)
    {
        //Debug.Log("PrefabKey recibido: " + prefabKey);
        if (pools.ContainsKey(prefabKey))
        {
            instance.SetActive(false); // Simplemente lo apagamos
            pools[prefabKey].Enqueue(instance); // Lo metemos de nuevo en la cola
        }
        else
        {
            // Solo aquí destruimos, porque el pool no existe (error de configuración)
            Destroy(instance);
        }
        ;
    }
    #endregion
}

