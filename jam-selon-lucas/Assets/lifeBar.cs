using UnityEngine;
using UnityEngine.UI;

public class lifeBar : MonoBehaviour
{
    Slider slider;
    Damageable player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = PlayerMain.instance.GetComponent<Damageable>();
        slider = GetComponent<Slider>();
        slider.maxValue = player.hp;
        slider.value = player.hp;
        player.onDamageTaken.AddListener(UpdateSlider);
    }

    void UpdateSlider()
    {
         slider.value = player.hp; 
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
