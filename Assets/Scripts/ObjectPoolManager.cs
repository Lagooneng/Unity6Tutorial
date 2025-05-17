using UnityEngine;
using UnityEngine.Pool;
using System.Collections.Generic;

[System.Serializable]
struct PoolTargetStruct
{
    public GameObject prefab;
    public int poolSize;
    public int maxPoolSize;
}

public class ObjectPoolManager : MonoBehaviour
{
    public static ObjectPoolManager Instance { get; private set; }

    [SerializeField] private List<PoolTargetStruct> _poolTargetStructList;

    private Dictionary<GameObject, ObjectPool<GameObject>> _poolDict;

    private void Awake()
    {
        if( Instance == null )
        {
            Instance = this;
            DontDestroyOnLoad( Instance );
        }
        else
        {
            Destroy(gameObject);
        }

        _poolDict = new Dictionary<GameObject, ObjectPool<GameObject>>();
    }

    private void Start()
    {
        foreach (PoolTargetStruct poolTargetStruct in _poolTargetStructList)
        {
            InitializePool(poolTargetStruct.prefab, poolTargetStruct.poolSize, poolTargetStruct.maxPoolSize);
        }
    }

    public void InitializePool(GameObject prefab, int size, int maxSize)
    {
        if (_poolDict.ContainsKey(prefab)) return;

        ObjectPool<GameObject> pool = new ObjectPool<GameObject>(
                createFunc: () => Instantiate(prefab),
                actionOnGet: (obj) => obj.SetActive(true),
                actionOnRelease: (obj) => obj.SetActive(false),
                collectionCheck: false,
                defaultCapacity: size,
                maxSize: maxSize
            );

        _poolDict.Add(prefab, pool);
        Debug.Log("ObjectPoolManager - InitializePool");
    }

    public GameObject Get(GameObject prefab, Vector3 pos, Quaternion rotation = default)
    {
        if (_poolDict.TryGetValue(prefab, out var pool))
        {
            GameObject obj = pool.Get();
            obj.transform.position = pos;
            obj.transform.rotation = rotation;

            Debug.Log("ObjectPoolManager - Get");

            return obj;
        }

        return null;
    }

    public void Release(GameObject prefab, GameObject TargetObject)
    {
        if (_poolDict.TryGetValue(prefab, out var pool))
        {
            pool.Release(TargetObject);
            Debug.Log("ObjectPoolManager - Release");
        }
    }
}
