using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMegaGameController : MonoBehaviour
{
    public List<GameObject> fishMinigames;
    public int currentMinigameIndex = 0;
    public GameObject currentMinigame;
    private bool isPlaying = false;

    public static FishMegaGameController instance;
    
    private void Awake()
    {
        instance = this;
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && Vector3.Distance(transform.position, GameObject.FindWithTag("Player").transform.position) < 3f && !isPlaying)
        {
            StartMinigames();
        }
    }

    public void StartMinigames()
    {
        isPlaying = true;
        currentMinigame = fishMinigames[currentMinigameIndex];
        currentMinigame.SetActive(true);
    }

    public void NextMinigame()
    {
        currentMinigame.SetActive(false);
        currentMinigameIndex++;
        if (currentMinigameIndex >= fishMinigames.Count)
        {
            currentMinigameIndex = 0;
        }

        currentMinigame = fishMinigames[currentMinigameIndex];
        currentMinigame.SetActive(true);
    }

    public void CompleteMinigame()
    {
        currentMinigame.SetActive(false);
        isPlaying = false;
    }
}
