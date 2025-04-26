using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class MoveForwardOverTime : PhysicalObject
{
    [SerializeField] float speed;
    [SerializeField] float randomAdditionnalSpeedMagnitude;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnInstantiatedByPool()
    {
        speed += Random.value * randomAdditionnalSpeedMagnitude;
    }

    // Update is called once per frame
    protected override void Update()
    {
        Velocity = transform.right * speed;
        base.Update();
    }
}
