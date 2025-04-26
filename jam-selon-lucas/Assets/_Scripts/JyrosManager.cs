using System;
using UnityEngine;

public class JyrosManager : MonoBehaviour
{
    public event Action OnJyrosUnSummon;

    #region Singleton
    private static JyrosManager instance;

    public static JyrosManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = new GameObject("Jyros Manager");
                instance = go.AddComponent<JyrosManager>();
            }
            return instance;
        }
    }
    #endregion

    [SerializeField] public int jyrosSummonThreshold;
    [SerializeField] public int jyrosUnSummonThreshold;

    [HideInInspector] public int EntityCpt;

    Spawner _spawner;

    [HideInInspector] public Jyros jyros;

    private void Awake()
    {
        _spawner = GetComponent<Spawner>();
        instance = this;
    }

    public void AddEntity(int value = 1)
    {
        EntityCpt += value;

        if (EntityCpt >= jyrosSummonThreshold && jyros == null)
        {
            SummonJyros();
        }
    }

    public void RemoveEntity(int value = 1)
    {
        EntityCpt -= value;

        if(EntityCpt <=  jyrosUnSummonThreshold && jyros != null)
        {
            OnJyrosUnSummon?.Invoke();
        }
    }

    void SummonJyros()
    {
        jyros = _spawner.Summon().GetComponent<Jyros>();
    }
}
