using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameplayManager : NetworkBehaviour
{
    [SyncVar]
    public int teamOneScore;

    [SyncVar]
    public int teamTwoScore;

    [SyncVar]
    public int timer = -1;
    private bool timeAlreadyStarted = false;

    [SerializeField]
    AudioSource gameMusic;

    [SerializeField]
    AudioSource ammoSpawnedAudio;

    [Server]
    public void Score(int oldTeamId, int newTeamId)
    {
        if (oldTeamId != newTeamId)
        {
            if (oldTeamId == 0)
            {
                switch (newTeamId)
                {
                    case 1:
                        this.teamOneScore++;
                        break;
                    case 2:
                        this.teamTwoScore++;
                        break;
                }
            }
            else
            {
                switch (newTeamId)
                {
                    case 1:
                        this.teamOneScore += 2;
                        break;
                    case 2:
                        this.teamTwoScore += 2;
                        break;
                }
            }
        }

        Debug.Log("TEAM ONE: " + teamOneScore + " | TEAM TWO: " + teamTwoScore);
    }

    [Server]
    public void ScoreShotPlayer(int teamId, int points)
    {
        switch (teamId)
        {
            case 1:
                this.teamOneScore += points;
                break;
            case 2:
                this.teamTwoScore += points;
                break;
        }
    }

    [Server]
    public void StartTimer()
    {
		GameObject timerObject   = GameObject.FindGameObjectWithTag ("Timer");
		Vector3    timerPosition = new Vector3 ((float)(Camera.main.pixelWidth/2) , (float) (Camera.main.pixelHeight/2), (float) 0);
		timerObject.transform.position = timerPosition;

        this.timer = 150;
        if (this.timeAlreadyStarted)
        {
            CancelInvoke("CmdUpdateTimer");
        }
        InvokeRepeating("CmdUpdateTimer", 0.0f, 1.0f);
        this.timeAlreadyStarted = true;
    }

    [Command]
    public void CmdUpdateTimer ()
    {
        if (this.timer != 0)
        {
            this.timer--;
            if (this.timer % 30 == 0)
            {
                if (GameObject.FindGameObjectWithTag("AmmoSpawn") == null)
                {
                    Debug.Log("Spawn ammo!");
                    GameObject ammoSpawn = (GameObject)Instantiate(Resources.Load("Prefabs/AmmoSpawn"));
                    NetworkServer.Spawn(ammoSpawn);
                    this.RpcAmmoSpawnedEffects();
                }
            }
        }
        else
        {
			// Checking who won
			int winnerTeam = 0;
			if (teamOneScore > teamTwoScore) 
			{
				winnerTeam = 1;
			} 
			else if (teamOneScore < teamTwoScore) 
			{
				winnerTeam = 2;
			}

			GameObject[] players = GameObject.FindGameObjectsWithTag ("Player");
			foreach (GameObject player in players) 
			{
				Player p = (Player) player.GetComponent<Player>();
				p.RpcGameOver (winnerTeam);
			}
			CancelInvoke("CmdUpdateTimer");
        }
    }

    [ClientRpc]
    public void RpcAmmoSpawnedEffects()
    {
        this.ammoSpawnedAudio.Play();
    }
}