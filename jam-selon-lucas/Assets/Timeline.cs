using UnityEngine;
using UnityEngine.SceneManagement;

public class Timeline : MonoBehaviour
{

    Vector3 StartDirection;
    Vector3 startPose;

    [SerializeField]
    float speed,magnitude,frequency;

    private void Start()
    {
        OnPulledFromPool();
    }
    public void OnPulledFromPool()
    {
        startPose = transform.position;
        StartDirection = transform.right;
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = startPose + StartDirection * Time.time * speed + transform.up * Mathf.Sin(Time.time * frequency) * magnitude;
        
    }
}
