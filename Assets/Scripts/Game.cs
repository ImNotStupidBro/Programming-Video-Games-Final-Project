using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public CameraController cameraController;
    public PlayerHealthManager playerHealthManager;
    public Map mapPrefab;
    public GameObject playerPrefab;
    private Map currentMap;
    public GameObject player;
    private float playerSpawnX;
    private float playerSpawnY;

    public bool isInitialized = false;
    void Start()
    {
        cameraController = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();
        playerHealthManager = GameObject.FindGameObjectWithTag("GUI_PlayerHealthBar").GetComponent<PlayerHealthManager>();
        LoadLevel();
        isInitialized = true;
    }

    private void LoadLevel()
    {
        SpawnMap();
        SpawnPlayer();
    }

    private void SpawnMap()
    {
        Instantiate(mapPrefab, new Vector3(0, 0, 0), transform.rotation);
        currentMap = mapPrefab;
        cameraController.minX = currentMap.camMinX;
        cameraController.maxX = currentMap.camMaxX;
        cameraController.minY = currentMap.camMinY;
        cameraController.maxY = currentMap.camMaxY;
    }

    private void SpawnPlayer()
    {
        playerSpawnX = currentMap.playerSpawnPoint.transform.position.x;
        playerSpawnY = currentMap.playerSpawnPoint.transform.position.y;
        Vector3 spawnPosition = new Vector3(playerSpawnX, playerSpawnY, 0);
        player = Instantiate(playerPrefab, spawnPosition, Quaternion.identity);
        cameraController.target = player.transform;
        playerHealthManager.currPlayer = player.GetComponent<Player>();
    }
}
