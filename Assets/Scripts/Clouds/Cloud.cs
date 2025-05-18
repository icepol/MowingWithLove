using UnityEngine;

public class Cloud : MonoBehaviour
{
    [SerializeField] private float _speed = 1f;
    [SerializeField] private float _speedVariation = 0.1f;
    
    private void Update()
    {
        var speed = _speed + Random.Range(-_speedVariation, _speedVariation);
        transform.Translate(Vector2.left * (speed * Time.deltaTime));
    }
}
