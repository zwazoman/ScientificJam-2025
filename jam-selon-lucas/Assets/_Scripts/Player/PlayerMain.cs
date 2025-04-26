using UnityEngine;

public class PlayerMain : MonoBehaviour
{
    public static PlayerMain instance { get; private set; }

    public PlayerXP playerXP;

    void Awake()
    {
        instance = this;

        TryGetComponent(out playerXP);
    }



}
