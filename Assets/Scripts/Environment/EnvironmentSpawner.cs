using UnityEngine;

public class EnvironmentSpawner : MonoBehaviour
{
    [SerializeField] private EnvironmentGroup[] environmentGroups;
    
    [SerializeField] private Camera cameraToFollow;
    [SerializeField] private float spawnDistance = 10f;
    
    private float _currentSpawnPositionX;

    private void FixedUpdate()
    {
        SpawnEnvironment();
    }
    
    private void SpawnEnvironment()
    {
        // Get the camera's position
        var cameraPosition = cameraToFollow.transform.position;

        // we have enough segments in the screen
        if (_currentSpawnPositionX > cameraPosition.x + spawnDistance) return;
        
        var selectedGroup = environmentGroups[UnityEngine.Random.Range(0, environmentGroups.Length)];

        // Instantiate the selected environment group at the spawn position
        var spawned = Instantiate(selectedGroup, new Vector2(_currentSpawnPositionX, 0), Quaternion.identity);
        spawned.transform.SetParent(transform);

        // Update the last spawn position
        _currentSpawnPositionX += spawned.Width;
    }
}
