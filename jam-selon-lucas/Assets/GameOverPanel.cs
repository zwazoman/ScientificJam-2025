using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverPanel : MonoBehaviour
{


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerMain.instance.gameObject.GetComponent<Damageable>().onDeath.AddListener(() =>
        {
            gameObject.SetActive(true);
            Time.timeScale = 0;
        });

        gameObject.SetActive(false) ;
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);

    }
}
