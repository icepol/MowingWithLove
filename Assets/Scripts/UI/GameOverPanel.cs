using pixelook;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    [SerializeField] private Text flowersPlantedText;

    private void Start()
    {
        scoreText.text = GameState.Score.ToString();
        flowersPlantedText.text = GameState.FlowersPlanted.ToString();
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            EventManager.TriggerEvent(Events.RESTART_GAME);
        }
    }
}
