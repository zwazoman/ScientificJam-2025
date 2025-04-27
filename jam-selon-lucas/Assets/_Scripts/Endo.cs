using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class Endo : MonoBehaviour
{
    public GameObject panel;



    private async void Start()
    {
        PlayerMain.instance.GetComponent<Damageable>().onDeath.AddListener(()=>Destroy(gameObject));
        while(isActiveAndEnabled)
        {
            await Awaitable.WaitForSecondsAsync(Random.Range(45, 70));
            activate();
        }
    }

    private void activate()
    {
        panel.SetActive(true);
        Time.timeScale = 0;
    }

    private void Update()
    {
        if (Input.anyKeyDown && panel.activeSelf)
        {
            Time.timeScale = 1;
            panel.SetActive(false);
        }
    }
}
