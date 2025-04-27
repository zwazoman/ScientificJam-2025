using DG.Tweening;
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
    }

    void Die()
    {
        transform.DOScale(0, 2).SetEase(Ease.InBack).onComplete += ReallyDie;
    }

    void ReallyDie()
    {
        JyrosManager.Instance.jyros = null;
        PoolManager.Instance.ChoosePool(Pools.Jyros).PutObjectBackInPool(_poolObject);
    }
}
