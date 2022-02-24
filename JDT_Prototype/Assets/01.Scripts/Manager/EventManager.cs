using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class EventManager
{
    private static Hashtable eventHashtable = new Hashtable();
    private static Dictionary<string, dynamic> eventDictionary = new Dictionary<string, dynamic>( );

    //public static void AddEvent(string eventName, Action addEvent)
    //{
    //    dynamic thisEvent;

    //    if (eventDictionary.TryGetValue(eventName, out thisEvent))
    //    {
    //        thisEvent += addEvent;
    //        eventDictionary[eventName] = thisEvent;
    //    }
    //    else
    //    {
    //        thisEvent += addEvent;
    //        eventDictionary.Add(eventName, thisEvent);
    //    }
    //}
    public static void AddEvent_Action(string eventName, Action addEvent)
    {
        if (eventHashtable.ContainsKey(eventName))
        {
            eventHashtable[eventName] = addEvent;
        }
        else
        {
            eventHashtable.Add(eventName, addEvent);
        }
    }
    public static void AddEvent_Action(string eventName, Action<int> addEvent)
    {
        if (eventHashtable.ContainsKey(eventName))
        {
            eventHashtable[eventName] = addEvent;
        }
        else
        {
            eventHashtable.Add(eventName, addEvent);
        }
    }
    public static void AddEvent_Action(string eventName, Action<bool> addEvent)
    {
        if (eventHashtable.ContainsKey(eventName))
        {
            eventHashtable[eventName] = addEvent;
        }
        else
        {
            eventHashtable.Add(eventName, addEvent);
        }
    }
    public static void AddEvent_Action(string eventName, Action<Action> addEvent)
    {
        if (eventHashtable.ContainsKey(eventName))
        {
            eventHashtable[eventName] = addEvent;
        }
        else
        {
            eventHashtable.Add(eventName, addEvent);
        }
    }
    public static void AddEvent_Action(string eventName, Action<float, float> addEvent)
    {
        if (eventHashtable.ContainsKey(eventName))
        {
            eventHashtable[eventName] = addEvent;
        }
        else
        {
            eventHashtable.Add(eventName, addEvent);
        }
    }
    public static void AddEvent_Function(string eventName, Func<IEnumerator> addEvent)
    {
        if (eventHashtable.ContainsKey(eventName))
        {
            eventHashtable[eventName] = addEvent;
        }
        else
        {
            eventHashtable.Add(eventName, addEvent);
        }
    }
    public static void AddEvent_Function(string eventName, Func<Tween> addEvent)
    {
        if (eventHashtable.ContainsKey(eventName))
        {
            eventHashtable[eventName] = addEvent;
        }
        else
        {
            eventHashtable.Add(eventName, addEvent);
        }
    }
    public static void AddEvent_Function(string eventName, Func<Vector3> addEvent)
    {
        if (eventHashtable.ContainsKey(eventName))
        {
            eventHashtable[eventName] = addEvent;
        }
        else
        {
            eventHashtable.Add(eventName, addEvent);
        }
    }
    public static void AddEvent_Function(string eventName, Func<int> addEvent)
    {
        if (eventHashtable.ContainsKey(eventName))
        {
            eventHashtable[eventName] = addEvent;
        }
        else
        {
            eventHashtable.Add(eventName, addEvent);
        }
    }
    public static void AddEvent_Function(string eventName, Func<float> addEvent)
    {
        if (eventHashtable.ContainsKey(eventName))
        {
            eventHashtable[eventName] = addEvent;
        }
        else
        {
            eventHashtable.Add(eventName, addEvent);
        }
    }

    public static void RemoveEvent(string eventName)
    {
        if (eventHashtable.ContainsKey(eventName))
        {
            eventHashtable.Remove(eventName);
        }
        else
        {
            Debug.Log("제거할 함수가 없습니다.");
        }
    }

    public static void TriggerEvent_Action(string eventName)
    {
        Action action;

        if (eventHashtable.ContainsKey(eventName))
        {
            if (eventHashtable[eventName] is Action)
            {
                action = (Action)eventHashtable[eventName];
                action?.Invoke();
            }
        }
    }
    public static void TriggerEvent_Action(string eventName, GameObject obj)
    {
        Action<GameObject> action;

        if (eventHashtable.ContainsKey(eventName))
        {
            if (eventHashtable[eventName] is Action<GameObject>)
            {
                action = (Action<GameObject>)eventHashtable[eventName]; // 언박싱
                action?.Invoke(obj);
            }
        }
    }
    public static void TriggerEvent_Action(string eventName, int param)
    {
        Action<int> action;

        if (eventHashtable.ContainsKey(eventName))
        {
            if (eventHashtable[eventName] is Action<int>)
            {
                action = (Action<int>)eventHashtable[eventName]; // 언박싱
                action?.Invoke(param);
            }
        }
    }
    public static void TriggerEvent_Action(string eventName, float moveX, float moveY)
    {
        Action<float, float> action;

        if (eventHashtable.ContainsKey(eventName))
        {
            if (eventHashtable[eventName] is Action<float, float>)
            {
                action = (Action<float, float>)eventHashtable[eventName]; // 언박싱
                action?.Invoke(moveX, moveY);
            }
        }
    }
    public static void TriggerEvent_Action(string eventName, bool param)
    {
        Action<bool> action;

        if (eventHashtable.ContainsKey(eventName))
        {
            if (eventHashtable[eventName] is Action<bool>)
            {
                action = (Action<bool>)eventHashtable[eventName]; // 언박싱
                action?.Invoke(param);
            }
        }
    }

    // Int 타입
    public static int TriggerEvent_Int(string eventName)
    {
        Func<int> func;
        int value = 0;

        if (eventHashtable.ContainsKey(eventName))
        {
            if (eventHashtable[eventName] is Func<int>)
            {
                func = (Func<int>)eventHashtable[eventName];
                value = func.Invoke();
            }
        }

        return value;
    }

    // Float 타입
    public static float TriggerEvent_Float(string eventName)
    {
        Func<float> func;
        float value = 0f;

        if (eventHashtable.ContainsKey(eventName))
        {
            if (eventHashtable[eventName] is Func<float>)
            {
                func = (Func<float>)eventHashtable[eventName];
                value = func.Invoke();
            }
        }

        return value;
    }

    // Vector3 타입
    public static Vector3 TriggerEvent_Vector3(string eventName)
    {
        Func<Vector3> func;
        Vector3 tween = new Vector3(0, 0, 0);

        if (eventHashtable.ContainsKey(eventName))
        {
            if (eventHashtable[eventName] is Func<Vector3>)
            {
                func = (Func<Vector3>)eventHashtable[eventName];
                tween = func.Invoke();
            }
        }

        return tween;
    }

    // Tween 타입
    public static Tween TriggerEvent_Tween(string eventName)
    {
        Func<Tween> func;
        Tween tween = null;

        if (eventHashtable.ContainsKey(eventName))
        {
            if (eventHashtable[eventName] is Func<Tween>)
            {
                func = (Func<Tween>)eventHashtable[eventName];
                tween = func.Invoke();
            }
        }

        return tween;
    }

    //public static T TriggerEvent<T>(string eventName) 
    //{
    //    Action action;

    //    if (eventHashtable.ContainsKey(eventName))
    //    {
    //        if (eventHashtable[eventName] is Action)
    //        {
    //            action = (Action)eventHashtable[eventName];
    //            action?.Invoke();
    //        }
    //    }

    //    return default(T);
    //}
}