using UnityEngine;

public class LookAtTarget : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Transform target;

    [SerializeField] LookBehaviour lookBehaviour;

    [SerializeField] float RandomOffsetMagnitude;

    public void OnPulledFromPool()
    {
        if(lookBehaviour == LookBehaviour.LookAtPlayerAlways || lookBehaviour == LookBehaviour.LookAtPlayerOnStart)
        {
            target = PlayerMain.instance.transform;
        }

        if(lookBehaviour == LookBehaviour.LookAtMouseOnStart || lookBehaviour == LookBehaviour.LookAtMouseAlways)
        {
            target = Mouse.Instance.transform;
        }

        
        Vector3 o = target.position + Random.insideUnitCircle.normalized * RandomOffsetMagnitude - transform.position;
        o.z = 0f;
        o = o.normalized;
        float a = Mathf.Rad2Deg * Mathf.Atan2(o.y, o.x);
        transform.rotation = Quaternion.Euler(0, 0, a);

        //Debug.Log(transform.eulerAngles);
        if (lookBehaviour == LookBehaviour.LookAtTargetTransformAtStart || lookBehaviour == LookBehaviour.LookAtTargetTransformAtStart || lookBehaviour == LookBehaviour.LookAtMouseOnStart)
        {
            this.enabled = false;
        }
    }

    void Update()
    {
        Vector3 o = target.position - transform.position;
        o.z = 0f;
        o = o.normalized;
        float a = Mathf.Rad2Deg * Mathf.Atan2(o.y, o.x);
        float actualAngle = Mathf.MoveTowardsAngle(transform.eulerAngles.z, a, speed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0, 0, a);
    }

    public enum LookBehaviour
    {
        LookAtTargetTransformAlways,
        LookAtTargetTransformAtStart,
        LookAtPlayerAlways,
        LookAtPlayerOnStart,
        LookAtMouseAlways,
        LookAtMouseOnStart,
    }
}
