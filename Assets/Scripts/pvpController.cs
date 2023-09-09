using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using WebSocketSharp;
using System.Net;
using System.Net.Sockets;
using System.Collections.Concurrent;

public class pvpController : MonoBehaviour
{
    private WebSocket ws;
    bool isReady = false;
    bool isStart = false;
    bool isMove = false;

    public GameObject opponentObj;

    Vector3 relativePos;
    float xPos;

    private void OnEnable()
    {
        playerController.changePos += SendRelativePos;
    }

    private void OnDisable()
    {
        playerController.changePos -= SendRelativePos;
    }

    // Start is called before the first frame update
    void Start()
    {
        ws = new WebSocket("ws://" + IP() + ":3333");
        ws.OnMessage += Message;
        ws.OnOpen += Open;
        ws.OnClose += Close;
        ws.Connect();
        ws.Send("hello");
    }

    // Update is called once per frame
    void Update()
    {
        LoadMain();
        //Debug.Log(relativePos);

        //SetOpponentDir(relativePos);
        //tmp();
        //Debug.Log(relativePos);

        if (relativePos.x != 0)
        {
            Debug.Log(relativePos);

            opponentObj.transform.position -= relativePos;
        }

    }

    void Message(object sender, MessageEventArgs e)
    {
        //Debug.Log("Received message: " + e.Data);
        if (e.Data == "main")
        {
            isStart = true;
        }
        if (e.Data.StartsWith("pos|"))
        {
            string[] data = e.Data.Split('|');
            float x = float.Parse(data[1]);
               
            //Debug.Log(x);
            relativePos = new Vector3(x, 0, 0);
            //Debug.Log(relativePos);
        }
    }

    void tmp()
    {
        Debug.Log(isMove);
        if (isMove)
        {
            relativePos = new Vector3(xPos, 0, 0);
            Debug.Log(relativePos);

            opponentObj.transform.position -= relativePos;
            Debug.Log(opponentObj.transform.position);
        }
    }
    void Open(object sender, System.EventArgs e)
    {
        Debug.Log("open");
    }

    void Close(object sender, CloseEventArgs e)
    {
        Debug.Log("close");
    }

    public void SendReady()
    {
        if (!isReady)
        {
            ws.Send("start");
            isReady = true;
        }
    }

    public void CancelReady()
    {
        if (isReady)
        {
            ws.Send("cancel");
            isReady = false;
        }
    }

    void LoadMain()
    {
        if (isStart) SceneManager.LoadScene("Main");
    }

    string IP()
    {
        IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
        string localIP = string.Empty;
        for (int i = 0; i < host.AddressList.Length; i++)
        {
            if (host.AddressList[i].AddressFamily == AddressFamily.InterNetwork)
            {
                localIP = host.AddressList[i].ToString();
                break;
            }
        }
        return localIP;
    }

    void SendRelativePos(Vector3 pos)
    {
        string RelativeData = "pos|" + pos.x + "|" + pos.y + "|" + pos.z;
        //Debug.Log(RelativeData);
        ws.Send(RelativeData);
    }

    void SetOpponentDir(Vector3 relativePos)
    {
        if (opponentObj != null)
        {
            opponentObj.transform.position -= relativePos;
        }

    }

}