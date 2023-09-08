using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletController : MonoBehaviour
{
    [SerializeField]
    float speed = 0.5f;

    float lifetime = 2f;
    float startTime;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        if (Time.time - startTime > lifetime) Destroy(this.gameObject);
    }

    void Move()
    {
        //rb.AddForce(transform.up * Speed, ForceMode2D.Impulse);

        transform.position += new Vector3(0, speed * Time.deltaTime, 0);
    } 
}
