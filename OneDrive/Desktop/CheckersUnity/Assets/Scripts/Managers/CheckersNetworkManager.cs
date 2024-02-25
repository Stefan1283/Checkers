using Mirror;
using Steamworks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckersNetworkManager : NetworkManager
{
    [SerializeField] GameObject gameOverHandlerPrefab, boardPrefab, 
        turnsHandlerPrefab;
    public static event Action ClientOnConnected;

    public List<PlayerNetwork> networkPlayers = new List<PlayerNetwork>();

    public override void OnClientConnect()
    {
        base.OnClientConnect();
        ClientOnConnected?.Invoke();
    }

    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        GameObject playerInstance = Instantiate(playerPrefab);
        NetworkServer.AddPlayerForConnection(conn, playerInstance);
        PlayerNetwork player = playerInstance.GetComponent<PlayerNetwork>();
        networkPlayers.Add(player);
        player.IsWhite = numPlayers == 1;
        player.PlayerDisplayName = player.IsWhite 
            ? "White"
            : "Black";
    }

    public override void OnServerDisconnect(NetworkConnection conn)
    {
        var player = conn.identity.GetComponent<PlayerNetwork>();
        networkPlayers.Remove(player);
        base.OnServerDisconnect(conn);
    }

    public override void OnStopServer()
    {
        networkPlayers.Clear();
    }

    public override void OnClientDisconnect()
    {
        base.OnClientDisconnect();
        SceneManager.LoadScene("Lobby Scene");
        Destroy(gameObject);
    }
}
