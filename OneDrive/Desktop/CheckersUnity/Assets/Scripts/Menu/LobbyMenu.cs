using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyMenu : MonoBehaviour
{
    [SerializeField] Button startGameButton;
    [SerializeField] Text[] playerNameTexts = new Text[2];

    public void StartGame()
    {
        
    }

    private void Start()
    {
        PlayerNetwork.ClientOnInfoUpdated += ClientHandleInfoUpdated;
        PlayerNetwork.AuthorityOnLobbyOwnerStateUpdated += AuthorityHandleLobbyOwnerStateUpdated;
    }

    private void OnDestroy()
    {
        PlayerNetwork.ClientOnInfoUpdated -= ClientHandleInfoUpdated;
        PlayerNetwork.AuthorityOnLobbyOwnerStateUpdated -= AuthorityHandleLobbyOwnerStateUpdated;
    }

    void ClientHandleInfoUpdated()
    {
        List<PlayerNetwork> players = ((CheckersNetworkManager)NetworkManager.singleton).networkPlayers;
        for (int i = 0; i < players.Count; i++)
        {
            playerNameTexts[i].text = players[i].PlayerDisplayName;
        }
        for (int i = players.Count; i < playerNameTexts.Length; i++)
        {
            playerNameTexts[i].text = "Waiting for players...";
        }
    }
    void AuthorityHandleLobbyOwnerStateUpdated(bool status)
    {
        startGameButton.gameObject.SetActive(status);
    }
}
