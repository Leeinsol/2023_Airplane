using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;
using System.Net;
using System.Net.Sockets;


public class opponentConroller : MonoBehaviour
{
    private WebSocket ws;

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

    void Message(object sender, MessageEventArgs e)
    {
        if (e.Data.StartsWith("pos|"))
        {
            string[] data = e.Data.Split('|');
            float x = float.Parse(data[1]);
            transform.position -= new Vector3(x, 0, 0);
            
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
