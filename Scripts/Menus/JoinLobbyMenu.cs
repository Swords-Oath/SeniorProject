// Programs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;
using UnityEngine.UI;

public class JoinLobbyMenu : MonoBehaviour
{
    // Gets Lobby Screen objects
    [SerializeField] private GameObject landingPagePanel = null;
    [SerializeField] private TMP_InputField addressInput = null;
    [SerializeField] private Button joinButton = null;

    // If Lobby screen is open run
    private void OnEnable()
    {
        MyNetworkManager.ClientOnConnected += HandleClientConnected;
        MyNetworkManager.ClientOnDisconnected += HandleClientDisconnected;
    }

    // If Lobby Screen is closed run
    private void OnDisable()
    {
        MyNetworkManager.ClientOnConnected -= HandleClientConnected;
        MyNetworkManager.ClientOnDisconnected -= HandleClientDisconnected;
    }

    // If a player joins the lobby run
    public void Join()
    {
        // Gets new players address and stops player from joining other lobbys
        string address = addressInput.text;

        NetworkManager.singleton.networkAddress = address;
        NetworkManager.singleton.StartClient();

        joinButton.interactable = false;
    }

    // If joining lobby
    private void HandleClientConnected()
    {
        // Allows player to join and closes main page
        joinButton.interactable = true;

        gameObject.SetActive(false);
        landingPagePanel.SetActive(false);
    }

    // If joined lobby
    private void HandleClientDisconnected()
    {
        joinButton.interactable = true;
    }
}
