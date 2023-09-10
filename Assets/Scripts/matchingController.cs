using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class matchingController : MonoBehaviourPunCallbacks
{
    public int playerCount = 0;

    public GameObject startButton;

    private void Awake()
    {
        PhotonNetwork.ConnectUsingSettings();
        OnConnectedToMaster();
    }

    public void Connect() => PhotonNetwork.ConnectUsingSettings();

    // Start is called before the first frame update
    void Start()
    {
    }


    // Update is called once per frame
    void Update()
    {
        
        if (PhotonNetwork.InRoom &&PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers)
        {
            PhotonNetwork.LeaveRoom();
            SceneManager.LoadScene("Main");
        }
    }
    public void Disconnect() => PhotonNetwork.Disconnect();
    public override void OnDisconnected(DisconnectCause cause) => print("연결끊김");

    public override void OnConnectedToMaster()
    {
        Debug.Log("서버연결");
        startButton.GetComponent<Button>().interactable = true;
    }

    public void CreateRoom()
    {
        string roomName = "Room" + Random.Range(1000, 9999);
        PhotonNetwork.CreateRoom(roomName, new RoomOptions { MaxPlayers = 2 });
    }
    public void JoinOrCreateRoom() => PhotonNetwork.JoinOrCreateRoom(null,new RoomOptions { MaxPlayers = 2 }, null);

    public void JoinRandomRoom() => PhotonNetwork.JoinRandomRoom();
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        Debug.Log("나감");
        playerCount--;

    }

    public override void OnCreatedRoom() => print("방만들기완료");

    public override void OnJoinedRoom() { 
        print("방참가완료");
        playerCount++;

        
    }

    public override void OnCreateRoomFailed(short returnCode, string message) => print("방만들기실패");

    public override void OnJoinRoomFailed(short returnCode, string message) => print("방참가실패");

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        print("방랜덤참가실패");
        CreateRoom();

    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log("취소");
    }
    

}
