using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Photon.Pun;

public class bulletController : MonoBehaviour
{
    [SerializeField]
    float speed = 10f;

    float lifetime = 2f;
    float startTime;
    Vector3 initialDirection;
    public PhotonView PV;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        GetComponent<Rigidbody2D>().AddForce(transform.up * 10f, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {

        if (Time.time - startTime > lifetime) Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!PV.IsMine && collision.tag == "Player" && collision.GetComponent<PhotonView>().IsMine)
        {
            collision.GetComponent<playerController>().Hit();
            PV.RPC("DestroyRPC", RpcTarget.AllBuffered);

        }
    }
    [PunRPC]
    void DestroyRPC() => Destroy(gameObject);
}
