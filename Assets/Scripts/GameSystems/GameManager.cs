using Sirenix.OdinInspector;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] [BoxGroup("Required Data")] private SOGameData gameData;
    [SerializeField] [TitleGroup("Required Data/Events")] private SOGameEvent onGameStart;
    [SerializeField] [TitleGroup("Required Data/Components")] private TMPro.TextMeshProUGUI finalScoreText;

    private void Start()
    {
        onGameStart.Raise();
    }

    public void UpdateFinalScoreText()
    {
        finalScoreText.text = gameData.CurrentScore.ToString();
    }
}
