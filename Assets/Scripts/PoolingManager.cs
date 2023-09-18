using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PoolingManager : MonoBehaviourPunCallbacks
{
    public static PoolingManager instance;

    public Queue<GameObject> poolingManagerQueue = new Queue<GameObject>();
    public List<GameObject> tmp;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    void Initialize(int n)
    {
        for(int i=0; i<n; i++)
        {
            GameObject bullet = PhotonNetwork.Instantiate("Bullet", transform.position, Quaternion.identity);
            bullet.SetActive(false);
        }
    }

    private GameObject CreateNewObject()
    {
        GameObject bullet = PhotonNetwork.Instantiate("Bullet", transform.position, Quaternion.identity);

        return bullet;

    }

    public GameObject GetObject(Vector3 position, Quaternion rotation)
    {
        var bullet = poolingManagerQueue.Count > 0 ? poolingManagerQueue.Dequeue() : CreateNewObject();

        if (bullet != null)
        {
            bullet.transform.position = position;
            bullet.transform.rotation = rotation;
            bullet.SetActive(true);
        }

        return bullet;
    }

    public void ReturnObject(GameObject bullet)
    {
        bullet.gameObject.SetActive(false);
        instance.poolingManagerQueue.Enqueue(bullet);
    }


    // Update is called once per frame
    void Update()
    {

    }
}