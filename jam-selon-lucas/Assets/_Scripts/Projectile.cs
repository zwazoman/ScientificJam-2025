using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float MaxLifeTime = 5;

    PooledObject _pooledObject;

    public void OnInstantiatedByPool()
    {
        TryGetComponent(out _pooledObject);
    }

    public void OnPulledFromPool()
    {
        StartCoroutine(Life());
    }

    IEnumerator Life()
    {
        yield return new WaitForSeconds(MaxLifeTime);
        _pooledObject.GoBackIntoPool();
    }
}
