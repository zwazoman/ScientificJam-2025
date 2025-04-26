using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    public float damage;
    public Team team;

    [SerializeField] bool DieOnContact = false;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent(out Damageable damageable))
        {
            if(AreEnnemies(team,damageable.team))
            {
                damageable.TakeDamage(damage);
                
                if (DieOnContact)
                {
                    if (TryGetComponent(out Damageable o))
                    {
                        o.Die();
                    }
                    else if (transform.root.TryGetComponent(out PooledObject pooledObject))
                    {
                        pooledObject.GoBackIntoPool();
                    }
                }
            }
        }
    }

    public static bool AreEnnemies(Team a, Team b)
    {
        if ((a == Team.Player &&  b == Team.Ennemy)||(a == Team.Ennemy && b == Team.Player)
            || a == Team.None || b == Team.None || b == Team.Both || a == Team.Both) 
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

public enum Team
{
    None,
    Player,
    Ennemy,
    Both,
}