using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Mirror;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LobbyMenu : MonoBehaviour
{
    [SerializeField] private GameObject lobbyUI = null;
    [SerializeField] private Button startGameButton = null;
    [SerializeField] private TMP_Text[] playerNameTexts = new TMP_Text[4];
    public GameObject[] WeaponButtons = new GameObject[4];


    private void Start()
    {
        MyNetworkManager.ClientOnConnected += HandleClientConnected;
        MyNetworkPlayer.AuthorityOnPartyOwnerStateUpdated += AuthorityHandlePartyOwnerStateUpdated;
        MyNetworkPlayer.ClientOnInfoUpdated += ClientHandleInfoUpdated;
    }

    private void OnDestroy()
    {
        MyNetworkManager.ClientOnConnected -= HandleClientConnected;
        MyNetworkPlayer.AuthorityOnPartyOwnerStateUpdated -= AuthorityHandlePartyOwnerStateUpdated;
        MyNetworkPlayer.ClientOnInfoUpdated -= ClientHandleInfoUpdated;
    }

    private void ClientHandleInfoUpdated()
    {
        List<MyNetworkPlayer> players = ((MyNetworkManager)NetworkManager.singleton).Players;

        for (int i = 0; i < players.Count; i++)
        {
            playerNameTexts[i].text = players[i].GetClientName();
        }

        //for (int i = players.Count; i < playerNameTexts.Length; i++)
        //{
            //playerNameTexts[i].text = "Waiting For Player...";
        //}

        
    }

    private void HandleClientConnected()
    {
        lobbyUI.SetActive(true);
    }


    public void AuthorityHandlePartyOwnerStateUpdated (bool state)
    {
        startGameButton.gameObject.SetActive(state);
    }

    public void StartGame()
    {
        NetworkClient.connection.identity.GetComponent<MyNetworkPlayer>().CmdStartGame();
    }

    public void LeaveLobby()
    {
        if (NetworkServer.active && NetworkClient.isConnected)
        {
            MyNetworkManager.singleton.StopHost();
        }
        else
        {
            MyNetworkManager.singleton.StopClient();

            SceneManager.LoadScene(0);
        }
    }
}
