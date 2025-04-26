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
    }

    private void Start()
    {
        playerXP.OnLvlUp += () =>
        {
            UpgradeSpawners(playerGuns, ref _playerSpawnerCPT, _lvlUpgrade, 1.3f, 1.6f, 0.65f, 0.78f);
            UpgradeSpawners(enemySpawners, ref _enemySpawnerCPT, _lvlEnemy, 1.1f, 1.4f, 0.8f, 0.9f);
        };
    }

    void UpgradeSpawners(List<Spawner> spawners, ref short cpt, int lvlOffset, float minProjPerSalveGrowth, float maxProjPerSalveGrowth, float minTimePerSalveGrowth, float maxTimePerSalveGrowth)
    {
        foreach (Spawner spawner in spawners)
        {
            if (spawner.gameObject.activeSelf)
            {
                float newProjectilesPerSalves = spawner.projectilesPerSalve * Random.Range(minProjPerSalveGrowth, maxProjPerSalveGrowth);
                spawner.projectilesPerSalve = newProjectilesPerSalves;

                spawner.timeBetweenSalves *= Random.Range(minTimePerSalveGrowth, maxTimePerSalveGrowth);
            }
        }

        if (playerXP.currentLvl % lvlOffset == 0)
        {
            if (cpt >= spawners.Count) return;
            cpt++;
            spawners[cpt].gameObject.SetActive(true);
        }
    }

}
