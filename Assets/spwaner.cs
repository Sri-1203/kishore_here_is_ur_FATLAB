using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject alienPrefab; // Alien prefab to spawn
    public int numberOfAliens; // Number of aliens to spawn
    public float planetRadius; // Radius of the planet
    public Transform parentTransform; // Parent Transform for all spawned aliens


    private void Start()
    {
        SpawnAliens();
    }

    void SpawnAliens()
    {
        for (int i = 0; i < numberOfAliens; i++)
        {
            // Generate a random point on the sphere surface
            Vector3 randomPointOnSphere = Random.onUnitSphere * planetRadius + transform.position;

            // Instantiate the alien at this point, oriented towards the planet center
            //Quaternion spawnRotation = Quaternion.LookRotation(transform.position - randomPointOnSphere);
            GameObject alien =Instantiate(alienPrefab, randomPointOnSphere, Quaternion.identity);
	    alien.transform.parent = parentTransform;
        }
    }
}
