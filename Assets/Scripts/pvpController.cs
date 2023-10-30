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


        if (PhotonNetwork.IsMasterClient)
        {
            Vector3 pos = new Vector3(0, -4f, 0);
            Color color = new Color(1f, 0.5f, 0.5f, 1f);
            InstanciateCharacter(pos,color,Quaternion.identity);
        }
        else
        {
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

    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
