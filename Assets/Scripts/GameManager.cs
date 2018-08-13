using UnityEngine;

public class GameManager : MonoBehaviour {

    public static int windPower;
    public static float arrowAngle;
    public static int score;
    public static bool inGame;
    public static bool playerDead;
	// Use this for initialization
	void Start () {
        score = 0;
        inGame = false;
	}
	
	// Update is called once per frame
	void Update () {

	}
}
