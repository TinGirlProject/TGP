using UnityEngine;
using System.Collections;

public class Timer 
{
	private bool _timeComplete;
	private bool _timerActive;
	private float _curTime;
	private float _defaultTime;
	
    /// <summary>
    /// Default time is 1.0 seconds.
    /// </summary>
	public Timer()
	{
		_timeComplete = false;
		_timerActive = false;
		_defaultTime = 1.0f;
		_curTime = _defaultTime;
	}
	
    /// <summary>
    /// Default time is passed in.
    /// </summary>
    /// <param name="defaultTime">What this timer's total time is.</param>
	public Timer(float defaultTime)
	{
		_timeComplete = false;
		_timerActive = false;
		_defaultTime = defaultTime;
		_curTime = _defaultTime;
	}
	
    /// <summary>
    /// Countdown the time for this frame.
    /// </summary>
	public void UpdateTimer() 
	{
		if (_timerActive)
		{
			if (_curTime > 0)
			{
				_curTime -= Time.deltaTime * Time.timeScale;
				//Debug.Log ("Timer time: " + _curTime);
			}
			else
			{
				_timeComplete = true;
				_timerActive = false;
			}
		}
	}

	public void StartTimer()
	{
		_timerActive = true;
	}

	public void PauseTimer()
	{
		_timerActive = false;
	}

	public void ResetTimer()
	{
		_curTime = _defaultTime;
		_timerActive = false;
		_timeComplete = false;
	}

	public bool IsTimeComplete
	{
		get { return _timeComplete; }
	}
	
	public bool IsTimerActive
	{
		get { return _timerActive; }
	}

	public float DefaultTime
	{
		get { return _defaultTime; }
		set { _defaultTime = value; }
	}

	public float CurrentTime
	{
		get { return _curTime; }
	}
}
