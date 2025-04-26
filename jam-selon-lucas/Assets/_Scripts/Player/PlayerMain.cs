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

    short _spawnerCPT;

    void Awake()
    {
        instance = this;

        TryGetComponent(out playerXP);
    }

    private void Start()
    {
        playerXP.OnLvlUp += UpgradeGame;
    }

    void UpgradeGame()
    {
        UpdradePlayer();
        UpgradeEnemies();
    }

    void UpdradePlayer()
    {
        UpgradeSpawners(playerGuns, 1.3f, 1.6f, 1.5f, 1.9f);

        if (playerXP.currentLvl % _lvlUpgrade == 0)
        {
            _spawnerCPT++;
            if (_spawnerCPT >= PlayerMain.instance.playerGuns.Count) return;
            PlayerMain.instance.playerGuns[_spawnerCPT].gameObject.SetActive(true);
        }
    }

    void UpgradeEnemies()
    {
        UpgradeSpawners(enemySpawners, 1.1f, 1.4f, 1.3f, 1.5f);


    }


    void UpgradeSpawners(List<Spawner> spawners, float minProjPerSalveGrowth, float maxProjPerSalveGrowth, float minTimePerSalveGrowth, float maxTimePerSalveGrowth)
    {
        foreach (Spawner spawner in spawners)
        {
            if (spawner.gameObject.activeSelf)
            {
                float newProjectilesPerSalves = spawner.projectilesPerSalve * UnityEngine.Random.Range(minProjPerSalveGrowth, maxProjPerSalveGrowth);
                spawner.projectilesPerSalve = newProjectilesPerSalves;

                spawner.timeBetweenSalves *= UnityEngine.Random.Range(minTimePerSalveGrowth, maxTimePerSalveGrowth);
            }
        }
    }

}
