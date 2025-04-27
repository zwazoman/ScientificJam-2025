using UnityEngine;
using UnityEngine.UI;

public class ExpSlider : MonoBehaviour
{
    Slider slider;
    private void Start()
    {
        TryGetComponent(out slider);
        slider.maxValue= PlayerMain.instance.playerXP.maxXp;
        slider.value = PlayerMain.instance.playerXP.currentXP;
        PlayerMain.instance.playerXP.OnGainXP += () => { slider.value = PlayerMain.instance.playerXP.currentXP; };
    }
}
