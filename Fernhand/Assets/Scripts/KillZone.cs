using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour
{
    [SerializeField] private Transform respawnPoint;

    [SerializeField] private float delayBeforeKill = 3f;

    private Coroutine _coroutine;

    IEnumerator Kill()
    {
        yield return new WaitForSeconds(delayBeforeKill);
        GameManager.singleton.player.Die(respawnPoint);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _coroutine = StartCoroutine(Kill());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StopCoroutine(_coroutine);
        }
    }
}
