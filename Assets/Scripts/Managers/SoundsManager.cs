using UnityEngine;

namespace pixelook
{
    public class SoundsManager : MonoBehaviour
    {
        [SerializeField] private Transform targetTransform;
        
        [SerializeField] private AudioClip playerWalkSound;
        [SerializeField] private AudioClip playerFireSound;
        [SerializeField] private AudioClip[] playerDiedSounds;
        
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
            EventManager.AddListener(Events.PLAYER_DIED, OnPlayerDied);
        }

        private void OnDisable()
        {
            EventManager.RemoveListener(Events.PLAYER_WALKED_STEP, OnPlayerWalkedStep);
            EventManager.RemoveListener(Events.PLAYER_FIRED, OnPlayerFired);
            EventManager.RemoveListener(Events.PLAYER_DIED, OnPlayerDied);
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

        private void OnPlayerDied()
        {
            var randomIndex = Random.Range(0, playerDiedSounds.Length);
            var clip = playerDiedSounds[randomIndex];

            if (clip && Settings.IsSfxEnabled)
                _audioSource.PlayOneShot(clip);
        }
    }
}