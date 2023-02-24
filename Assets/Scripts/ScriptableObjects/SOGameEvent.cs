using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Event", menuName = "Systems/Events")]
public class SOGameEvent : ScriptableObject
{
    private readonly List<EventListener> Listeners = new List<EventListener>();

    public void Raise()
    {
        for (int i = Listeners.Count - 1; i >= 0; i--)
            Listeners[i].OnEventRaised();
    }

    public void Register(EventListener listener) => Listeners.Add(listener);
    public void Unregister(EventListener listener) => Listeners.Remove(listener);
}
