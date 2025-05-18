using pixelook;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    private bool _isFiring;
    private bool _isActive;
    
    private void OnEnable()
    {
        EventManager.AddListener(Events.LEVEL_STARTED, OnLevelStarted);
        EventManager.AddListener(Events.PLAYER_DIED, OnPlayerDied);
    }
    
    private void Update()
    {
        if (!_isActive) return;
        
        if (Input.GetKey(KeyCode.Space))
        {
            if (_isFiring) return;
            
            _isFiring = true;
            
            EventManager.TriggerEvent(Events.PLAYER_FIRED);
            
            // no movement while firing
            return;
        }
        
        _isFiring = false;
        
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            EventManager.TriggerEvent(Events.PLAYER_MOVED_LEFT);
        }
        
        if (Input.GetKey(KeyCode.RightArrow))
        {
            EventManager.TriggerEvent(Events.PLAYER_MOVED_RIGHT);
        }
    }

    private void OnDisable()
    {
        EventManager.RemoveListener(Events.LEVEL_STARTED, OnLevelStarted);
        EventManager.RemoveListener(Events.PLAYER_DIED, OnPlayerDied);
    }
    
    private void OnLevelStarted()
    {
        _isActive = true;
    }

    private void OnPlayerDied()
    {
        _isActive = false;
    }
}
