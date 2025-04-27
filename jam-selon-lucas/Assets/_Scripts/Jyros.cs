using DG.Tweening;
using System.Threading.Tasks;
using UnityEngine;

public class Jyros : MonoBehaviour
{
    PooledObject _poolObject;

    Vector3 _oldScale;

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
        _oldScale = transform.localScale;
        transform.DOScale(0, 2).SetEase(Ease.InBack).onComplete += ReallyDie;
    }

    void ReallyDie()
    {
        JyrosManager.Instance.jyros = null;
        transform.localScale = _oldScale;
        PoolManager.Instance.ChoosePool(Pools.Jyros).PutObjectBackInPool(_poolObject);
    }
}
