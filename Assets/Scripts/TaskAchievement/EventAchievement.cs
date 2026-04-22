using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

// sự kiện trong game
public enum EventType
{
    AddDaily1,
    AddDaily2,
    AddDaily3,
    AddDaily4,
    AddDaily5,
    AddMonthly1,
    AddMonthly2,
    AddMonthly3,
    AddMonthly4,
    AddMonthly5
}

public static class EventAchievement
{
    private static Dictionary<EventType, Delegate> events = new();

    public static void Subscribe(EventType type, Action action)
    {
        if (events.TryGetValue(type, out var existing))
            events[type] = (Action)existing + action;
        else
            events[type] = action;
    }

    public static void Subscribe<T>(EventType type, Action<T> action)
    {
        if (events.TryGetValue(type, out var existing))
            events[type] = (Action<T>)existing + action;
        else
            events[type] = action;
    }

    public static void Unsubscribe(EventType type, Action action)
    {
        if (!events.TryGetValue(type, out var existing)) return;

        var newAction = (Action)existing - action;

        if (newAction == null)
            events.Remove(type);
        else
            events[type] = newAction;
    }

    public static void Unsubscribe<T>(EventType type, Action<T> action)
    {
        if (!events.TryGetValue(type, out var existing)) return;

        var newAction = (Action<T>)existing - action;

        if (newAction == null)
            events.Remove(type);
        else
            events[type] = newAction;
    }

    public static void Trigger(EventType type)
    {
        if (events.TryGetValue(type, out var del))
            (del as Action)?.Invoke();
    }

    public static void Trigger<T>(EventType type, T value)
    {
        if (events.TryGetValue(type, out var del))
            (del as Action<T>)?.Invoke(value);
    }
}
