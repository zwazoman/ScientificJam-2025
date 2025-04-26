using UnityEngine;

public class RotateOverTime : MonoBehaviour
{
    [SerializeField] float speed, randomSpeedOffsetMagnitude;
    static Vector3 axis = new Vector3(0, 0, 1);
    private void OnInstantiatedByPool()
    {
        speed += (Random.value-.5f) * randomSpeedOffsetMagnitude;
        transform.Rotate(axis, Random.value*360);
    }

    private void Update()
    {
        transform.Rotate(axis, speed * Time.deltaTime); 
    }

}
