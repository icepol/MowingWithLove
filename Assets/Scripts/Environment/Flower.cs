using System.Collections;
using UnityEngine;

public class Flower : MonoBehaviour
{
    [SerializeField] private float maxInitialDelay = 0.25f;
    [SerializeField] private float growthDelay = 1f;
    [SerializeField] private Sprite[] flowerSprites;
    
    private int _currentSpriteIndex;
    
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Start()
    {
        StartCoroutine(Grow());
    }
    
    IEnumerator Grow()
    {
        yield return new WaitForSeconds(Random.Range(0f, maxInitialDelay));
        
        var currentSpriteIndex = 0;

        while (currentSpriteIndex < flowerSprites.Length)
        {
            _spriteRenderer.sprite = flowerSprites[currentSpriteIndex];
            
            currentSpriteIndex++;
            
            yield return new WaitForSeconds(growthDelay);
        }
    }
}
