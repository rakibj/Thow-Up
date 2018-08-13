using UnityEngine;
using EZCameraShake;
public class PlayerController : MonoBehaviour {

    public float speed;

    public static bool shot;
    public static bool respawn;
    public GameObject scoreEffect;
    public GameObject deadEffect;

    bool aiming;
    Rigidbody2D rb;
    LineRenderer lr;
    Vector3 startPos, endPos;

    private void Start()
    {
        GameManager.playerDead = false;
        rb = GetComponent<Rigidbody2D>();
        lr = GetComponent<LineRenderer>();
        Inititalize();
    }

    public void Inititalize()
    {
        aiming = false;
        shot = false;
        transform.position = new Vector2(0, 0);
        rb.angularVelocity = 0;
        rb.velocity = Vector2.zero;
    }

    private void Update()
    {
        if (!GameManager.inGame) return;
        GetHold();
        Aiming();
        ReleaseHold();
        PositionCorrection();
    }

    void GetHold()
    {
        if (Input.GetMouseButtonDown(0) && !aiming && !shot)
        {
            aiming = true;
        }
    }

    void Aiming()
    {
        if (aiming)
        {
            lr.enabled = true;
            startPos = transform.position;
            lr.SetPosition(0, startPos);
            Vector3 shootPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            shootPos.z = 0;
            shootPos = transform.position + (transform.position - shootPos);
            endPos = shootPos;
            lr.SetPosition(1, endPos);
        }
        else
        {
            lr.enabled = false;
        }
    }

    void ReleaseHold()
    {
        if (Input.GetMouseButtonUp(0) && aiming && !shot)
        {
            AudioManager.instance.Play("release");
            aiming = false;
            Vector2 direction = startPos - endPos;
            rb.AddForce(direction * -speed);
            shot = true;
        }
    }

    void PositionCorrection()
    {
        if (!shot)
        {
            transform.position = new Vector2(0, 0);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Target")
        {
            Instantiate(scoreEffect, transform.position, Quaternion.identity);
            AudioManager.instance.Play("hit");
            GameManager.score++;
            respawn = true;
            Inititalize();

        }else if(collision.gameObject.tag == "Boundary")
        {
            Instantiate(deadEffect, transform.position, Quaternion.identity);
            CameraShaker.Instance.ShakeOnce(4f, 4f, .1f, 1f);
            AudioManager.instance.Play("death");
            GameManager.playerDead = true;
            GameManager.inGame = false;
            Destroy(gameObject);
            //respawn = true;
            //Inititalize();
        }
    }
}
