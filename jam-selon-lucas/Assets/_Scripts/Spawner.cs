using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] Pools _pool;

    /// <summary>
    /// cadence de tir en secondes ( si c'est 0 c'ets pas periodique)
    /// </summary>
    [SerializeField] float _fireRate;

    private void Start()
    {
        OnPulledFromPool();
    }

    private void OnPulledFromPool()
    {
        StartCoroutine(PeriodicShoot());
    }

    IEnumerator PeriodicShoot()
    {
        while (true)
        {
            Shoot();
            yield return new WaitForSeconds(_fireRate);
        }
    }

    public void Shoot()
    {
        PoolManager.Instance.ChoosePool(_pool).PullObjectFromPool(transform.position);
    }
}
