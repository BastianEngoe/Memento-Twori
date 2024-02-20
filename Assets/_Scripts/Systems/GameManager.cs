using StarterAssets;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    public enum Rooms
    {
        INTRO,
        FARM,
        RACE,
        BLOCK,
        SHOOTER
    }
    
    [Header("Room state")]
    public Rooms curRoom;
    
    [HideInInspector] public GameObject player;
    private CharacterController playerCharController;
    private FirstPersonController playerFPSController;

    private bool canPause, isPaused;
    [HideInInspector] public bool isNodding, isShaking;

	[Header("Dialogue audio source")]
    public AudioSource mascotSpeaker;

    private void Awake()
    {
        //Set the GM instance to this script so we can reference it easily from any other script.
        instance = this;
        
        //Setting variables.
        player = GameObject.FindWithTag("Player");
        playerCharController = player.GetComponent<CharacterController>();
        playerFPSController = player.GetComponent<FirstPersonController>();
    }

    private void Update()
    {
        //Simple pause function
        if (canPause)
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                isPaused = !isPaused;
                Debug.Log("Paused " + isPaused);
            }

            if (isPaused)
            {
                Time.timeScale = 0f;
                playerFPSController.RotationSpeed = 0f;
            }
            else
            {
                Time.timeScale = 1f;
                playerFPSController.RotationSpeed = 1f;
            }
        }
    }

    public void ToggleMovement(bool canMove)
    {
        //Easy to use function to toggle all movement, can be referenced from any script.
        if (!canMove)
        {
            playerFPSController.enabled = false;
            HeldItem.instance.canPickup = false;
        }
        else
        {
            playerFPSController.enabled = true;
            HeldItem.instance.canPickup = true;
        }
    }
    
    public void ToggleJump(bool canJump)
    {
        //Easy to use function to toggle all jumping, can be referenced from any script.
        if (!canJump)
        {
            playerCharController.slopeLimit = 90.0f;
        }
        else
        {
            playerCharController.slopeLimit = 45.0f;
        }
    }

    public void RetrievePlayerInput(out int input)
    {
        //int explanation
        //0 - No input
        //1 - Yes
        //2 - No

        if (isNodding)
        {
            input = 1;
            return;
        }

        if (isShaking)
        {
            input = 2;
            return;
        }

        input = 0;
    }
}
