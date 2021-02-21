using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    public static GameManager singleton;

    private void Awake()
    {
        if (singleton == null)
            singleton = this;
        else if (singleton != this)
            Destroy(gameObject);
    }

    [SerializeField] private MainCanvas canvasPrefab;
    [SerializeField] private Player playerPrefab;
    
    
    private Camera camera;
    public MainCanvas mainCanvas;
    public Player player;
    public Transform spawnPoint;

    
    private void Start()
    {
        camera = (new GameObject()).AddComponent<Camera>();
        mainCanvas = Instantiate(canvasPrefab);
        player = Instantiate(playerPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
    }

    
}
