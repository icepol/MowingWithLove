using UnityEngine;

public class Soul : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float speedVariation = 0.1f;

    private void Update()
    {
        transform.Translate(Vector2.up * ((speed + Random.Range(-speedVariation, speedVariation)) * Time.deltaTime));
    }
}
