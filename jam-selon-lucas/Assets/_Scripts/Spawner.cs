using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject _prefab;

    /// <summary>
    /// cadence de tir en secondes ( si c'est 0 c'ets pas periodique)
    /// </summary>
    [SerializeField] float _fireRate;
    

    private void Start()
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
        Instantiate(_prefab);
    }
}
