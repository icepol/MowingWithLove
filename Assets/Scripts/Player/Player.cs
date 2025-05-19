using System.Linq;
using DG.Tweening;
using pixelook;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private LayerMask enemyLayerMask;

    [SerializeField] private float undergroundPositionOffset = 5f;
    [SerializeField] private float introDuration = 1f;
    [SerializeField] private float outroDuration = 2.5f;
    
    private Animator _animator;
    
    private float _cameraHalfWidth;
    private Transform _cameraTransform;
    private bool _isOutroInProgress;
    private bool _isActive;
    
    public bool IsFacingRight => transform.localScale.x > 0;
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    
    private void Start()
    {
        _cameraHalfWidth = Camera.main.orthographicSize * Camera.main.aspect;
        _cameraTransform = Camera.main.transform;
        
        // Set initial position to underground
        transform.Translate(Vector2.down * undergroundPositionOffset);
    }

    private void OnEnable()
    {
        EventManager.AddListener(Events.GAME_STARTED, OnGameStarted);
        EventManager.AddListener(Events.LEVEL_STARTED, OnLevelStarted);
        EventManager.AddListener(Events.PLAYER_FIRED, OnPlayerFired);
        EventManager.AddListener(Events.PLAYER_DIED, OnPlayerDied);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!_isActive) return;
        
        var enemy = other.GetComponent<Enemy>();
        if (!enemy) return;
        
        if (enemy.IsDying || enemy.IsDead) return;

        EventManager.TriggerEvent(Events.PLAYER_DIED);
    }

    private void OnDisable()
    {
        EventManager.RemoveListener(Events.GAME_STARTED, OnGameStarted);
        EventManager.RemoveListener(Events.LEVEL_STARTED, OnLevelStarted);
        EventManager.RemoveListener(Events.PLAYER_FIRED, OnPlayerFired);
        EventManager.RemoveListener(Events.PLAYER_DIED, OnPlayerDied);
    }

    private void OnGameStarted()
    {
        Intro();
    }

    private void OnLevelStarted()
    {
        _isActive = true;
    }

    private void OnPlayerDied()
    {
        _isActive = false;
        
        Outro();
    }

    private void OnPlayerFired()
    {
        _animator.SetTrigger("IsAttacking");
        
        var direction = IsFacingRight ? Vector2.right : Vector2.left;
        
        var distance = IsFacingRight
            ? _cameraTransform.position.x + _cameraHalfWidth - transform.position.x
            : transform.position.x - (_cameraTransform.position.x - _cameraHalfWidth);
        
        var hits = Physics2D.RaycastAll(transform.position + Vector3.up * 0.25f, direction, distance, enemyLayerMask);
        var sortedHits = hits.OrderBy(hit => hit.distance).Take(3).ToArray();
        
        foreach (var hit in sortedHits)
        {
            var enemy = hit.collider.GetComponent<Enemy>();
            var wasDying = enemy.IsDying;

            enemy.OnAttack();                
            
            if (!wasDying)
            {
                // hit the next enemy only if the first one is dying
                break;
            }
        }
    }

    void Intro()
    {
        transform.DOMoveY(transform.position.y + undergroundPositionOffset, introDuration).OnComplete(
            () => 
            {
                EventManager.TriggerEvent(Events.LEVEL_STARTED);
            });
    }
    
    void Outro()
    {
        transform.DOMoveY(transform.position.y - undergroundPositionOffset, outroDuration).OnComplete(
            () =>
            {
                EventManager.TriggerEvent(Events.GAME_OVER);                
            });        
    }
}
