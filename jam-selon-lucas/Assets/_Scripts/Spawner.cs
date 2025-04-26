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

    [SerializeField] bool _randomSpawn;
    [SerializeField,Tooltip("JAUNe")] float _minSpawnRange = 0;
    [SerializeField,Tooltip("ROUGE")] float _maxSpawnRange = 0;

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
        Vector2 choosenSpawnPos;

        if (_randomSpawn)
            choosenSpawnPos = Random.insideUnitCircle.normalized * Random.Range(_minSpawnRange,_maxSpawnRange);
        else
            choosenSpawnPos = transform.position;

        if (_spawnsEnnemies)
        {
            JyrosManager.Instance.AddEntity();
        }

        return PoolManager.Instance.ChoosePool(_pool).PullObjectFromPool(choosenSpawnPos);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _minSpawnRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _maxSpawnRange);   
    }
}
