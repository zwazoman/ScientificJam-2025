using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class expDot : MonoBehaviour
{
    Transform target;
    Vector3 velocity;

    GameManager gm;
    PooledObject _asPooledObject;

    public float initialBurstPower;
    public float acceleration=50;
    public float magnetStrenght = 0.01f;
    public float maxVelocity = 50;
    public float TimeReward = 0.5f;
    // Start is called before the first frame update
    public void OnInstantiatedByPool()
    {
        target = PlayerMain.instance.transform;
        transform.root.TryGetComponent(out _asPooledObject);
    }

    public void OnPulledFromPool()
    {
        velocity = (transform.position - target.transform.position).normalized * initialBurstPower;
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            velocity += (transform.position - target.position).normalized * -acceleration;
            velocity = Vector3.ClampMagnitude(velocity, maxVelocity);

            transform.position += velocity * Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, target.position, Mathf.Pow(magnetStrenght, Time.deltaTime * 60));
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == target)
        {
            _asPooledObject.GoBackIntoPool();
        }
        
    }

}
