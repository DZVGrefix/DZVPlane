using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Unity.Collections;
using UnityEngine;


/*
    Pool rendszer
*/
public class ObjectPooler : MonoBehaviour
{
    #region Singelton
    private static ObjectPooler _instance;
    public static ObjectPooler Instance()
    {
        return _instance;
    }
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this);
        }
        else
        {
            _instance = this;
        }
    }
    #endregion

    [Header("Pool settings")]
    [SerializeField] Transform activeParent;                    // ha nincs hova rakni az új objectet akkor ide teszem
    [SerializeField] List<PoolItem> poolListItems;              // Alapból milyen elemeket hozzon létre
    Dictionary<string, Queue<GameObject>> _poolDictionary;      // POOL

    // POOL létrehozás
    void Start()
    {
        _poolDictionary = new Dictionary<string, Queue<GameObject>>();
        CreatePools();
    }
    void CreatePools()
    {
        foreach (PoolItem pool in poolListItems)
        {
            LoadPoolItem(pool);
        }
    }
    void LoadPoolItem(PoolItem poolItem)
    {
        Queue<GameObject> objectPool = new Queue<GameObject>();
        for (int i = 0; i < poolItem.count; i++)
        {
            GameObject obj = Instantiate(poolItem.prefab);
            obj.name = poolItem.prefab.name;
            obj.SetActive(false);
            obj.transform.SetParent(transform);
            objectPool.Enqueue(obj);
        }

        _poolDictionary.Add(poolItem.prefab.name, objectPool);
    }
    void CreatePoolItem(GameObject prefab, Queue<GameObject> objectPool)
    {
        GameObject obj = Instantiate(prefab);
        obj.name = prefab.name;
        obj.SetActive(false);
        obj.transform.SetParent(transform);
        objectPool.Enqueue(obj);
    }
    /**************************************************************************/

    // elem kivétel
    public GameObject SpawnFromPool(string name, Vector3 position, Quaternion rotation, Transform parent)
    {
        if (!_poolDictionary.ContainsKey(name))
        {
            Debug.LogWarning($"Nincs ilyen név {name} a pool-ban");
            return null;
        }
        if (parent == null)
        {
            parent = activeParent;
        }

        if (_poolDictionary[name].Count == 0)
        {
            CreatePoolItem(GetPrefabsWithName(name), _poolDictionary[name]);
        }

        // kivétel a pool-ból
        GameObject objectToSpawn = _poolDictionary[name].Dequeue();
        // alap beállítások
        objectToSpawn.SetActive(true);
        objectToSpawn.transform.SetPositionAndRotation(position, rotation);
        objectToSpawn.transform.SetParent(parent);

        // poolDictionary[name].Enqueue(objectToSpawn);
        return objectToSpawn;
    }

    GameObject GetPrefabsWithName(string name)
    {
        foreach (PoolItem pItem in poolListItems)
        {
            if (pItem.prefab.name == name)
            {
                return pItem.prefab;
            }
        }
        return null;
    }

    // elem visszarakás
    public void ObjectToPool(GameObject obj)
    {
        obj.SetActive(false);
        obj.transform.SetParent(transform);
        _poolDictionary[obj.name].Enqueue(obj);
    }


    // poolItem létrehozás
    public void AddPoolItem(PoolItem poolItem)
    {
        if (!_poolDictionary.ContainsKey(poolItem.prefab.name))
        {
            poolListItems.Add(poolItem);
            LoadPoolItem(poolItem);
        }
    }

    // poolItem tömb alapján létrehozás
    public void AddPoolItems(PoolItem[] poolItems)
    {
        foreach (PoolItem pool in poolItems)
        {
            AddPoolItem(pool);
        }
    }

    // törlöm az összes elemet ami ezen a parenten van
    public void ClearActiveParent()
    {
        int count = activeParent.transform.childCount;
        for (int i = count - 1; i >= 0; i--)
        {
            ObjectToPool(activeParent.transform.GetChild(i).gameObject);
        }
    }
}
