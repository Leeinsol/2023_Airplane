using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class playerController : MonoBehaviour
{
    Rigidbody2D rb;

    [SerializeField]
    float speed = 50f;

    [SerializeField]
    GameObject bulletPrefab;

    [SerializeField]
    GameObject gameOverPanel;
    [SerializeField]
    GameObject player2;

    [SerializeField]
    float bulletRate = 0.5f;

    int hp = 3;

    bool canFire = true;

    Vector3 lastPos;
    public static event Action<Vector3> changePos;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gameOverPanel.SetActive(false);
        Time.timeScale = 1;
        lastPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();

        CheckGameOver();
        //SetTimeScale();
    }

    void Move()
    {
        Vector3 currentPos = transform.position;
        Vector3 relativePositionChange = currentPos - lastPos;
        

        lastPos = currentPos;

        float moveX = speed * Time.deltaTime * Input.GetAxisRaw("Horizontal");

        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            transform.Translate(moveX, 0, 0);

            Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);

            viewPos.x = Mathf.Clamp01(viewPos.x);

            Vector3 worldPos = Camera.main.ViewportToWorldPoint(viewPos);
            transform.position = worldPos;

            //Debug.Log(relativePositionChange);
            changePos?.Invoke(relativePositionChange);
        }
    }

    void Fire()
    {
        if (canFire && Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(bulletPrefab, transform.GetChild(0).position, transform.rotation);
            StartCoroutine(CoolTime(bulletRate));
        }
    }

    IEnumerator CoolTime(float time)
    {
        canFire = false;
        yield return new WaitForSeconds(time);
        canFire = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        hp--;
        Destroy(collision.gameObject);
    }

    void CheckGameOver()
    {
        if (hp <= 0)
        {
            Time.timeScale = 0;
            gameOverPanel.SetActive(true);
        }
    }
}