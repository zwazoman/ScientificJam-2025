using NUnit.Framework;
using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;

public class PlayerXP : MonoBehaviour
{
    public event Action OnGainXP;
    public event Action OnLvlUp;

    [SerializeField] public float xpGrowthFactor = 1.1f;

    [SerializeField] public float maxXp;
    [HideInInspector] public float currentXP;
    [HideInInspector] public int currentLvl;
    [HideInInspector] public float totalXP;

    public void GainXP(float value)
    {
        OnGainXP?.Invoke();

        currentXP += value;
        totalXP += value;

        if(currentXP >= maxXp)
        {
            float remainder = currentXP - maxXp;
            LvlUp();
            currentXP += remainder;
        }
    }

    public void LvlUp()
    {
        currentXP = 0;
        maxXp *= xpGrowthFactor;
        currentLvl++;

        print("lvl UP !");
        OnLvlUp?.Invoke();
        SFXManager.Instance.PlaySFXClip(Sounds.LVLUP);
    }



}
