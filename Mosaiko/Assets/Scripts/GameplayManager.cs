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
    public int timer;
    private bool timeAlreadyStarted = false;

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

        Debug.Log("TEAM ONE: " + teamOneScore);
        Debug.Log("TEAM TWO: " + teamTwoScore);
    }

    public void StartTimer()
    {
        this.timer = 150;
        if (this.timeAlreadyStarted)
        {
            CancelInvoke("CmdUpdateTime");
        }
        InvokeRepeating("CmdUpdateTimer", 0.0f, 1.0f);
        this.timeAlreadyStarted = true;
    }

    [Command]
    public void CmdUpdateTimer()
    {
        if (this.timer != 0)
        {
            this.timer--;
        }
        else
        {
            RpcGameOver (netId);
        }
    }

    [ClientRpc]
    public void RpcGameOver (NetworkInstanceId networkId)
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible   = true;

        if (isLocalPlayer)
        {
            if (netId == networkId)
            {
                // you won
            }
            else
            {
                // you lost
            }
        }
    }
}