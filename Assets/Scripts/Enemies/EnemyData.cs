using UnityEngine.Events;
using Sirenix.OdinInspector;

public class EnemyData : ShipData
{
    [UnityEngine.SerializeField] [BoxGroup("Required Data")] SOGameEvent onDestroy;
    [UnityEngine.SerializeField] [BoxGroup("Required Data")] SOGameData gameData;

    [UnityEngine.SerializeField] [BoxGroup("Ship Properties")] private int score;

    private void OnDestroy()
    {
        onDestroy.Raise();
        gameData.CurrentScore += score;
    }
}
