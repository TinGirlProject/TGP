using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
	public GameObject _player;
    public Transform _playerSpawn;

	private GameObject m_currentPlayer;
    private CameraScrolling m_cam;

	public static List<Timer> s_listOfTimers;
	private Timer m_testTimer;
	
	void Awake() 
    {
		s_listOfTimers = new List<Timer>();
		m_cam = GetComponent<CameraScrolling>();

        m_currentPlayer = GameObject.Find(_player.name);

        if (!m_currentPlayer)
            SpawnPlayer(_playerSpawn.position);

        m_cam.SetTarget(m_currentPlayer.transform);
	}
	
	// Spawn player
	private void SpawnPlayer(Vector3 spawnPos) 
    {
		m_currentPlayer = Instantiate(_player, spawnPos, Quaternion.identity) as GameObject;
	}

	private void Update() 
    {
		for (int cnt = 0; cnt < s_listOfTimers.Count; ++cnt)
		{
			// if (gamestate != gamestate.pause)
			s_listOfTimers[cnt].UpdateTimer();
		}

		if (!m_currentPlayer) 
        {
			if (Input.GetButtonDown("Respawn")) 
            {
                SpawnPlayer(_playerSpawn.position);
			}
		}
	}
}
