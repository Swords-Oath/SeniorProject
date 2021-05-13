// Programs
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
using Mirror;

public class MyNetworkPlayer : NetworkBehaviour
{
    // Variable for the player name
    [SerializeField] private TMP_Text displayNameText = null;

    // Variable shared with all other clients of the players name
    [SyncVar(hook = nameof(HandleDisplayNameUpdate))]
    [SerializeField]
    private string displayName = "Missing Name";
    [SyncVar(hook =nameof(ClientHandleClientNameUpdated))]
    private string clientName;

    // Variable for if player is the host and  shares it with all other clients
    [SyncVar(hook = nameof(AuthorityHandlePartyOwnerStateUpdated))]
    private bool isPartyOwner = false;

    // Creates events for other programs to pull from
    public static event Action ClientOnInfoUpdated;
    public static event Action<bool> AuthorityOnPartyOwnerStateUpdated;

    // Variable for the players camera
    public GameObject PlayerCamera;

    private void Update()
    {
        // Checks the scene and if the player is in the menu it will cause the player camera to not be active and stop the player from moving.
        if (SceneManager.GetActiveScene().name != "Menu")
        {
            PlayerCamera.SetActive(true);
            gameObject.GetComponent<FPSMovement>().enabled = true;
            
        }
        else
        {
            PlayerCamera.SetActive(false);
            gameObject.GetComponent<FPSMovement>().enabled = false;

            // Unlocks the cursor for the player to push buttons
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public string GetClientName()
    {
        // Allows engine to call and get the clients name
        return clientName;
    }

    public bool GetIsPartyOwner()
    {
        // Allows engine to call and get if the player is the host
        return isPartyOwner;
    }

    #region Server
    // All functions are sent to the server for public use between clients

    [Server]
    public void SetClientName(string clientName)
    {
        // Will set the player the name sent to it by the network manager
        this.clientName = clientName;
    }

    [Server]
    public void SetDisplayName(string newDisplayName)
    {
        // Sets the name variable to players name
        displayName = newDisplayName;
    }

    [Server]
    public void SetPartyOwner(bool state)
    {
        // Sets if player is the owner
        isPartyOwner = state;
    }

    [Command]
    private void CmdSetDisplayName(string NewDisplayName)
    {
        // Makes sure the player name is within the provided paramitors
        if (NewDisplayName.Length < 2 || NewDisplayName.Length > 20) { return; }

        RpcLogNewName(NewDisplayName);

        SetDisplayName(NewDisplayName);
    }

    [Command]
    public void CmdStartGame()
    {
        // if the player is the owner then Call the startm game function from the Network Manager
        if (!isPartyOwner) { return; }

        ((MyNetworkManager)MyNetworkManager.singleton).StartGame();
    }
    #endregion

    #region Client
    //Only for the local client to access

    private void ClientHandleClientNameUpdated(string oldClientName, string newClientName)
    {
        // Gives the client info the players name
        ClientOnInfoUpdated?.Invoke();
    }

    private void HandleDisplayNameUpdate(string OldName, string NewName)
    {
        // Sets the name text box to players name
        displayNameText.text = NewName;
    }

    [ContextMenu("Set My Name")]
    private void SetMyName()
    {
        // Sets Server display name
        CmdSetDisplayName("My New Name");
    }

    [ClientRpc]
    private void RpcLogNewName(string newDisplayName)
    {
        // Sends to the remote user the name of the player that has been set
        Debug.Log(newDisplayName);
    }

    public override void OnStartClient()
    {
        // Gets if the Server is active
        if(NetworkServer.active) { return; }

        // Adds a player to the player list on the network
        ((MyNetworkManager)NetworkManager.singleton).Players.Add(this);
    }

    public override void OnStopClient()
    {
        // Calls original method
        ClientOnInfoUpdated?.Invoke();

        // Gets if this function is only effecting the current client
        if (!isClientOnly) { return; }

        // Removes the player from players list
        ((MyNetworkManager)NetworkManager.singleton).Players.Remove(this);

        // Gets if player has authority
        if(!hasAuthority) { return; }
    }

    private void AuthorityHandlePartyOwnerStateUpdated(bool oldState, bool newState)
    {
        // Gets if player has authority
        if (!hasAuthority) { return; }

        // Runs original program with a modified input
        AuthorityOnPartyOwnerStateUpdated?.Invoke(newState);
    }
    #endregion


}
