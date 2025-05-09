using System.Collections.Generic;
using UnityEngine;

public class PlayerMain : MonoBehaviour
{
    public static PlayerMain instance { get; private set; }

    [HideInInspector] public PlayerXP playerXP;

    [SerializeField]  List<Spawner> playerGuns = new();
    [SerializeField]  List<Spawner> enemySpawners = new();

    [Space(10)]

    [SerializeField, Tooltip("tous les combien de niveaux on gagne un nouveau spawner")] public int _lvlUpgrade = 5;
    [SerializeField, Tooltip("tous les combien de niveaux un nouvel ennemi spawn")] public int _lvlEnemy = 3;

    short _playerSpawnerCPT;
    short _enemySpawnerCPT;

    void Awake()
    {
        instance = this;

        TryGetComponent(out playerXP);

        foreach(var p in playerGuns)
        {
            p.enabled = false;
        }
        playerGuns[0].enabled = true;
    }

    private void Start()
    {
        playerXP.OnLvlUp += () =>
        {
            UpgradeSpawners(playerGuns, ref _playerSpawnerCPT, _lvlUpgrade, 1.05f, 1.2f, 0.8f, 0.9f);
            UpgradeSpawners(enemySpawners, ref _enemySpawnerCPT, _lvlEnemy, 1.2f, 1.4f, 0.7f, 0.9f);
        };
    }

    void UpgradeSpawners(List<Spawner> spawners, ref short cpt, int lvlOffset, float minProjPerSalveGrowth, float maxProjPerSalveGrowth, float minTimePerSalveGrowth, float maxTimePerSalveGrowth)
    {
        foreach (Spawner spawner in spawners)
        {
            if (spawner.enabled)
            {
                float newProjectilesPerSalves = spawner.projectilesPerSalve * Random.Range(minProjPerSalveGrowth, maxProjPerSalveGrowth);
                spawner.projectilesPerSalve = newProjectilesPerSalves;

                spawner.timeBetweenSalves *= Random.Range(minTimePerSalveGrowth, maxTimePerSalveGrowth);
            }
        }

        if (playerXP.currentLvl % lvlOffset == 0)
        {
            cpt++;
            if (cpt >= spawners.Count) return;
            spawners[cpt].enabled = true;
        }
    }

}
