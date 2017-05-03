using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameplayManager : NetworkBehaviour
{
    [SyncVar]
    int teamOneScore;

    [SyncVar]
    int teamTwoScore;

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

        Debug.Log(teamOneScore);
        Debug.Log(teamTwoScore);
    }
}