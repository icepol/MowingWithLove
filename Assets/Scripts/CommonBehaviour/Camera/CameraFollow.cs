using pixelook;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private float smoothX;
    [SerializeField] private float smoothY;
    [SerializeField] private bool forwardOnly;
    
    [SerializeField] private bool keepYOffset = true;
    [SerializeField] private bool lockYAxis = false;
    
    private Rigidbody2D _targetBody;
    private Transform _target;
    private Vector2 _velocity;

    private float _width;
    private float _height;
    
    private float _originalZ;
    private float _offsetY;

    private bool _isFollowing;

    private void Awake()
    {
        EventManager.AddListener(Events.GAME_OVER, OnGameOver);
    }

    private void Start()
    {
        _height = Camera.main.orthographicSize * 2f;
        _width = _height * Camera.main.aspect;
        
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        
        _target = player.transform;
        _targetBody = player.GetComponent<Rigidbody2D>();

        _originalZ = transform.position.z;
        _offsetY = keepYOffset ? _target.position.y : 0f;

        _isFollowing = true;
    }

    private void OnDestroy()
    {
        EventManager.AddListener(Events.GAME_OVER, OnGameOver);
    }

    private void LateUpdate() {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        if (!_isFollowing)
            return;
        
        Vector2 currentPosition = transform.position;
        Vector2 targetPosition = _target.position;

        var velocity = _targetBody.linearVelocity;

        var targetX = targetPosition.x + velocity.x * 0.5f;
        targetX = forwardOnly ? Mathf.Max(targetX, currentPosition.x) : targetX;
        
        var targetY = targetPosition.y + velocity.y * 0.5f;

        var x = Mathf.SmoothDamp(currentPosition.x, targetX, ref _velocity.x, smoothX);
        var y = lockYAxis 
            ? currentPosition.y 
            : Mathf.SmoothDamp(currentPosition.y, targetY, ref _velocity.y, smoothY) - _offsetY;
        
        transform.position = new Vector3(x, y, _originalZ);
    }

    void OnGameOver()
    {
        _isFollowing = false;
    }
}