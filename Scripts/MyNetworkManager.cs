using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Mirror;

public class MyNetworkManager : NetworkManager
{
    [SerializeField] private GameObject PlayerPrefab = null;

    public static event Action ClientOnConnected;
    public static event Action ClientOnDisconnected;

    private bool isGameInProgress = false;

    public List<MyNetworkPlayer> Players { get; } = new List<MyNetworkPlayer>();

    #region Server

    public override void OnServerConnect(NetworkConnection conn)
    {
        if (!isGameInProgress) { return; }

        conn.Disconnect();
    }

    public override void OnServerDisconnect(NetworkConnection conn)
    {
        MyNetworkPlayer player = conn.identity.GetComponent<MyNetworkPlayer>();

        Players.Remove(player);

        base.OnServerDisconnect(conn);
    }

    public override void OnStopServer()
    {
        Players.Clear();

        isGameInProgress = false;
    }

    public void StartGame()
    {
        isGameInProgress = true;

        ServerChangeScene("Main");
    }

    public override void ServerChangeScene(string newSceneName)
    {
        base.ServerChangeScene(newSceneName);
    }

    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        base.OnServerAddPlayer(conn);

        MyNetworkPlayer player = conn.identity.GetComponent<MyNetworkPlayer>();

        Players.Add(player);

        player.SetClientName($"Player {Players.Count}");

        player.SetDisplayName($"Player {numPlayers}.");

        player.SetPartyOwner(Players.Count == 1);
    }

    //public override void OnServerSceneChanged(string sceneName)
    ///{
        //if (SceneManager.GetActiveScene().name == "Main")
        //{
           //foreach(MyNetworkPlayer player in Players)
            //{
                //GameObject playerInstance = Instantiate(PlayerPrefab, GetStartPosition().position, Quaternion.identity);

                //NetworkServer.Spawn(playerInstance, player.connectionToClient);
            //}
        //}
    //}
    #endregion

    #region Client

    public override void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);

        ClientOnConnected?.Invoke();
    }

    public override void OnClientDisconnect(NetworkConnection conn)
    {
        base.OnClientDisconnect(conn);

        ClientOnDisconnected?.Invoke();
    }

    public override void OnStopClient()
    {
        Players.Clear();
    }
    #endregion



}
