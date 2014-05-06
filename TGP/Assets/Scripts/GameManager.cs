using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
	public GameObject player;
    public Transform playerSpawn;
	private GameObject currentPlayer;
    private CameraScrolling cam;
	public static List<Timer> listOfTimers;
	private Timer testTimer;
	
	void Start() 
    {
		listOfTimers = new List<Timer>();
		testTimer = new Timer(5.0f);
		listOfTimers.Add(testTimer);
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
		for (int cnt = 0; cnt < listOfTimers.Count; ++cnt)
		{
			// if (gamestate != gamestate.pause)
			listOfTimers[cnt].UpdateTimer();
		}

		if (!currentPlayer) 
        {
			if (Input.GetButtonDown("Respawn")) 
            {
                SpawnPlayer(playerSpawn.position);
			}
		}

		if (Input.GetKeyUp(KeyCode.I))
		{
			if (!testTimer.IsTimerActive && !testTimer.IsTimeComplete)
				testTimer.StartTimer();
			else if (testTimer.IsTimeComplete)
			{
				testTimer.ResetTimer();
				testTimer.StartTimer();
			}
		}

		if (Input.GetKeyUp(KeyCode.U))
		{
			if (Time.timeScale == 1)
				Time.timeScale = 0.5f;
			else
				Time.timeScale = 1;
		}
	}
}
