using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float speedVariation = 0.5f;
    
    private Enemy _enemy;

    private bool _isFacingLeft;

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
    }

    public bool IsFacingLeft 
    {
        get => _isFacingLeft;
        
        set
        {
            _isFacingLeft = value;
            
            var scale = transform.localScale;
            scale.x = _isFacingLeft ? -1 : 1;
            
            transform.localScale = scale;
        }
    }
    
    void Update()
    {
        if (_enemy.IsDying) return;
        
        var direction = _isFacingLeft ? Vector2.left : Vector2.right;

        transform.Translate(direction * (_enemy.Speed * Time.deltaTime));
    }
}
