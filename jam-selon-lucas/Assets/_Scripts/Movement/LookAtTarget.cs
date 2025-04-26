using UnityEngine;

public class LookAtTarget : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Transform target;

    [SerializeField] LookBehaviour lookBehaviour;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnPulledFromPool()
    {
        if(lookBehaviour == LookBehaviour.LookAtTargetTransformAlways|| lookBehaviour == LookBehaviour.LookAtPlayerOnStart)
        {
            target = PlayerMain.instance.transform;
        }

        if(lookBehaviour == LookBehaviour.LookAtTargetTransformAtStart || lookBehaviour == LookBehaviour.LookAtTargetTransformAtStart)
        {
            transform.LookAt(target,Vector3.forward);
            enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 o = target.position - transform.position;
        float a = Mathf.Rad2Deg * Mathf.Atan2(o.y,o.x);
        a = Mathf.MoveTowardsAngle(transform.rotation.z,a,speed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(transform.rotation.x,transform.rotation.y,a);
    }

    public enum LookBehaviour
    {
        LookAtTargetTransformAlways,
        LookAtTargetTransformAtStart,
        LookAtPlayerAlways,
        LookAtPlayerOnStart
    }
}
