using UnityEngine;

public class SoftDistanceConstraint : PhysicalObject
{
    [SerializeField] Transform target;
    [SerializeField] Vector2 distanceRange;
    [SerializeField] float speed;

    // Update is called once per frame
    void Update()
    {
        Vector3 offset = target.position - transform.position;
        float m = offset.magnitude;
        offset = offset / m * Mathf.Clamp(m, distanceRange.x, distanceRange.y);
        Velocity = Vector3.ClampMagnitude(offset, speed * Time.deltaTime);
    }

    public enum TargetableObject
    {
        TargetTransform,
        Mouse,
        Player
    }
}
