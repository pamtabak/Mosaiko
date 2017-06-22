using Assets.Scripts.Utils;
using Prototype.NetworkLobby;
using UnityEngine;
using UnityEngine.Networking;

public class MosaikoLobbyHook : LobbyHook
{
    public override void OnLobbyServerSceneLoadedForPlayer(NetworkManager manager, GameObject lobbyPlayer, GameObject gamePlayer)
    {
        LobbyPlayer lPlayer = lobbyPlayer.GetComponent<LobbyPlayer>();
        Player gPlayer = gamePlayer.GetComponent<Player>();

        if (lPlayer.playerColor.Equals(TeamColors.TEAM_ONE_COLOR))
        {
            gPlayer.teamId = 1;
        }
        else if (lPlayer.playerColor.Equals(TeamColors.TEAM_TWO_COLOR))
        {
            gPlayer.teamId = 2;
        }
        gPlayer.teamColor = lPlayer.playerColor;
        gPlayer.playerName = lPlayer.playerName;
    }
}
