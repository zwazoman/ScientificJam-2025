using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Pools
{
    TowardsMouse,
    Jyros,
    Owen,
    Drone,
    DroneBullet,
    Fourmie,
    Timeline,
    huitre,
    explosionVFX,
    beans,
    Senegal,
    Ant,
    Punch,
    waveProjectile
}

public class PoolManager : MonoBehaviour
{
    //singleton
    private static PoolManager instance;

    public static PoolManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = new GameObject("Pool Manager");
                instance = go.AddComponent<PoolManager>();
            }
            return instance;
        }
    }

    private void Awake()
    {
        instance = this;
    }

    [SerializeField] List<Pool> pools = new();

    public Pool ChoosePool(Pools pool)
    {
        return pools[(int)pool];
    }
}
