using System.Collections;
using pixelook;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float hitForce = 0.1f;
    
    [SerializeField] private GameObject soulPrefab;
    [SerializeField] private GameObject flowerSpawnerPrefab;
    
    [SerializeField] private ParticleSystem bloodPrefab;
    
    public float Speed { get; set; }
    
    private EnemyMovement _enemyMovement;
    
    public bool IsDying { get; private set; }
    public bool IsDead { get; private set; }
    
    private Animator _animator;

    private void Awake()
    {
        _enemyMovement = GetComponent<EnemyMovement>();
        _animator = GetComponent<Animator>();
    }

    public void GoLeft()
    {
        _enemyMovement.IsFacingLeft = true;
    }
    
    public void GoRight()
    {
        _enemyMovement.IsFacingLeft = false;
    }

    public void OnAttack()
    {
        if (IsDead) return;

        if (!IsDying)
        {
            Instantiate(soulPrefab, transform.position + PositionAfterHit() + Vector3.up * 0.5f, Quaternion.identity);
            GameState.EnemyKilled++;
        }
        
        GameState.EnemyShots++;
        IsDying = true;

        StopAllCoroutines();
        StartCoroutine(WaitAndDie());
    }

    IEnumerator WaitAndDie()
    {
        _animator.SetBool("IsDying", true);
        
        transform.Translate(PositionAfterHit());
        
        Instantiate(bloodPrefab, transform.position + Vector3.up, Quaternion.identity);
        
        yield return new WaitForSeconds(0.5f);
        
        IsDead = true;
        
        EventManager.TriggerEvent(Events.ENEMY_DIED);
        
        Instantiate(flowerSpawnerPrefab, transform.position, Quaternion.identity);
        GameState.FlowersPlanted++;
        
        Destroy(gameObject);
    }

    Vector3 PositionAfterHit()
    {
        return _enemyMovement.IsFacingLeft ? Vector3.right * hitForce : Vector3.left * hitForce;
    }
}
