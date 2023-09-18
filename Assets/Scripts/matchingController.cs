using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class matchingController : MonoBehaviourPunCallbacks
{
    public int playerCount = 0;

    public GameObject startButton;

    public GameObject[] characterButton;

    private void Awake()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public void Connect() => PhotonNetwork.ConnectUsingSettings();

    // Start is called before the first frame update
    void Start()
    {
    }


    // Update is called once per frame
    void Update()
    {

        if (PhotonNetwork.InRoom && PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers)
        {
            SceneManager.LoadScene("Main");
        }
        if (PhotonNetwork.IsConnected && !startButton.GetComponent<Button>().interactable)
        {
            startButton.GetComponent<Button>().interactable = true;
        }
    }
    public void Disconnect() => PhotonNetwork.Disconnect();
    public override void OnDisconnected(DisconnectCause cause) => print("¿¬°á²÷±è");

    public override void OnConnectedToMaster()
    {
        Debug.Log("¼­¹ö¿¬°á");
        startButton.GetComponent<Button>().interactable = true;
    }

    public void CreateRoom()
    {
        string roomName = "Room" + Random.Range(1000, 9999);
        PhotonNetwork.CreateRoom(roomName, new RoomOptions { MaxPlayers = 2 });
    }
    public void JoinOrCreateRoom() => PhotonNetwork.JoinOrCreateRoom(null, new RoomOptions { MaxPlayers = 2 }, null);

    public void JoinRandomRoom() => PhotonNetwork.JoinRandomRoom();
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }


    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        CreateRoom();
    }


    public void ClickOptionButton(string option)
    {
        SetOption(option);
        GameObject obj = EventSystem.current.currentSelectedGameObject;
        obj.GetComponent<Button>().interactable = false;
    }

    void SetOption(string option)
    {
        ExitGames.Client.Photon.Hashtable playerProperties = new ExitGames.Client.Photon.Hashtable();
        playerProperties["SelectedOption"] = option;
        PhotonNetwork.LocalPlayer.SetCustomProperties(playerProperties);

        foreach(var button in characterButton)
        {
            button.GetComponent<Button>().interactable = true;
        }
    }

    public void SetDefaultOption()
    {
        SetOption("player");

        characterButton[0].GetComponent<Button>().interactable = false;
    }

}
