using UnityEngine;

public class CameraManager : MonoBehaviour
{
    // Static instance of the class
    private static CameraManager instance;

    // Public property to access the instance
    public static CameraManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<CameraManager>();
                if (instance == null)
                {
                    GameObject go = new GameObject();
                    go.name = "CameraManager";
                    instance = go.AddComponent<CameraManager>();
                }
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
}
