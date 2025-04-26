using UnityEngine;

public class SoftDistanceConstraint : PhysicalObject
{
    [SerializeField] Transform target;
    [SerializeField] Vector2 distanceRange;
    [SerializeField] float speed;

    public void OnInstantiatedByPool()
    {
        if (target == null) target = PlayerMain.instance.transform;
    }

    // Update is called once per frame
    protected override void Update()
    {
        Debug.Log("tvybitonj");
        Vector3 offset = target.position - transform.position;
        float m = offset.magnitude;
        offset = offset / m * Mathf.Clamp(m, distanceRange.x, distanceRange.y);
        Velocity = Vector3.ClampMagnitude(offset, speed * Time.deltaTime);

        base.Update();
    }

    public enum TargetableObject
    {
        TargetTransform,
        Mouse,
        Player
    }
}
