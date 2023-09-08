using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using WebSocketSharp;

public class pvpController : MonoBehaviour
{
    private WebSocket ws;

    // Start is called before the first frame update
    void Start()
    {
        ws = new WebSocket("ws://192.168.0.21:3333");
        ws.OnMessage += message;
        ws.OnOpen += open;
        ws.OnClose += close;
        ws.Connect();
        ws.Send("hello");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void message(object sender, MessageEventArgs e)
    {
        Debug.Log(e.Data);
    }

    void open(object sender, System.EventArgs e)
    {
        Debug.Log("open");
    }

    void close(object sender,CloseEventArgs e)
    {
        Debug.Log("close");
    }
}
