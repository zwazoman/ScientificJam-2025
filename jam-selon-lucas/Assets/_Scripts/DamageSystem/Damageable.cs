using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    [field : SerializeField] public float maxHP {  get; private set; }
    public float hp;

    public UnityEvent onDamageTaken,onDeath = new();

    [SerializeField] bool destroyOnDeath = true;

    public Team team;

    public void TakeDamage(float damage)
    {
        hp-=(float)damage;
        onDamageTaken.Invoke();
        if(hp <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        onDeath?.Invoke();
        if (destroyOnDeath)
        {
            if (TryGetComponent(out PooledObject pooledObject))
            {
                pooledObject.GoBackIntoPool();
            }
            else
            {
                Debug.LogError("No pooled object attached");
            }
        }
    }
}
