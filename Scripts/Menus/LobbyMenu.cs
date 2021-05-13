// Program
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Mirror;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LobbyMenu : MonoBehaviour
{
    // Gets Objects in the lobby menu
    [SerializeField] private GameObject lobbyUI = null;
    [SerializeField] private Button startGameButton = null;
    [SerializeField] private TMP_Text[] playerNameTexts = new TMP_Text[4];
    [SerializeField] public Transform Spawn;
    [SerializeField] public GameObject[] WeaponButtons = new GameObject[4];


    private void Start()
    {
        // Sets the clients connected, the host is, and updates the clients information
        MyNetworkManager.ClientOnConnected += HandleClientConnected;
        MyNetworkPlayer.AuthorityOnPartyOwnerStateUpdated += AuthorityHandlePartyOwnerStateUpdated;
        MyNetworkPlayer.ClientOnInfoUpdated += ClientHandleInfoUpdated;
    }

    private void OnDestroy()
    {
        // Removes client, host, and information related to the client
        MyNetworkManager.ClientOnConnected -= HandleClientConnected;
        MyNetworkPlayer.AuthorityOnPartyOwnerStateUpdated -= AuthorityHandlePartyOwnerStateUpdated;
        MyNetworkPlayer.ClientOnInfoUpdated -= ClientHandleInfoUpdated;
    }

    private void ClientHandleInfoUpdated()
    {
        // Gets the active list of players connected to the active player
        List<MyNetworkPlayer> players = ((MyNetworkManager)NetworkManager.singleton).Players;

        // For the total amount of players it will set their name in the lobby screen and if one leaves it will reset the text value.
        for (int i = 0; i < players.Count; i++)
        {
            playerNameTexts[i].text = players[i].GetClientName();
        }

        for (int i = players.Count; i < playerNameTexts.Length; i++)
        {
            playerNameTexts[i].text = "Waiting For Player...";
        }

        
    }

    // When a client connects it will set the lobby to active
    private void HandleClientConnected()
    {
        lobbyUI.SetActive(true);
    }

    // If player is the host the start game button will be interactible
    public void AuthorityHandlePartyOwnerStateUpdated (bool state)
    {
        startGameButton.gameObject.SetActive(state);
    }

    // When the host starts the game it will call the start game function from the players network
    public void StartGame()
    {
        NetworkClient.connection.identity.GetComponent<MyNetworkPlayer>().CmdStartGame();
    }

    // If player leaves the lobby runs function
    public void LeaveLobby()
    {
        // Gets if player was active host
        if (NetworkServer.active && NetworkClient.isConnected)
        {
            // Closes Lobby
            MyNetworkManager.singleton.StopHost();
        }
        else
        {
            // Player leaves lobby and changes scene but lobby stays intact
            MyNetworkManager.singleton.StopClient();

            SceneManager.LoadScene(0);
        }
    }
}
