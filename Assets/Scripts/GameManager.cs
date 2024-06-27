using System;
using System.Collections.Generic;
using Timers;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : Singleton<GameManager>
{
    [Header("Timer")] public int initialTime;
    public int maxTime;
    public CountdownTimer Timer;

    [Header("Guys")] public MinMax levelSpeed;
    public int numGuysPerType;
    int initialNumGuysPerType;
    public int numGuysPerTypeScaling;
    public List<GameObject> GuyObjs;

    [Header("Other")] public Vector2 levelMinBound;
    public Vector2 levelMaxBound;
    public int hitTargetTimeGain;
    public int missedTargetTimeLost;
    public Player Player;

    private List<GameObject> guyInstances = new();

    public int level = 1;

    private void Awake()
    {
        Timer = new CountdownTimer(initialTime);
        Timer.EndEvent += Lose;
        initialNumGuysPerType = numGuysPerType;
    }

    public void StartGame()
    {
        Timer.Start();
        InitLevel(level);
        Player.isActive = true;
        UIManager.Instance.ToggleStartScreenGuyImage(false);
        UIManager.Instance.ToggleStartScreen(false);
    }

    void InitLevel(int level)
    {
        // Spawn guys
        int targetGuyIndex = Random.Range(0, GuyObjs.Count);
        List<int> nonTargetGuyIndexes = new();
        for (int i = 0; i < GuyObjs.Count; i++)
        {
            if (i == targetGuyIndex) continue;
            nonTargetGuyIndexes.Add(i);
        }

        foreach (int nonTargetGuyIndex in nonTargetGuyIndexes)
        {
            float nonTargetSpeed = Random.Range((float) levelSpeed.Min, (float) levelSpeed.Max);
            Vector3 nonTargetDirVector = Util.GetRandomDirection();
            for (int i = 0; i < numGuysPerType; i++)
            {
                SpawnGuy(nonTargetGuyIndex, nonTargetSpeed, nonTargetDirVector);
            }
        }
        
        // Set up target guy
        Guy targetGuy = SpawnGuy(targetGuyIndex, Random.Range((float) levelSpeed.Min, (float) levelSpeed.Max),Util.GetRandomDirection());
        targetGuy.AddComponent<BoxCollider2D>();
        targetGuy.OnClick += FoundGuy;
        UIManager.Instance.UpdateLevelWanted(targetGuy.GetComponent<SpriteRenderer>().sprite);
        
        UIManager.Instance.UpdateLevelText(level);
    }
    
    Guy SpawnGuy(int guyIndex, float speed, Vector3 dirVector)
    {
        Vector2 spawnPos = new Vector2(Random.Range(levelMinBound.x, levelMaxBound.x),
            Random.Range(levelMinBound.y, levelMaxBound.y));
        GameObject o = Instantiate(GuyObjs[guyIndex], new Vector3(spawnPos.x, spawnPos.y, 0), Quaternion.identity);
        Guy guy = o.GetComponent<Guy>();
        GuyMovement guyMovement = o.GetComponent<GuyMovement>();

        guyMovement.Init(MoverID.Linear, speed, dirVector);
        
        guyInstances.Add(o);

        return guy;
    }

    void FoundGuy()
    {
        print("You found guy!");
        
        // Add time
        Timer.AddTime(hitTargetTimeGain);

        // Clean up level
        CleanLevel();

        // Start next level
        level++;
        numGuysPerType += numGuysPerTypeScaling;
        InitLevel(level);
    }

    void CleanLevel() {
        foreach (GameObject guyInstance in guyInstances)
        {
            Destroy(guyInstance);
        }
        guyInstances.Clear();
    }

    public void LoseTime()
    {
        Timer.Tick(missedTargetTimeLost);
    }

    void Lose()
    {
        print("You lose!");
        // Show score
        UIManager.Instance.ToggleHighScoreText(true);
        UIManager.Instance.UpdateHighScoreText(level);
        UIManager.Instance.UpdateStartScreenGuyImage();
        
        // Reset game state
        Timer.Reset();
        CleanLevel();
        level = 0;
        numGuysPerType = initialNumGuysPerType;
        Player.isActive = false;
        UIManager.Instance.ToggleStartScreenGuyImage(true);
        UIManager.Instance.ToggleStartScreen(true);
    }
}