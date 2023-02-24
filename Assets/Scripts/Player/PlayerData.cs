using Sirenix.OdinInspector;

public class PlayerData : ShipData
{
    [UnityEngine.SerializeField] [BoxGroup("Required Data")] private SOGameEvent OnGameOver;

    private void OnDestroy()
    {
        OnGameOver.Raise();
    }
}
