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
        Vector3 targetToTransform = transform.position - target.position;
        float m = targetToTransform.magnitude;
        targetToTransform = targetToTransform / m * Mathf.Clamp(m, distanceRange.x, distanceRange.y);
        Vector3 TargetPose = target.position + targetToTransform;
        
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
