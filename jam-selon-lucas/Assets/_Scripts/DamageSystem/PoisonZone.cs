using UnityEngine;

public class PoisonZone : MonoBehaviour
{
    public Team targetTeam;
    float DamagePerSeconds;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Damageable damageable)) damageable.TakeDamage(DamagePerSeconds * Damageable.invicibilityDuration);
    }

}
