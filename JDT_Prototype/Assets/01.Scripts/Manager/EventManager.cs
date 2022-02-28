using System;
using System.Collections.Generic;
using System.Diagnostics;

public class EventManager
{
    private static Dictionary<string, Delegate> eventDictionary = new Dictionary<string, Delegate>();

    /// <summary>
    /// EventManager 에 해당 이벤트를 추가합니다.
    /// </summary>
    /// <param name="eventName"></param>
    /// <param name="addEvent"></param>
    public static void AddEvent(in string eventName, in Delegate addEvent)
    {
        if (eventDictionary.TryGetValue(eventName, out Delegate thisEvent))
        {
            thisEvent = addEvent;
            Debug.Print(eventName + " : " + "이 이름의 함수는 이미 존재합니다.");
        }
        else
        {
            eventDictionary.Add(eventName, addEvent);
        }
    }

    /// <summary>
    /// EventManager 에서 해당 이름의 이벤트를 삭제합니다.
    /// </summary>
    /// <param name="eventName"></param>
    public static void RemoveEvent(in string eventName)
    {
        if (eventDictionary.ContainsKey(eventName))
        {
            eventDictionary.Remove(eventName);
        }
        else
        {
            Debug.Print("제거할 함수가 없습니다. " + "(" + eventName + ")");
        }
    }

    /// <summary>
    /// EventManager 에서 해당 이름의 이벤트를 실행합니다.
    /// </summary>
    /// <param name="eventName"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    public static dynamic TriggerEvent(in string eventName, params dynamic[] param)
    {
        dynamic value = default(dynamic);

        if (eventDictionary.TryGetValue(eventName, out Delegate triggerEvent))
        {
            value = triggerEvent.DynamicInvoke(param);
        }
        else
        {
            Debug.Print("실행할 함수가 없습니다. " + "(" + eventName + ")");
        }

        return value;
    }

    /// <summary>
    /// EventManager 에 있는 모든 이벤트를 출력합니다. (구현 중..)
    /// </summary>
    public static void ShowEvents()
    {
        Console.WriteLine("-----------<<Events Dictionary>>-----------");
        foreach (var item in eventDictionary)
        {
            Console.WriteLine(item.Key + " : " + item.Value.GetType().Namespace.GetType());
        }
        Console.WriteLine("-------------------------------------------");
    }

    /* 장점: 1. 모든 이벤트 관리가 용이함
     *       2. 코드가 간결해짐 
     *       3. 확장성을 갖음
     *       4. 범용성이 넓음
    */
    /* 단점: 1. 일반 함수를 매개변수로 쓸 수 없으며, Action 혹은 Func 타입 변수에 할당하여 사용해야 함 (추천 => 람다 식) 
     *       2. 람다 식 사용 시 일반 함수보다 상대적으로 성능이 떨어짐 (모바일 한정)
    */
    /* 패치 사항: 1. 일반 함수를 매개변수를 받을 수 있게끔 해야함
     *           2. 사전에 있는 이벤트의 정보를 알 수 있어야함 (예. class 이름)
     */
}
