using UnityEngine;

public class FlowerSpawner : MonoBehaviour
{
    [SerializeField] private Flower[] flowerPrefab;
    
    void Start()
    {
        var flower = Instantiate(flowerPrefab[Random.Range(0, flowerPrefab.Length)], transform.position, Quaternion.identity);
        flower.transform.SetParent(transform);
    }
}
