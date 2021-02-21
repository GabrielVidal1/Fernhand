using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator _animator;

    private bool _open;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Open(true);
    }
    
    private void OnTriggerExit(Collider other)
    {
        Open(false);
    }

    public void Open(bool value)
    {
        _open = value;
        _animator.SetBool("Open", value);
    }

    public void ToggleOpen()
    {
        Open(!_open);
    }
}
