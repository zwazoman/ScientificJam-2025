using UnityEngine;
using UnityEngine.UI;

public class Jyros_slider : MonoBehaviour
{
    Slider slider;
    [SerializeField] Image image; 

    private void Start()
    {
        slider = GetComponent<Slider>();
        slider.maxValue = JyrosManager.Instance.jyrosSummonThreshold;
    }

    private void Update()
    {
        slider.value = JyrosManager.Instance.EntityCpt;
        image.color = Color.Lerp(Color.white,Color.red, Mathf.Pow(Mathf.InverseLerp(0,slider.maxValue,slider.value),2f));
    }
}
