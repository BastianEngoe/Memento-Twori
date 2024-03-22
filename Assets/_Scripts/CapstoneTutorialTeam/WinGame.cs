using UnityEngine.SceneManagement;
using UnityEngine;

public class WinGame : MonoBehaviour
{
    public GameObject winPanel;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            winPanel.SetActive(true);
        }
    }

    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
