using System.Collections;
using pixelook;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy[] enemies;

    [SerializeField] private float initialSpeed = 5f;
    [SerializeField] private float speedVariation = 0.5f;
    [SerializeField] private float increaseSpeed = 0.5f;
    [SerializeField] private float increaseDelay = 5f;

    private bool _isFacingLeft;
    private float _currentSpeed;

    private void Awake()
    {
        _isFacingLeft = transform.position.x > Camera.main.transform.position.x;
    }
    
    private void Start()
    {
        _currentSpeed = initialSpeed + Random.Range(-speedVariation, speedVariation);
    }

    private void OnEnable()
    {
        EventManager.AddListener(Events.LEVEL_STARTED, OnLevelStarted);
        EventManager.AddListener(Events.PLAYER_DIED, OnPlayerDied);
    }

    private void OnDisable()
    {
        EventManager.RemoveListener(Events.LEVEL_STARTED, OnLevelStarted);
        EventManager.RemoveListener(Events.PLAYER_DIED, OnPlayerDied);
    }
    
    private void OnLevelStarted()
    {
        StartCoroutine(WaitAndSpawn());
        StartCoroutine(WaitAndIncreaseSpeed());
    }
    
    private void OnPlayerDied()
    {
        StopAllCoroutines();
    }

    IEnumerator WaitAndSpawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(0.2f, 3f));
            
            var randomEnemy = enemies[Random.Range(0, enemies.Length)];
            
            var spawnPosition = new Vector2(transform.position.x, transform.position.y);
            var spawnedEnemy = Instantiate(randomEnemy, spawnPosition, Quaternion.identity);
            
            spawnedEnemy.Speed = _currentSpeed + Random.Range(-speedVariation, speedVariation);
        
            // set direction based on the camera
            if (_isFacingLeft)
            {
                spawnedEnemy.GoLeft();
            }
            else
            {
                spawnedEnemy.GoRight();
            }
        }
    }
    
    IEnumerator WaitAndIncreaseSpeed()
    {
        while (true)
        {
            yield return new WaitForSeconds(increaseDelay);
            
            _currentSpeed += increaseSpeed;
        }
    }
}
