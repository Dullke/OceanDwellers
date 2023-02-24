using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "Game Data", menuName = "Data/Game Data")]
public class SOGameData : ScriptableObject
{
    private int currentLevelIndex;
    private int currentScore;

    [SerializeField] private int timeBetweenEnemySpawns;
    [SerializeField] private int matchDuration;

    public int CurrentLevelIndex { get { return currentLevelIndex; } private set { currentLevelIndex = value; } }

    public float TimeBetweenEnemySpawns { get { return timeBetweenEnemySpawns; }
                                          set { timeBetweenEnemySpawns = (int)Mathf.Abs(value); } }
    public float MatchDuration { get { return matchDuration; } set { matchDuration = (int)value; } }
    public int CurrentScore { get { return currentScore; } set { currentScore = value; } }



    public void ChangeLevel(int sceneIndex)
    {
        currentScore = 0;
        SceneManager.LoadScene(sceneIndex);
        CurrentLevelIndex = sceneIndex;
    }

    public void ReloadLevel()
    {
        ChangeLevel(currentLevelIndex);
    }

}
