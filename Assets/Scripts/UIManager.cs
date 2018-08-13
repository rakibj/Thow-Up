using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

    public GameObject MenuUI;
    public GameObject InGameUI;
    public GameObject DeathUI;

    public Button playBtn;
    public Button restartBtn;
    public Text highScoreText;
    public Text gameScoreText;
    public Text deathScoreText;

    public static UIManager instance;
    private AudioManager audioManager;

    public Text windPowerText;
    public Image windArrowImage;
    public Text scoreText;
	// Use this for initialization
	void Start () {
        Debug.Log("Strat Called");
        windPowerText.text = "0";
        windArrowImage.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        StartScreen();
        instance = this;
        playBtn.onClick.AddListener(GameScreen);
        restartBtn.onClick.AddListener(Restart);
        audioManager = FindObjectOfType<AudioManager>();
    }
	
	// Update is called once per frame
	void Update () {
        windPowerText.text = GameManager.windPower.ToString();
        windArrowImage.transform.rotation = Quaternion.Euler(new Vector3(0, 0, GameManager.arrowAngle));
        scoreText.text = GameManager.score.ToString();
        gameScoreText.text = GameManager.score.ToString();
        deathScoreText.text = gameScoreText.text;
        if (GameManager.playerDead)
        {
            DeathScreen();
        }
    }

    void GameScreen()
    {
        audioManager.Play("BtnTu");
        GameManager.inGame = true;
        MenuUI.SetActive(false);
        InGameUI.SetActive(true);
    }

    void StartScreen()
    {
        MenuUI.SetActive(true);
        InGameUI.SetActive(false);
        DeathUI.SetActive(false);
        highScoreText.text = "High Score: " + PlayerPrefs.GetInt("highScore", 0).ToString();
    }

    public void DeathScreen()
    {
        MenuUI.SetActive(false);
        InGameUI.SetActive(false);
        DeathUI.SetActive(true);
        if (GameManager.score >= PlayerPrefs.GetInt("highScore", 0))
        {
            PlayerPrefs.SetInt("highScore", GameManager.score);
            deathScoreText.text = "Congrats!!\nNew High Score: " + GameManager.score;
        }
        else
        {
            deathScoreText.text = "Score: " + GameManager.score;
        }
    }

    public void Restart()
    {
        GameManager.playerDead = false;
        audioManager.Play("BtnTu");
        SceneManager.LoadScene(0);
    }
}
