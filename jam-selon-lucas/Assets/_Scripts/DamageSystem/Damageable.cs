using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    public float hp;

    [SerializeField] bool destroyOnDeath = true;

    public Team team;

    [SerializeField] float _xpGains;

    public UnityEvent onDamageTaken, onDeath = new();

    public const float invicibilityDuration = .2f;
    bool CanTakeDamage = true;

    [SerializeField] SpriteRenderer spriteRenderer;

    [SerializeField] bool spawnThingOnDeath = true;
    [SerializeField] Pools DeathObject = Pools.explosionVFX;

    Vector3 baseScale;
    bool _isDead;

    private void Awake()
    {
        OnInstantiatedByPool();
    }

    void OnInstantiatedByPool()
    {
        _isDead = false;
        if (spriteRenderer==null)TryGetComponent(out spriteRenderer);
        baseScale = transform.localScale;
    }

    public void TakeDamage(float damage)
    {
        if (CanTakeDamage)
        {
            hp -= (float)damage;

            Sequence s = DOTween.Sequence();
            s.Append( spriteRenderer.DOColor(Color.red, invicibilityDuration*.5f));
            s.Append( spriteRenderer.DOColor(Color.white, invicibilityDuration*.5f));

            Sequence s2 = DOTween.Sequence();
            s2.Append(spriteRenderer.transform.DOScale(baseScale * 1.5f,invicibilityDuration*.5f));
            s2.Append(spriteRenderer.transform.DOScale(baseScale * 1f,invicibilityDuration*.5f));

            onDamageTaken?.Invoke();

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

    public async void Die()
    {
        if (_isDead) return;
        _isDead = true;

        await Awaitable.WaitForSecondsAsync(invicibilityDuration*.5f);
        onDeath?.Invoke();

        if (team == Team.Ennemy)
        {
            PlayerMain.instance.playerXP.GainXP(_xpGains);
            JyrosManager.Instance.RemoveEntity();
        }

        if (spawnThingOnDeath) PoolManager.Instance.ChoosePool(DeathObject).PullObjectFromPool(transform.position);

        if (destroyOnDeath)
        {
            PooledObject pooledObject;
            if (transform.root.TryGetComponent(out pooledObject)|| TryGetComponent(out pooledObject))
            {
                pooledObject.GoBackIntoPool();
            }
            else
            {
                Debug.LogError("No pooled object attached - "+ gameObject.name,this);
                Destroy(gameObject);
            }
        }
    }
}
