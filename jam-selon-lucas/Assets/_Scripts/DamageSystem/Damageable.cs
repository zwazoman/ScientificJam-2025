using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    public float hp;

    [SerializeField] bool destroyOnDeath = true;

    public Team team;

    public UnityEvent onDamageTaken, onDeath = new();

    public const float invicibilityDuration = .2f;
    bool CanTakeDamage = true;

    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        OnInstantiatedByPool();
    }

    void OnInstantiatedByPool()
    {
        TryGetComponent(out spriteRenderer);
    }

    public void TakeDamage(float damage)
    {
        if (CanTakeDamage)
        {
            hp -= (float)damage;

            Sequence s = DOTween.Sequence();
            s.Append( spriteRenderer.DOColor(Color.red, .1f));
            s.Append( spriteRenderer.DOColor(Color.white, .1f));
 
            onDamageTaken.Invoke();
            if (hp <= 0)
            {
                Die();
            }
            else
            {
                StartCoroutine(ApplyInvicibility());
            }
        }
    }

    IEnumerator ApplyInvicibility()
    {
        CanTakeDamage = false;
        yield return new WaitForSeconds(invicibilityDuration);
        CanTakeDamage = true;
    }

    public void Die()
    {
        onDeath?.Invoke();
        if (destroyOnDeath)
        {
            if (TryGetComponent(out PooledObject pooledObject))
            {
                JyrosManager.Instance.RemoveEntity();
                pooledObject.GoBackIntoPool();
            }
            else
            {
                Debug.LogError("No pooled object attached");
            }
        }
    }
}
