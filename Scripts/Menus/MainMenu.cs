using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;
using Mirror;

public class MainMenu : MonoBehaviour
{
    // Variable for Steam and main page
    [SerializeField] private GameObject LandingPagePanel = null;
    [SerializeField] private bool useSteam = false;

    // Calls from events of other programs and pulls variables from them
    protected Callback<LobbyCreated_t> lobbyCreated;
    protected Callback<GameLobbyJoinRequested_t> gameLobbyJoinRequested;
    protected Callback<LobbyEnter_t> lobbyEntered;

    private void Start()
    {
        // gets if player isn't using steam
        if (!useSteam) { return; }

        // Sets variables to the events callback
        lobbyCreated = Callback<LobbyCreated_t>.Create(OnLobbyCreated);
        gameLobbyJoinRequested = Callback<GameLobbyJoinRequested_t>.Create(OnGameLobbyJoinRequested);
        lobbyEntered = Callback<LobbyEnter_t>.Create(OnLobbyEntered);
    }

    public void HostLobby()
    {
        // Sets main page to inactive
        LandingPagePanel.SetActive(false);

        // if using steam create lobby in steam
        if (useSteam)
        {
            SteamMatchmaking.CreateLobby(ELobbyType.k_ELobbyTypeFriendsOnly, 4);
        }

        // Call StartHost function
        NetworkManager.singleton.StartHost();

    }

    private void OnLobbyCreated(LobbyCreated_t callback)
    {
        // Sets Lobby panel to active
        if(callback.m_eResult != EResult.k_EResultOK)
        {
            LandingPagePanel.SetActive(true);
            return;
        }

        // Calls StartHost function
        NetworkManager.singleton.StartHost();

        // Sets Steam variables
        SteamMatchmaking.SetLobbyData(new CSteamID(callback.m_ulSteamIDLobby), "HostAddress", SteamUser.GetSteamID().ToString());
    }

    private void OnGameLobbyJoinRequested(GameLobbyJoinRequested_t callback)
    {
        // Adds player Steam Lobby
        SteamMatchmaking.JoinLobby(callback.m_steamIDLobby);
    }

    private void OnLobbyEntered(LobbyEnter_t callback)
    {
        // Gets if Server is active
        if(NetworkServer.active) { return; }

        // Sets host adress variable
        string hostAddress = SteamMatchmaking.GetLobbyData(new CSteamID(callback.m_ulSteamIDLobby), "HostAddress");

        // Sets network address to host address and starts client
        NetworkManager.singleton.networkAddress = hostAddress;
        NetworkManager.singleton.StartClient();

        // Sets main page to inactive
        LandingPagePanel.SetActive(false);
    }

}
