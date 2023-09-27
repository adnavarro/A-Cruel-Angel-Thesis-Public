using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField] private List<Enemy> testEnemies;
    [SerializeField] private float probability = 40f; //40f
    [SerializeField] private float startDelay = 10f;  //10f 
    [SerializeField] private float nextOcurrence = 5f; //5f;
    
    private static bool _isGeneratorPaused;
    private static DateTime _timeForNextCombat;

    public static bool IsGeneratorPaused
    {
        get => _isGeneratorPaused;
        set
        {
            _isGeneratorPaused = value;
            if (value == false)
            {
                _timeForNextCombat = DateTime.Now.AddSeconds(2f);
            }
        }
    }

    private void Start()
    {
        _timeForNextCombat = DateTime.Now.AddSeconds(startDelay);
        IsGeneratorPaused = false;
    }

    private void Update()
    {
        if (!IsGeneratorPaused)
        {
            GenerateRandomBattle();
        }
    }

    private void GenerateRandomBattle()
    {
        if (DateTime.Now >= _timeForNextCombat)
        {
            if (Random.Range(0f, 100f) <= probability)
            {
                GameManager.Instance.CurrentEnemy = testEnemies[Random.Range(0, testEnemies.Count)];
                GameManager.Instance.StartBattleScene();
            }
            else
            {
                _timeForNextCombat = DateTime.Now.AddSeconds(nextOcurrence);
            }
        }
    }
}
