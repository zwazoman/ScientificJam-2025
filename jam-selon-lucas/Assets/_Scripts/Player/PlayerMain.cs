using UnityEngine;

public class PlayerMain : MonoBehaviour
{
    public static PlayerMain instance { get; private set; }

    void Awake()
    {
        instance = this;
    }

}
