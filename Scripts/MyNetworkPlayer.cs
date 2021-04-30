using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
using Mirror;

public class MyNetworkPlayer : NetworkBehaviour
{
    [SerializeField] private TMP_Text displayNameText = null;

    [SyncVar(hook = nameof(HandleDisplayNameUpdate))]
    [SerializeField]
    private string displayName = "Missing Name";
    [SyncVar(hook = nameof(AuthorityHandlePartyOwnerStateUpdated))]
    private bool isPartyOwner = false;
    [SyncVar(hook =nameof(ClientHandleClientNameUpdated))]
    private string clientName;

    public static event Action ClientOnInfoUpdated;
    public static event Action<bool> AuthorityOnPartyOwnerStateUpdated;
    public GameObject PlayerCamera;

    private void Start()
    {
        if (!hasAuthority) { return; }
        this.name = "Player";
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if(SceneManager.GetActiveScene().name != "Menu")
        {
            PlayerCamera.SetActive(true);
            gameObject.GetComponent<FPSMovement>().enabled = true;
            
        }
        else
        {
            PlayerCamera.SetActive(false);
            gameObject.GetComponent<FPSMovement>().enabled = false;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public string GetClientName()
    {
        return clientName;
    }

    public bool GetIsPartyOwner()
    {
        return isPartyOwner;
    }

    #region Server

    [Server]
    public void SetClientName(string clientName)
    {
        this.clientName = clientName;
    }

    [Server]
    public void SetDisplayName(string newDisplayName)
    {
        displayName = newDisplayName;
    }

    [Server]
    public void SetPartyOwner(bool state)
    {
        isPartyOwner = state;
    }

    [Command]
    private void CmdSetDisplayName(string NewDisplayName)
    {
        if (NewDisplayName.Length < 2 || NewDisplayName.Length > 20) { return; }

        RpcLogNewName(NewDisplayName);

        SetDisplayName(NewDisplayName);
    }

    [Command]
    public void CmdStartGame()
    {
        if (!isPartyOwner) { return; }

        ((MyNetworkManager)MyNetworkManager.singleton).StartGame();
    }
    #endregion

    #region Client

    private void ClientHandleClientNameUpdated(string oldClientName, string newClientName)
    {
        ClientOnInfoUpdated?.Invoke();
    }

    private void HandleDisplayNameUpdate(string OldName, string NewName)
    {
        displayNameText.text = NewName;
    }

    [ContextMenu("Set My Name")]
    private void SetMyName()
    {
        CmdSetDisplayName("My New Name");
    }

    [ClientRpc]
    private void RpcLogNewName(string newDisplayName)
    {
        Debug.Log(newDisplayName);
    }

    public override void OnStartClient()
    {
        if(NetworkServer.active) { return; }

        ((MyNetworkManager)NetworkManager.singleton).Players.Add(this);
    }

    public override void OnStopClient()
    {
        ClientOnInfoUpdated?.Invoke();

        if (!isClientOnly) { return; }

        ((MyNetworkManager)NetworkManager.singleton).Players.Remove(this);

        if(!hasAuthority) { return; }
    }

    private void AuthorityHandlePartyOwnerStateUpdated(bool oldState, bool newState)
    {
        if (!hasAuthority) { return; }

        AuthorityOnPartyOwnerStateUpdated?.Invoke(newState);
    }
    #endregion


}
