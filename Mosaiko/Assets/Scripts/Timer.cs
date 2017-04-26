using System;
using UnityEngine;
using UnityEngine.Networking;

public class Timer : NetworkBehaviour
{
	DateTime dateTime;

	void Start()
	{
		DateTime now = DateTime.UtcNow;
		this.dateTime = new DateTime (now.Year, now.Month, now.Day, 0, 2, 30);
		InvokeRepeating("UpdateTimer", 0.0f, 1.0f);
	}

	void OnGUI ()
	{
		GUIStyle style  = new GUIStyle();
		style.fontSize  = 20;
		string niceTime = string.Format("{0}:{1}", this.dateTime.Minute, this.dateTime.Second);
		GUI.Label(new Rect(Screen.width/2,10,250,100), niceTime, style);
	}

	void UpdateTimer ()
	{
		// WE CHECK HERE IF THE GAME IS OVER!!!!!
		this.dateTime = dateTime.AddSeconds (-1);
	}
}

