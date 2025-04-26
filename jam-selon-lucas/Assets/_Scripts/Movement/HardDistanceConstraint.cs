using UnityEngine;

public class HardDistanceConstraint : MonoBehaviour
{
    [SerializeField] Transform target ;
    [SerializeField] Vector2 distanceRange;

    // Update is called once per frame
    void Update()
    {
        Vector3 offset =  target.position - transform.position;
        float m = offset.magnitude;
        offset = offset / m * Mathf.Clamp(m, distanceRange.x, distanceRange.y);

        transform.position += offset;
    }
}
