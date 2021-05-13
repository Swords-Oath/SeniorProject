// Programs
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Mirror;

public class MyNetworkManager : NetworkManager
{
    // Gets the Scene and creates variables for the spawn point and player model
    [SerializeField] private GameObject PlayerPrefab = null;
    [SerializeField] public string NextScene = "Main";
    [SerializeField] private Transform SpawnPoint;

    // Creates events for other programs to pull from
    public static event Action ClientOnConnected;
    public static event Action ClientOnDisconnected;

    // variable for when the game is running
    private bool isGameInProgress = false;

    // Lists for the Options panel and players in the scene
    public List<GameObject> SetOptions = new List<GameObject>();
    public List<MyNetworkPlayer> Players { get; } = new List<MyNetworkPlayer>();

    private void Update()
    {
        // If there are more then one option panels it will delete the copy
        if (SetOptions.Count >= 2)
        {
            Destroy(SetOptions[1]);
        }
    }

    #region Server
    // Overrides pre-existing functions keeping them intact with addedf features

    public override void OnServerConnect(NetworkConnection conn)
    {
        // Will check if the game is running before Connecting the server
        if (!isGameInProgress) { return; }

        conn.Disconnect();
    }

    public override void OnServerDisconnect(NetworkConnection conn)
    {
        // On Server Disconnect will get the player and remove them from the player list then disconnect the server
        MyNetworkPlayer player = conn.identity.GetComponent<MyNetworkPlayer>();

        Players.Remove(player);

        base.OnServerDisconnect(conn);
    }

    public override void OnStopServer()
    {
        // Will clear the list and end the game
        Players.Clear();

        isGameInProgress = false;
    }

    public void ChangeNextScene(string Scene)
    {
        // Will set the next scene to switch to from the current scene which is provided by the function calling to change scene
        NextScene = Scene;
    }

    public void StartGame()
    {
        // Will set the game to running and change the scene to the next scene
        isGameInProgress = true;

        ServerChangeScene(NextScene);
    }

    public override void ServerChangeScene(string newSceneName)
    {
        // Running original function with a modified input
        base.ServerChangeScene(newSceneName);
    }

    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        // Runs original method
        base.OnServerAddPlayer(conn);

        // Gets the players network
        MyNetworkPlayer player = conn.identity.GetComponent<MyNetworkPlayer>();

        // Adds them to the player list
        Players.Add(player);

        // Sets the player name to their player number
        player.SetClientName($"Player {Players.Count}");
        player.SetDisplayName($"Player {numPlayers}.");

        // Increase the total number of players
        player.SetPartyOwner(Players.Count == 1);
    }

    public override void OnServerSceneChanged(string sceneName)
    {
        // Checks the scene if player isn't in the menu it runs
        if (SceneManager.GetActiveScene().name != "Menu")
        {
            // Then creates new models for all players and connects the models to the network
           foreach(MyNetworkPlayer player in Players)
           {
                GameObject playerInstance = Instantiate(PlayerPrefab, GetStartPosition().position, Quaternion.identity);

                NetworkServer.Spawn(playerInstance, player.connectionToClient);
           }
        }
    }
    #endregion

    #region Client

    public override void OnClientConnect(NetworkConnection conn)
    {
        //Runs original method
        base.OnClientConnect(conn);

        // Then proceeds to run any added features described in the engine
        ClientOnConnected?.Invoke();
    }

    public override void OnClientDisconnect(NetworkConnection conn)
    {
        //Runs original method
        base.OnClientDisconnect(conn);

        // Then proceeds to run any added features described in the engine
        ClientOnDisconnected?.Invoke();
    }

    public override void OnStopClient()
    {
        // Clears the player list
        Players.Clear();
    }
    #endregion



}
