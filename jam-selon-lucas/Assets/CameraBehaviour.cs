using DG.Tweening;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerMain.instance.GetComponent<Damageable>().onDamageTaken.AddListener( Shake);
    }

    public void Shake()
    {
        transform.DOShakePosition(.5f, 1, 10, 90);
    }

}
