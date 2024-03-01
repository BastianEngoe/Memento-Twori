using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class FishMegaGameController : MonoBehaviour
{
    public static FishMegaGameController instance;

    [SerializeField] private GameObject mainUI;
    [SerializeField] private TMP_Text scoreText;
    public TMP_Text instructions;
    
    public List<GameObject> fishMinigames;
    public int currentMinigameIndex = 0;
    public GameObject currentMinigame;
    private bool isPlaying = false;

    public int currentScore;

    public float masterSpeed = 1f;

    public UnityEvent onGameStart, onGameCompletion;

    
    
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

        if (isPlaying)
        {
            scoreText.text = "Score: " + currentScore;
        }
    }

    public void StartMinigames()
    {
        isPlaying = true;
        currentMinigame = Instantiate(fishMinigames[currentMinigameIndex], mainUI.transform);
        currentMinigame.SetActive(true);
        currentScore = 0;
        onGameStart.Invoke();
    }

    public void NextMinigame()
    {
        StartCoroutine(RealNextGame());
    }

    private IEnumerator RealNextGame()
    {
        if (isPlaying)
        {
            Destroy(currentMinigame);

            instructions.text = "Good job! Keep going!";

            yield return new WaitForSeconds(2f);

            if (isPlaying)
            {
                        
                currentMinigameIndex++;
                if (currentMinigameIndex >= fishMinigames.Count)
                {
                    currentMinigameIndex = 0;
                    masterSpeed += 0.25f;
                }

                currentMinigame = Instantiate(fishMinigames[currentMinigameIndex], mainUI.transform);
                currentMinigame.SetActive(true);
            }
        }
    }

    public void CompleteMinigame()
    {
        Destroy(currentMinigame);
        StopCoroutine(RealNextGame());
        masterSpeed = 1f;
        isPlaying = false;
        onGameCompletion.Invoke();
    }
}
