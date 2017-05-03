using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class ServerManager : NetworkManager
{
    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        GameObject player = (GameObject)GameObject.Instantiate(playerPrefab, this.GetSpawnPoint(), Quaternion.identity);

        if (this.numPlayers % 2 == 0)
        {
            // add player to team one
            player.GetComponent<Player>().team = Color.blue;
        }
        else
        {
            // add player to team two
            player.GetComponent<Player>().team = Color.red;
        }

        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
    }

    private Vector3 GetSpawnPoint()
    {
        if (this.playerSpawnMethod == PlayerSpawnMethod.Random && this.startPositions.Count > 0)
        {
            // try to spawn at a random start location
            int index = Random.Range(0, this.startPositions.Count);
            return this.startPositions[index].position;
        }
        if (this.playerSpawnMethod == PlayerSpawnMethod.RoundRobin && this.startPositions.Count > 0)
        {
            Transform startPos = this.startPositions[this.numPlayers % this.startPositions.Count];
            return startPos.position;
        }

        return new Vector3(0, 0, 0);
    }

}