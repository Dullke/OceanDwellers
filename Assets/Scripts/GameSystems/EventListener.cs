using UnityEngine;
using UnityEngine.Events;

public class EventListener : MonoBehaviour
{
    public SOGameEvent toRespond;
    public UnityEvent Response;

    private void OnEnable() => toRespond.Register(this);
    private void OnDisable() => toRespond.Unregister(this);

    public void OnEventRaised() => Response.Invoke();
}
