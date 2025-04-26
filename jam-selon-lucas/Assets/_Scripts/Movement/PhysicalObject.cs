using UnityEngine;

public class PhysicalObject : MonoBehaviour
{
    public Vector2 Velocity { get; protected set; }
    Rigidbody2D rb;

    private void Awake()
    {
        TryGetComponent(out rb);
    }
    protected virtual void Update()
    {
        if(rb!=null) rb.linearVelocity = Velocity;
        else transform.position += (Vector3)Velocity * Time.deltaTime;
    }

}
