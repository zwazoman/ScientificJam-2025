using UnityEngine;

public class HardDistanceConstraint : MonoBehaviour
{
    [SerializeField] Transform target ;
    [SerializeField] float distanceRange = 1;

    // Update is called once per frame
    void Update()
    {
        Vector3 offset =   transform.position - target.position;
        offset.z = 0;
        //float m = offset.magnitude;
        offset = Vector3.ClampMagnitude(offset,distanceRange);

        transform.position = target.position + offset;
    }
}
