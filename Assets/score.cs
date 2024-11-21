using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro; // Required for TextMeshPro

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;        // UI Text to display the score
    public GameObject winnerPanel; // Panel to show when the player wins
    private int totalAliens;      // Total number of aliens in the scene
    private int score = 0;        // Player's score

    private void Start()
    {
        // Count the number of aliens at the start
        totalAliens = GameObject.FindGameObjectsWithTag("Alien").Length;

        // Ensure totalAliens is not zero at the start (sanity check)
        if (totalAliens <= 0)
        {
            Debug.LogError("No aliens found at the start of the game!");
            return;
        }

        // Update the score UI at the start
        UpdateScoreUI();

        // Hide the winner panel initially
        if (winnerPanel != null)
            winnerPanel.SetActive(false);
    }

    // Method to call when an alien is killed
    public void AlienKilled(int points)
    {
        score += points; // Add points to the score
        UpdateScoreUI();

        totalAliens--; // Decrease the total aliens count
        Debug.Log("Aliens remaining: " + totalAliens);

        // Check if all aliens are defeated
        if (totalAliens <= 0)
        {
            ShowWinnerPanel();
        }
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score;
    }

    private void ShowWinnerPanel()
    {
        if (winnerPanel != null)
            winnerPanel.SetActive(true);

        // Optionally pause the game
        Time.timeScale = 0f; // Freezes all actions, including player and physics
    }

    public void RestartGame()
    {
        // Reset time scale in case it was paused
        Time.timeScale = 1f;

        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
