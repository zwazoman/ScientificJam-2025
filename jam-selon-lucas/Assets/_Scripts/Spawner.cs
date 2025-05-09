using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] Pools _pool;

    [SerializeField] public float fireRate;
    [SerializeField] public float timeBetweenSalves = 0;
    [SerializeField] public float projectilesPerSalve = 1;

    [SerializeField] bool _spawnsEnnemies;

    [Header("Position")]

    [SerializeField] bool _randomSpawn;
    [SerializeField,Tooltip("JAUNe")] float _minSpawnRange = 0;
    [SerializeField,Tooltip("ROUGE")] float _maxSpawnRange = 0;

    private void Start()
    {
        OnPulledFromPool();
    }

    private async void OnPulledFromPool()
    {
        await Awaitable.NextFrameAsync();
        if(fireRate != 0 || timeBetweenSalves !=0)
            StartCoroutine(PeriodicShoot());
    }

    private void OnDisable()
    {
        StopCoroutine("PeriodicShoot");
    }

    IEnumerator PeriodicShoot()
    {
        for (; ; )
        {
            yield return new WaitForSeconds(timeBetweenSalves - fireRate);

            if(_pool == Pools.waveProjectile) SFXManager.Instance.PlaySFXClipAtPosition(Sounds.MainProj, transform.position, false,false,3);

            for (short i = 0; i < projectilesPerSalve; i++)
            {
                Summon();
                yield return new WaitForSeconds(fireRate);
            }
        }
    }

    public GameObject Summon()
    {
        Vector2 choosenSpawnPos;

        if (_randomSpawn)
            choosenSpawnPos = (Vector2)transform.position + Random.insideUnitCircle.normalized * Random.Range(_minSpawnRange,_maxSpawnRange);
        else
            choosenSpawnPos = transform.position;

        //Debug.Log(_pool);

        if (_spawnsEnnemies)
        {
            JyrosManager.Instance.AddEntity();
        }

        //les sons
        switch (_pool)
        {
            case Pools.DroneBullet:
                SFXManager.Instance.PlaySFXClipAtPosition(Sounds.DroneShoot,transform.position);
                break;
            case Pools.Punch:
                SFXManager.Instance.PlaySFXClipAtPosition(Sounds.Punch,transform.position);
                print("punch");
                break;
            case Pools.huitre:
                SFXManager.Instance.PlaySFXClipAtPosition(Sounds.Oyster,transform.position);
                break;
            case Pools.TowardsMouse:
                SFXManager.Instance.PlaySFXClipAtPosition(Sounds.MainProj,transform.position);
                break;
            case Pools.TargetedMissile:
                SFXManager.Instance.PlaySFXClipAtPosition(Sounds.MainProj, transform.position,false,false,2,1.3f);

                break;
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
