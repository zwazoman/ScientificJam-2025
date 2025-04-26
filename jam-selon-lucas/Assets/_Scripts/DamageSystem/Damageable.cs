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

    private void Awake()
    {
        OnInstantiatedByPool();
    }

    void OnInstantiatedByPool()
    {
        if(spriteRenderer==null)TryGetComponent(out spriteRenderer);
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

    public async void Die()
    {
        await Awaitable.WaitForSecondsAsync(invicibilityDuration);
        onDeath?.Invoke();

        if(team == Team.Ennemy) PlayerMain.instance.playerXP.GainXP(_xpGains);

        if (spawnThingOnDeath) PoolManager.Instance.ChoosePool(DeathObject).PullObjectFromPool(transform.position);

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
                Destroy(gameObject);
            }
        }
    }
}
