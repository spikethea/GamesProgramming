using UnityEngine;
using UnityEngine.SceneManagement;

public class OuterSpace : MonoBehaviour
{
    [SerializeField] private UIManager UI;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) {
            FinishGame();
        }
    }

    private void FinishGame() 
    {
        UI.mainUI.SetMainText("You reached Outer Space");
        UI.graphicsUI.ShowBlackScreen();
        AudioListener listener = Camera.main.GetComponent<AudioListener>();

        listener.enabled = false;

        Invoke(nameof(ResetGame), 5f);
    }

    void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
