using UnityEngine;

public class CurrentScore : MonoBehaviour
{
    [SerializeField] private SOGameData gameData;

    private TMPro.TextMeshProUGUI scoreText;

    private void Awake()
    {
        scoreText = GetComponent<TMPro.TextMeshProUGUI>();
    }

    public void UpdateCurrentScoreUI()
    {
        scoreText.text = gameData.CurrentScore.ToString();
    }

}
