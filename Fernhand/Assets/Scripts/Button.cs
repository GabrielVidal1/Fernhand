using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class InteractEvent : UnityEvent {}

public class Button : Interactible
{
    [SerializeField] private InteractEvent _event;
    
    protected override void Interact()
    {
        print(name + " Interact");
        _event.Invoke();
    }
}
