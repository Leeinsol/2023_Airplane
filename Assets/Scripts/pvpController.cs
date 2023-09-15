using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


public class pvpController : MonoBehaviourPunCallbacks
{
    GameObject playerObj;
    GameObject opponentObj;

    private void Awake()
    {
        Screen.SetResolution(540, 960, false);

        //PhotonNetwork.ConnectUsingSettings();

       

        if (PhotonNetwork.IsMasterClient)
        {
            Debug.Log("1");
            Vector3 pos = new Vector3(0, -4f, 0);
            Color color = new Color(1f, 0.5f, 0.5f, 1f);
            InstanciateCharacter(pos,color,Quaternion.identity);
        }
        else
        {
            Debug.Log("2");
            Vector3 pos = new Vector3(0, 4f, 0);
            Color color = new Color(0.5f, 0.5f, 1f, 1f);
            Quaternion rot = Quaternion.Euler(0, 0, -180);

            Vector3 currentRotation = Camera.main.transform.rotation.eulerAngles;
            currentRotation.z += 180f;
            Camera.main.transform.rotation = Quaternion.Euler(currentRotation);
            InstanciateCharacter(pos,color,rot);

        }

    }


    void InstanciateCharacter(Vector3 pos, Color color, Quaternion rot)
    {
        string selectedOption = (string)PhotonNetwork.LocalPlayer.CustomProperties["SelectedOption"];
        Debug.Log("Selected Option: " + selectedOption);

        if (selectedOption == "player")
        {
            playerObj = PhotonNetwork.Instantiate("Player", pos, rot);
            playerObj.GetComponent<PhotonView>().RPC("ChangeColor", RpcTarget.AllBuffered, color.r, color.g, color.b, color.a);
        }
        else
        {
            opponentObj = PhotonNetwork.Instantiate("Player2", pos, rot);
            opponentObj.GetComponent<PhotonView>().RPC("ChangeColor", RpcTarget.AllBuffered, color.r, color.g, color.b, color.a);
        }
    }


    //void InstanciateCharacter(Vector3 pos, Color color, Quaternion rot)
    //{
    //    string selectedOption = (string)PhotonNetwork.LocalPlayer.CustomProperties["SelectedOption"];
    //    Debug.Log("Selected Option: " + selectedOption);

    //    if (selectedOption == "player")
    //    {
    //        //Color color = new Color(1, 0.5f, 0.5f, 1);
    //        playerObj = PhotonNetwork.Instantiate("Player", pos, Quaternion.identity);


    //        playerObj.GetComponent<PhotonView>().RPC("ChangeColor", RpcTarget.AllBuffered, 1f, 0.5f, 0.5f, 1f);
    //        //playerObj.GetComponent<playerController>().ChangeColor(serializableColor);
    //        //playerObj.GetComponent<SpriteRenderer>().color = color;
    //        //playerObj.GetComponent<PhotonView>().RPC("ChangeColor", RpcTarget.AllBuffered, serializableColor);
    //        //photonView.RPC("ChangeColor", RpcTarget.AllBuffered, color);
    //    }
    //    else
    //    {
    //        //Color color = new Color(0.5f, 0.5f, 1, 1);

    //        opponentObj = PhotonNetwork.Instantiate("Player2", pos, rot);
    //        //opponentObj.GetComponent<SpriteRenderer>().color = color;
    //        //photonView.RPC("ChangeColor", RpcTarget.AllBuffered, color);
    //        opponentObj.GetComponent<PhotonView>().RPC("ChangeColor", RpcTarget.AllBuffered, 0.5f, 0.5f, 1f, 1f);
    //        //opponentObj.GetComponent<playerController>().ChangeColor(serializableColor);


    //    }
    //}

    //public override void OnConnectedToMaster()
    //{
    //    PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions { MaxPlayers = 2 }, null);
    //}
    public override void OnJoinedRoom()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            Debug.Log("1");
            Vector3 pos = new Vector3(0, -4f, 0);
            playerObj=PhotonNetwork.Instantiate("Player", pos, Quaternion.identity);
            //playerObj.GetComponent<SpriteRenderer>().color = Color.yellow;
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
