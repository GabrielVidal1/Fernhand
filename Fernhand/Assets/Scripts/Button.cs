using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : Interactible
{
    public Door targetDoor;

    [SerializeField] private bool canCloseDoor;
    
    protected override void Interact()
    {
        print("test");
        targetDoor.Open = !(targetDoor.Open && canCloseDoor);
    }
}
