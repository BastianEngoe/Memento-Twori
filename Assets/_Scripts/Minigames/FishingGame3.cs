using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class FishingGame3 : MonoBehaviour
{
    [SerializeField] private GameObject fish;
    [SerializeField] private GameObject carBot, carTop;
    [SerializeField] private Transform[] carSpawnsTop, carSpawnsBot;
    private bool isMoving;

    private int fishPosition = -1;

    private float carSpeed = 0.5f;

    private void Start()
    {
        transform.parent = null;
        transform.position = new Vector3(-0.882170439f, -1.31179619f, 7.5526638f);
        transform.localScale = Vector3.one;
        
        FishMegaGameController.instance.instructions.text = "Help the fish cross the street!";
        
        InvokeRepeating("SpawnBottomCar", 0.2f, Random.Range(1.2f, 1.8f));
        InvokeRepeating("SpawnTopCar", 0.1f, Random.Range(1.2f, 1.8f));
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0) && fishPosition < 5)
        {
            if (!isMoving)
            {
                isMoving = true;
                fishPosition++;
                fish.transform.DOLocalMoveX(fishPosition, 0.25f).OnComplete(() => isMoving = false);
                Invoke("CheckWinCondition", 0.25f);
            }
        }
    }

    private void SpawnBottomCar()
    {
        Instantiate(carBot, carSpawnsBot[Random.Range(0, carSpawnsBot.Length)].position, Quaternion.identity, transform);
    }

    private void SpawnTopCar()
    {
        Instantiate(carTop, carSpawnsTop[Random.Range(0, carSpawnsTop.Length)].position, Quaternion.identity, transform);
    }

    private void CheckWinCondition()
    {
        if (fishPosition >= 5)
        {
            Debug.Log("WIN");
            FishMegaGameController.instance.currentScore += 250;
            FishMegaGameController.instance.NextMinigame();
        }
    }
}
