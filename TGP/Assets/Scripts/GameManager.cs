using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
	public GameObject player;
    public Transform playerSpawn;
	private GameObject currentPlayer;
    private CameraScrolling cam;
	
	void Start() 
    {
		cam = GetComponent<CameraScrolling>();
        SpawnPlayer(playerSpawn.position);

        //cam.SetTarget(player.transform);
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
