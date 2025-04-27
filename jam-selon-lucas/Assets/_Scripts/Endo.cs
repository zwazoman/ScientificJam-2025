using UnityEngine;

public class Endo : MonoBehaviour
{
    private void Start()
    {
        Time.timeScale = 0;
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            Time.timeScale = 1;
            Destroy(gameObject);
        }
    }
}
