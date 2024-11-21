using UnityEngine;

public class AlienCollision : MonoBehaviour
{public int points = 10;
    void OnTriggerEnter(Collider other)
    {
        // Check if the object that collided is tagged as "Bullet"
        if (other.CompareTag("Alien"))
        {
            // Destroy the alien
            Destroy(gameObject);

            // Optionally destroy the bullet as well
            Destroy(other.gameObject);
	Debug.Log($"Collided with: {other.gameObject.name}");
	ScoreManager scoreManager = FindObjectOfType<ScoreManager>();
            if (scoreManager != null)
            {
                scoreManager.AlienKilled(points);
            }
        }

    }


}
