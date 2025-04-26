using UnityEngine;

public class Jyros : MonoBehaviour
{
    PooledObject _poolObject;

    public void OnInstantiatedByPool()
    {
        TryGetComponent(out  _poolObject);
    }

    private void Start()
    {
        JyrosManager.Instance.OnJyrosUnSummon += Die;
        SFXManager.Instance.PlaySFXClip(Sounds.Jyros);
    }

    void Die()
    {
        JyrosManager.Instance.jyros = null;
        PoolManager.Instance.ChoosePool(Pools.Jyros).PutObjectBackInPool(_poolObject);
    }
}
