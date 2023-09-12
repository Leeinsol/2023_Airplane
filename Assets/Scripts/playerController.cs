using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Photon.Pun;
public class playerController : MonoBehaviourPunCallbacks,IPunObservable
{

    [SerializeField]
    float speed = 10f;

    [SerializeField]
    GameObject bulletPrefab;

    [SerializeField]
    float bulletRate = 0.5f;

    int hp = 3;

    bool canFire = true;

    public PhotonView PV;

    public GameObject UI;
   

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (PV.IsMine)
        {
            Move();
            Fire();
        }
        CheckWin();
    }

    void Move()
    {
        float moveX = speed * Time.deltaTime * Input.GetAxisRaw("Horizontal");

        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            transform.Translate(moveX, 0, 0);

            Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);

            viewPos.x = Mathf.Clamp01(viewPos.x);

            Vector3 worldPos = Camera.main.ViewportToWorldPoint(viewPos);
            transform.position = worldPos;

        }
    }

    void Fire()
    {
        if (photonView.IsMine && canFire && Input.GetKeyDown(KeyCode.Space))
        {
            //PhotonNetwork.Instantiate("Bullet", transform.GetChild(0).position, transform.rotation);
            var bullet = PoolingManager.instance.GetObject(transform.GetChild(0).position, transform.rotation);

            PhotonView bulletPhotonView = bullet.GetComponent<PhotonView>();
            bulletPhotonView.RPC("ActivateBullet", RpcTarget.AllBuffered);
            
            StartCoroutine(CoolTime(bulletRate));
        }
    }

    IEnumerator CoolTime(float time)
    {
        canFire = false;
        yield return new WaitForSeconds(time);
        canFire = true;
    }


    public void Hit()
    {
        hp--;
        UI.GetComponent<Text>().text = hp.ToString();
        if (hp <= 0)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
            PhotonNetwork.LeaveRoom();
            GameObject panel = GameObject.Find("Canvas").transform.GetChild(0).gameObject;
            panel.SetActive(true);
            panel.transform.GetChild(0).GetComponent<Text>().text = "YOU LOSE";
            PV.RPC("DestroyRPC", RpcTarget.AllBuffered);
        }
    }

    [PunRPC]
    void DestroyRPC() => Destroy(gameObject);

    void CheckWin()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount==1 && !PhotonNetwork.CurrentRoom.IsOpen)
        {
            GameObject panel = GameObject.Find("Canvas").transform.GetChild(0).gameObject;
            panel.SetActive(true);
            panel.transform.GetChild(0).GetComponent<Text>().text = "YOU WIN";

        }
    }


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(UI.GetComponent<Text>().text);
        }
        else
        {
            UI.GetComponent<Text>().text = (string)stream.ReceiveNext();
        }
    }
}