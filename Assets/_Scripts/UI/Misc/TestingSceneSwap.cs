using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class TestingSceneSwap : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            SceneManager.LoadScene("Bootscreen");
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            SceneManager.LoadScene(1);
        }
    }
}
