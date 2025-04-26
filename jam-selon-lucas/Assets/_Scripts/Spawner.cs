using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] Pools _pool;

    /// <summary>
    /// cadence de tir en secondes ( si c'est 0 c'ets pas periodique)
    /// </summary>
    [SerializeField] float _fireRate;

    [SerializeField] bool _spawnsEnnemies;
    

    private void Start()
    {
        StartCoroutine(PeriodicShoot());
    }

    IEnumerator PeriodicShoot()
    {
        while (true)
        {
            Summon();
            yield return new WaitForSeconds(_fireRate);
        }
    }

    public GameObject Summon()
    {
        if (_spawnsEnnemies)
        {
            JyrosManager.Instance.AddEntity();
        }

        return PoolManager.Instance.ChoosePool(_pool).PullObjectFromPool(transform.position);
    }
}
