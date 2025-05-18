using UnityEngine;

namespace pixelook
{
    public class SoundsManager : MonoBehaviour
    {
        [SerializeField] private Transform targetTransform;
        
        [SerializeField] private AudioClip playerWalkSound;
        [SerializeField] private AudioClip playerFireSound;
        
        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            _audioSource.spatialBlend = 0;
            _audioSource.bypassListenerEffects = true;
        }
        
        private void OnEnable()
        {
            EventManager.AddListener(Events.PLAYER_WALKED_STEP, OnPlayerWalkedStep);
            EventManager.AddListener(Events.PLAYER_FIRED, OnPlayerFired);
        }

        private void OnDisable()
        {
            EventManager.RemoveListener(Events.PLAYER_WALKED_STEP, OnPlayerWalkedStep);
            EventManager.RemoveListener(Events.PLAYER_FIRED, OnPlayerFired);
        }
        
        private void OnPlayerWalkedStep()
        {
            if (playerWalkSound && Settings.IsSfxEnabled)
                _audioSource.PlayOneShot(playerWalkSound);
        }
        
        private void OnPlayerFired()
        {
            if (playerFireSound && Settings.IsSfxEnabled)
                _audioSource.PlayOneShot(playerFireSound);
        }
    }
}