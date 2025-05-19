using pixelook;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float fireBackForce = 0.5f;
    
    public float CurrentSpeed => _isMoving ? speed * transform.localScale.x : 0f;

    private bool _isActive;
    private bool _isMoving;
    private float _cameraHalfWidth;
    private Transform _cameraTransform;
    private Transform _playerTransform;
    
    private Animator _animator;
    private Player _player;
    
    private Vector3 _lastPosition;
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _player = GetComponent<Player>();
    }

    private void OnEnable()
    {
        EventManager.AddListener(Events.PLAYER_MOVED_LEFT, OnPlayerMovedLeft);
        EventManager.AddListener(Events.PLAYER_MOVED_RIGHT, OnPlayerMovedRight);
        EventManager.AddListener(Events.PLAYER_FIRED, OnPlayerFired);
        EventManager.AddListener(Events.LEVEL_STARTED, OnLevelStarted);
        EventManager.AddListener(Events.PLAYER_DIED, OnPlayerDied);
    }

    private void Start()
    {
        _cameraHalfWidth = Camera.main.orthographicSize * Camera.main.aspect;
        _cameraTransform = Camera.main.transform;
        
        _lastPosition = transform.position;
    }

    private void FixedUpdate()
    {
        if (!_isActive) return;
        
        var xDistance = transform.position.x - _lastPosition.x;

        transform.localScale = xDistance switch
        {
            > 0.01f => new Vector2(1, 1),
            < -0.01f => new Vector2(-1, 1),
            _ => transform.localScale
        };

        var distance = Vector2.Distance(transform.position, _lastPosition);
        
        _isMoving = distance > 0.01f;
        _animator.SetBool("IsMoving", _isMoving);
        
        _lastPosition = transform.position;
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener(Events.PLAYER_MOVED_LEFT, OnPlayerMovedLeft);
        EventManager.RemoveListener(Events.PLAYER_MOVED_RIGHT, OnPlayerMovedRight);
        EventManager.RemoveListener(Events.PLAYER_FIRED, OnPlayerFired);
        EventManager.RemoveListener(Events.LEVEL_STARTED, OnLevelStarted);
        EventManager.RemoveListener(Events.PLAYER_DIED, OnPlayerDied);
    }

    private void OnPlayerMovedLeft()
    {
        var newXPosition = Mathf.Max(
            transform.position.x - speed * Time.deltaTime,
            _cameraTransform.position.x - _cameraHalfWidth
            );
        
        transform.position= new Vector2(newXPosition, transform.position.y);
    }

    private void OnPlayerMovedRight()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }
    
    private void OnLevelStarted()
    {
        _isActive = true;
    }
    
    private void OnPlayerDied()
    {
        _isActive = false;
        
        _animator.SetBool("IsMoving", false);
    }

    private void OnPlayerFired()
    {
        var positionAfterFire = _player.IsFacingRight ? Vector3.left * fireBackForce : Vector3.right * fireBackForce;
        
        transform.Translate(positionAfterFire);
        
        // update the position to avoid rotating towards the movement
        _lastPosition = transform.position;
    }

    public void OnPlayerWalkedStep()
    {
        EventManager.TriggerEvent(Events.PLAYER_WALKED_STEP);
    }
}
