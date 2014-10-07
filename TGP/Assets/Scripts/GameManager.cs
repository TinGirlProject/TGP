using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
	public GameObject player;
    public Transform playerSpawn;
	private GameObject _currentPlayer;
    private CameraScrolling _cam;
	public static List<Timer> s_listOfTimers;
	private Timer _testTimer;
	
	void Awake() 
    {
		s_listOfTimers = new List<Timer>();
		_cam = GetComponent<CameraScrolling>();

        _currentPlayer = GameObject.Find("Player");

        if (!_currentPlayer)
            SpawnPlayer(playerSpawn.position);

        _cam.SetTarget(_currentPlayer.transform);
	}
	
	// Spawn player
	private void SpawnPlayer(Vector3 spawnPos) 
    {
		_currentPlayer = Instantiate(player, spawnPos, Quaternion.identity) as GameObject;
	}

	private void Update() 
    {
		for (int cnt = 0; cnt < s_listOfTimers.Count; ++cnt)
		{
			// if (gamestate != gamestate.pause)
			s_listOfTimers[cnt].UpdateTimer();
		}

		if (!_currentPlayer) 
        {
			if (Input.GetButtonDown("Respawn")) 
            {
                SpawnPlayer(playerSpawn.position);
			}
		}
	}
}
