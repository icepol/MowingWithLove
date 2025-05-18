using pixelook;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayPanel : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    [SerializeField] private Text flowersPlantedText;

    private void OnEnable()
    {
        EventManager.AddListener(Events.SCORE_CHANGED, OnScoreUpdated);
    }

    private void OnDisable()
    {
        EventManager.RemoveListener(Events.SCORE_CHANGED, OnScoreUpdated);
    }

    private void OnScoreUpdated()
    {
        scoreText.text = GameState.Score.ToString();
        flowersPlantedText.text = $"x{GameState.FlowersPlanted}";
    }
}
