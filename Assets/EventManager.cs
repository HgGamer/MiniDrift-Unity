using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public static class EventManager 
{
    public static Queue<WSEvent> events = new Queue<WSEvent>();
    public static event UnityAction<string,string> OnPlayerInput;
    public static event UnityAction<string> OnPlayerJoin;
    public static event UnityAction<string> OnPlayerLeave;
    
    public static void PlayerInput(string input,string name) => OnPlayerInput?.Invoke(input,name);
    public static void PlayerJoin(string name) => OnPlayerJoin?.Invoke( name);
    public static void PlayerLeave(string name) => OnPlayerLeave?.Invoke(name);
    
}
