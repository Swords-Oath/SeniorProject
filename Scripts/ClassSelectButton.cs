using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class ClassSelectButton : NetworkBehaviour
{
    [SerializeField] public GameObject ClassImage;
    [SerializeField] public string ClassName;
    [SerializeField]  GameObject Player;
    [SerializeField]  public GameObject Lobby;
    public GameObject[] Classes = new GameObject[3];

    private void Update()
    {

        if (Lobby.activeSelf == true)
        {
            Player = GameObject.Find("Player");
        }

        if (!Player.GetComponent<NetworkIdentity>().hasAuthority) { return; }
    }

    public void SetClass()
    {
        for (int i = 0; i < Classes.Length; i++)
        {
            Classes[i].SetActive(false);
        }

        Player.GetComponent<Classes>().Class = ClassName;
        ClassImage.SetActive(true);
    }
}
