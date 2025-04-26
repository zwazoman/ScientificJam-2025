using UnityEngine;

public class HardDistanceConstraint : MonoBehaviour
{
    [SerializeField] Transform target ;
    [SerializeField] float distance ;

    // Update is called once per frame
    void Update()
    {
        Vector3 offset =  target.position - transform.position;
        offset = offset.normalized * distance;
        transform.position += offset;
    }
}
