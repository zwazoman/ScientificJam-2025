using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] Pools _pool;

    /// <summary>
    /// cadence de tir en secondes ( si c'est 0 c'ets pas periodique)
    /// </summary>
    [SerializeField] float _fireRate;
    [SerializeField] float TimeBetweenSalves = 0;
    [SerializeField] short projectilesPerSalve = 1;

    [SerializeField] bool _spawnsEnnemies;

    [Header("Position")]

    [SerializeField] float _maxSpawnRange = 0;

    private void Start()
    {
        //if(_fireRate != 0)
        StartCoroutine(PeriodicShoot());
    }

    IEnumerator PeriodicShoot()
    {
        for (; ; )
        {
            for(short i = 0; i < projectilesPerSalve; i++)
            {
                Summon();
                yield return new WaitForSeconds(_fireRate);
            }
            yield return new WaitForSeconds(TimeBetweenSalves - _fireRate);
        }
    }

    public GameObject Summon()
    {
        if (_spawnsEnnemies)
        {
            JyrosManager.Instance.AddEntity();
        }

        Vector2 choosenSpawnPos;

        if (_maxSpawnRange > 0)
            choosenSpawnPos = Random.insideUnitCircle * _maxSpawnRange;
        else
            choosenSpawnPos = transform.position;

        return PoolManager.Instance.ChoosePool(_pool).PullObjectFromPool(transform.position);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, _maxSpawnRange);   
    }
}
