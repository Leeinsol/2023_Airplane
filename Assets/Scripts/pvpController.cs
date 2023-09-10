using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class pvpController : MonoBehaviourPunCallbacks
{
    public GameObject playerObj;
    public GameObject opponentObj;

    private void Awake()
    {
        Screen.SetResolution(540, 960, false);
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions { MaxPlayers = 2 }, null);
    }
    public override void OnJoinedRoom()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            Debug.Log("1");
            Vector3 pos = new Vector3(0, -4f, 0);
            playerObj=PhotonNetwork.Instantiate("Player", pos, Quaternion.identity);
        }
        else
        {
            Debug.Log("2");

            Vector3 pos = new Vector3(0, 4f, 0);
            Quaternion rot= Quaternion.Euler(0, 0, -180);
            opponentObj=PhotonNetwork.Instantiate("Opponent", pos, rot);

            Vector3 currentRotation = Camera.main.transform.rotation.eulerAngles;
            currentRotation.z += 180f;
            Camera.main.transform.rotation = Quaternion.Euler(currentRotation);
        }
        
    }

    private void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
