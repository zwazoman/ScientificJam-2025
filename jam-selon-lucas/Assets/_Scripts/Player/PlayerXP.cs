using System;
using UnityEngine;
using UnityEngine.Events;

public class PlayerXP : MonoBehaviour
{
    public event Action OnGainXP;
    public event Action OnLvlUp;

    [SerializeField] public float xpGrowthFactor = 1.1f;

    [HideInInspector] public float maxXp;
    [HideInInspector] public float currentXP;
    [HideInInspector] public int currentLvl;

    public void GainXP(float value)
    {
        OnGainXP?.Invoke();

        currentXP += value;

        if(currentXP > maxXp)
        {
            float remainder = currentXP - maxXp;
            LvlUp();
            currentXP += remainder;
        }
    }

    public void LvlUp()
    {
        OnLvlUp?.Invoke();

        currentXP = 0;
        maxXp *= xpGrowthFactor;
        currentLvl++;
    }

}
