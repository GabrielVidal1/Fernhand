using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator _animator;

    private bool _open;
    public bool Open
    {
        get => _open;
        set
        {
            _animator.SetBool("Open", value);
            _open = value;
        }
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Open = true;
    }
    
    private void OnTriggerExit(Collider other)
    {
        Open = false;
    }
}
