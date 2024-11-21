using UnityEngine;

public class StartGame : MonoBehaviour
{
    public GameObject startButton; // Assign the Start Button GameObject in the Inspector

    private void Start()
    {
        // Pause the game initially
        Time.timeScale = 0f;

        // Ensure the Start Button is active
        startButton.SetActive(true);
    }

    // Method called by the Start Button
    public void StartTheGame()
    {
        // Resume the game by setting timeScale to 1
        Time.timeScale = 1f;

        // Disable the Start Button so it disappears
        startButton.SetActive(false);

        Debug.Log("Game Started!");
    }
}
