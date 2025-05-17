using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject SpawningTarget;
    [SerializeField] private float spawningTime = 1.0f;
    [SerializeField] private float lifeTime = 1.0f;

    private void Start()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        if (SpawningTarget == null) yield break;

        while (true)
        {
            //GameObject SpawnedObejct = Instantiate(SpawningTarget);
            GameObject SpawnedObejct = ObjectPoolManager.Instance.Get(SpawningTarget, transform.position, Quaternion.identity);

            //Destroy(SpawnedObejct, lifeTime);

            StartCoroutine(RetrieveTarget(SpawnedObejct));

            yield return new WaitForSeconds(spawningTime);
        }
    }

    IEnumerator RetrieveTarget(GameObject obj)
    {
        yield return new WaitForSeconds(spawningTime);

        ObjectPoolManager.Instance.Release(SpawningTarget, obj);
    }
}
