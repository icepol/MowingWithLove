using UnityEngine;

public class CloudSpawner : MonoBehaviour
{
    [SerializeField] private Cloud[] cloudPrefabs;

    private void Start()
    {
        var randomCloudPrefab = cloudPrefabs[UnityEngine.Random.Range(0, cloudPrefabs.Length)];
        
        Instantiate(randomCloudPrefab, transform.position, Quaternion.identity);
    }
}
