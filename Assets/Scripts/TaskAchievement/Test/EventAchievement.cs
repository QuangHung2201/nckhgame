using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

// sự kiện trong game
public enum EventType
{
    CheckDaily1,
    CheckDaily3,
    CheckDaily4,
    CheckMonthly3,
    CheckMonthly4
}

public static class EventAchievement
{
    private static Dictionary<EventType, Action> events = new Dictionary<EventType, Action>();

    // event có bool
    private static Dictionary<EventType, Action<bool>> eventsBool = new Dictionary<EventType, Action<bool>>();

    // đăng ký sự kiện với hành động tương ứng
    public static void Subscribe(EventType type, Action action)
    {
        if (!events.ContainsKey(type))
            events[type] = action;
        else
            events[type] += action;
    }

    public static void Subscribe(EventType type, Action<bool> action)
    {
        if (!eventsBool.ContainsKey(type))
            eventsBool[type] = action;
        else
            eventsBool[type] += action;
    }

    // hủy đăng ký sự kiện
    public static void Unsubscribe(EventType type, Action action)
    {
        if (events.ContainsKey(type))
            events[type] -= action;
    }

    public static void Unsubscribe(EventType type, Action<bool> action)
    {
        if (eventsBool.ContainsKey(type))
            eventsBool[type] -= action;
    }

    // kích hoạt sự kiện
    public static void Trigger(EventType type)
    {
        if (events.ContainsKey(type))
            events[type]?.Invoke();
    }

    public static void Trigger(EventType type, bool value)
    {
        if (eventsBool.ContainsKey(type))
            eventsBool[type]?.Invoke(value);
    }
}
