using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEditor;
using UnityEngine;

public class SCR_NetworkManager : NetworkManager
{
    [SerializeField] private Transform playerOneSpawn;
    [SerializeField] private Transform playerTwoSpawn;

    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        // Set player spawn on server connect
        Transform start = numPlayers == 0 ? playerOneSpawn : playerTwoSpawn;
        GameObject player = Instantiate(playerPrefab, start.position, start.rotation);
        player.name = $"{playerPrefab.name} [connId={conn.connectionId}]";
        NetworkServer.AddPlayerForConnection(conn, player);
    }
}
