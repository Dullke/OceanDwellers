using UnityEngine;

public class MatchDuration : MonoBehaviour
{
    [SerializeField] private SOGameData gameData;
    [SerializeField] private SOGameEvent onGameOver;

    private float remainingSeconds;
    private TMPro.TextMeshProUGUI timerText;

    private void Awake()
    {
        Time.timeScale = 1;
        remainingSeconds = gameData.MatchDuration;
        timerText = GetComponent<TMPro.TextMeshProUGUI>();
    }

    private void Update()
    {
        if (remainingSeconds <= 0) EndGame();

        remainingSeconds -= Time.deltaTime;
        timerText.text = ((int)remainingSeconds).ToString() + "s";
    }

    private void EndGame()
    {
        onGameOver.Raise();
        Time.timeScale = 0;
    }

}
