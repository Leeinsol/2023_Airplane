using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    float bulletRate = 0.5f;

    int hp = 3;

    bool canFire = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gameOverPanel.SetActive(false);
        Time.timeScale = 1;
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
        float hor = Input.GetAxisRaw("Horizontal");
        Vector3 moveHorizontal = transform.right * hor;

        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            Vector3 velocity;
            velocity = moveHorizontal.normalized * speed;

            rb.MovePosition(transform.position + velocity * Time.deltaTime);
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

    void SetTimeScale()
    {
        if (gameOverPanel.activeSelf) Time.timeScale = 0;
        else Time.timeScale = 1;
    }
}
