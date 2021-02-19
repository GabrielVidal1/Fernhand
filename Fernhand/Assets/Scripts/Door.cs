using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator _animator;
    private static readonly int Open = Animator.StringToHash("Open");

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            _animator.SetBool(Open, true);
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            _animator.SetBool(Open, false);
    }
}
