using NUnit.Framework;
using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;

public class PlayerXP : MonoBehaviour
{
    public event Action OnGainXP;
    public event Action OnLvlUp;

    [SerializeField] List<Spawner> _playerSpawners = new();

    [SerializeField] public float xpGrowthFactor = 1.1f;

    [SerializeField,Tooltip("tous les combien de niveaux on gagne un nouveau spawner")] int _lvlUpgrade = 5;

    [HideInInspector] public float maxXp;
    [HideInInspector] public float currentXP;
    [HideInInspector] public int currentLvl;

    short _spawnerCPT;

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
        currentXP = 0;
        maxXp *= xpGrowthFactor;
        currentLvl++;

        OnLvlUp?.Invoke();

        UpdradePlayer();
    }

    void UpdradePlayer()
    {
        foreach(Spawner spawner in _playerSpawners)
        {
            if (spawner.gameObject.activeSelf)
            {
                float newProjectilesPerSalves = (float)spawner.projectilesPerSalve * UnityEngine.Random.Range(1.2f, 1.7f);
                spawner.projectilesPerSalve = (short)newProjectilesPerSalves;

                spawner.timeBetweenSalves *= UnityEngine.Random.Range(1.2f, 1.7f);
            }
        }

        if (currentLvl % _lvlUpgrade == 0)
        {
            _spawnerCPT++;
            if (_spawnerCPT >= _playerSpawners.Count) return;
            _playerSpawners[_spawnerCPT].gameObject.SetActive(true);
        }
    }

}
