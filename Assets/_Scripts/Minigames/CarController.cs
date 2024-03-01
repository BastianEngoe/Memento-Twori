using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public enum carState
    {
        TOP,
        BOT
    }
    public carState curState;

    private bool hasCollided;
    
    private void Start()
    {
        Destroy(gameObject, 5f);

        /*
        if (curState == carState.TOP)
        {
            transform.DOLocalMoveY(transform.position.y - 15f, 8f);
        }
        else
        {
            transform.DOLocalMoveY(transform.position.y + 15f, 8f);
        }*/
    }

    private void Update()
    {
        if (curState == carState.TOP)
        {
            transform.Translate(Vector3.down * 5 * Time.deltaTime * FishMegaGameController.instance.masterSpeed);
        }
        else
        {
            transform.Translate(Vector3.up * 5 * Time.deltaTime * FishMegaGameController.instance.masterSpeed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fish") && !hasCollided)
        {
            hasCollided = true;
            Destroy(gameObject);
            Debug.Log("Yooooo");
            FishMegaGameController.instance.NextMinigame();
        }

        if (other.CompareTag("Respawn"))
        {
            Destroy(gameObject);
        }
    }
}
