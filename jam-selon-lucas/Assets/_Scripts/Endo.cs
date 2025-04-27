using UnityEngine;

public class Endo : MonoBehaviour
{
    PooledObject pooledObject;

    private void OnPulledFromPool()
    {
        Time.timeScale = 0;
        pooledObject = transform.root.GetComponent<PooledObject>();
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            Time.timeScale = 1;
            pooledObject.GoBackIntoPool();
        }
    }
}
