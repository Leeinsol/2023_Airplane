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

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Shoot()
    {
        GetComponent<Rigidbody2D>().AddForce(transform.up * 10f, ForceMode2D.Impulse);
        Invoke("ReturnPool", 2f);
    }

    void ReturnPool()
    {
        PoolingManager.instance.ReturnObject(this.gameObject);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            ReturnPool();
            collision.GetComponent<playerController>().Hit();
        }
    }
    [PunRPC]
    void DestroyRPC() => Destroy(gameObject);

    [PunRPC]
    public void ActivateBullet()
    {
        Shoot();
    }
}
