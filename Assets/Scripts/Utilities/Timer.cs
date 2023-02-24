using UnityEngine;

public class Timer : MonoBehaviour
{
    private string label;
    private float time;

    public string Label { get { return label; } set { label = value; } }
    public float Time { get { return time; } set { time = value; } }

    private void Start()
    {
        Destroy(this, time);
    }
}
