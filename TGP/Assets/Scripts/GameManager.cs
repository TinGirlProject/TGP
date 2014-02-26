using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
	public GameObject player;
    public Transform playerSpawn;
	private GameObject currentPlayer;
	private GameCamera cam;
	
	void Start() 
    {
		cam = GetComponent<GameCamera>();
        SpawnPlayer(playerSpawn.position);
	}
	
	// Spawn player
	private void SpawnPlayer(Vector3 spawnPos) 
    {
		currentPlayer = Instantiate(player, spawnPos, Quaternion.identity) as GameObject;
		cam.SetTarget(currentPlayer.transform);
	}

	private void Update() 
    {
		if (!currentPlayer) 
        {
			if (Input.GetButtonDown("Respawn")) 
            {
                SpawnPlayer(playerSpawn.position);
			}
		}
	}
}
