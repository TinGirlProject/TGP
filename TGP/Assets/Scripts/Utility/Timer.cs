using UnityEngine;
using System.Collections;

public class Timer 
{
	private bool m_timeComplete;
	private bool m_timerActive;
	private float m_curTime;
	private float m_defaultTime;
	
	public Timer()
	{
		m_timeComplete = false;
		m_timerActive = false;
		m_defaultTime = 1.0f;
		m_curTime = m_defaultTime;
	}
	
	public Timer(float defaultTime)
	{
		m_timeComplete = false;
		m_timerActive = false;
		m_defaultTime = defaultTime;
		m_curTime = m_defaultTime;
	}
	
	public void UpdateTimer() 
	{
		if (m_timerActive)
		{
			if (m_curTime > 0)
			{
				m_curTime -= Time.deltaTime * Time.timeScale;
				//Debug.Log ("Timer time: " + m_curTime);
			}
			else
			{
				m_timeComplete = true;
				m_timerActive = false;
			}
		}
	}

	public void StartTimer()
	{
		m_timerActive = true;
	}

	public void PauseTimer()
	{
		m_timerActive = false;
	}

	public void ResetTimer()
	{
		m_curTime = m_defaultTime;
		m_timerActive = false;
		m_timeComplete = false;
	}

	public bool IsTimeComplete
	{
		get { return m_timeComplete; }
	}
	
	public bool IsTimerActive
	{
		get { return m_timerActive; }
	}

	public float DefaultTime
	{
		get { return m_defaultTime; }
		set { m_defaultTime = value; }
	}

	public float CurrentTime
	{
		get { return m_curTime; }
	}
}
