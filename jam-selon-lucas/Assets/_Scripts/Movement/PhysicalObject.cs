using UnityEngine;

public class PhysicalObject : MonoBehaviour
{
    public Vector2 Velocity { get; protected set; }
    protected virtual void Update()
    {
        transform.position += (Vector3)Velocity * Time.deltaTime;
    }

}
