using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class TestingSceneSwap : MonoBehaviour
{
    private void Update()
    {
        //check if in unity editor
        if (Application.isEditor)
        {
            //check if the key is pressed
            if (Keyboard.current.bKey.wasPressedThisFrame)
            {
                //load the bootscreen scene
                SceneManager.LoadScene("Bootscreen");
            }
            if (Keyboard.current.gKey.wasPressedThisFrame)
            {
                //load the game scene
                SceneManager.LoadScene(1);
            }
        }
    }
}
