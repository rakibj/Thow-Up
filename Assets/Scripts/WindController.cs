using UnityEngine;

public class WindController : MonoBehaviour {

    public float power = 10f;
    public float powerRange = 2000f;
    public static WindController instance;

    private void Start()
    {
        instance = this;
        PlayerController.respawn = true;
    }

    private void Update()
    {
        WindValuesCalc();
    }

    public void WindValuesCalc()
    {
        if (PlayerController.respawn)
        {
            power = Random.Range(-powerRange, powerRange);
            GameManager.windPower = (int)(Mathf.Abs(power) / 200);
            int signedPower = (int)(power / 200);
            if (signedPower == 0)
            {
                power = 0;
                GameManager.arrowAngle = 0;
            }
            else if (signedPower > 0)
            {
                GameManager.arrowAngle = -90;
            }
            else
            {
                GameManager.arrowAngle = 90;
            }
            PlayerController.respawn = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (PlayerController.shot)
            {
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.right * power * Time.deltaTime);
            }
        }
    }
}
