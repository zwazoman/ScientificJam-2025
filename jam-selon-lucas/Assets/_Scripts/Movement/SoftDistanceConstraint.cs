using UnityEngine;

public class SoftDistanceConstraint : PhysicalObject
{
    [SerializeField] Transform target;
    [SerializeField] Vector2 distanceRange;
    [SerializeField] float speed;

    private void Awake()
    {
        OnInstantiatedByPool();
    }
    public void OnInstantiatedByPool()
    {
        if (target == null) target = PlayerMain.instance.transform;
    }

    // Update is called once per frame
    protected override void Update()
    {
        Vector3 offset = transform.position - target.position;
        float m = offset.magnitude;
        offset = offset / m * Mathf.Clamp(m, distanceRange.x, distanceRange.y);
        Vector3 TargetPose = target.position + offset;
        
        Velocity = Vector3.ClampMagnitude(TargetPose-transform.position, speed * Time.deltaTime);

        base.Update();
    }

    public enum TargetableObject
    {
        TargetTransform,
        Mouse,
        Player
    }
}
