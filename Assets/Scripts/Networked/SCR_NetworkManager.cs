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
        NetworkServer.AddPlayerForConnection(conn, player);
        
        
    }

    public override void OnServerDisconnect(NetworkConnection conn)
    {
        //TODO: Destroy the ball for the player that leaves
        
        base.OnServerDisconnect(conn);
    }
}
