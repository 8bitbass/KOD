using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static int score = 0;
    public static float playerHealthPercent;
    public static bool alwaysShowTimer = true;

    public Slider playerHealthSlider;
    public Text scoreText;
    public Text waveNumberText;
    public Text waveCountDownText;

    private static CharacterHealthLogic playerHealthLogic;

    private GameObject gameOverMenu;
    private static GameObject mainMenu;
	private GameObject pauseMenu;
            

    // Use this for initialization
    void Start()
    {
        gameOverMenu = GameObject.Find("GameOverMenu");
        gameOverMenu.SetActive(false);
        mainMenu = GameObject.Find("MainMenu");
		pauseMenu = GameObject.Find("PauseMenu");
		pauseMenu.SetActive(false);
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        playerHealthLogic = p.GetComponent<CharacterHealthLogic>();
    }

    // Update is called once per frame
    void Update()
    {
        playerHealthPercent = (playerHealthLogic.currentHealth / playerHealthLogic.maxHealth) * 100;
        playerHealthSlider.value = playerHealthPercent;
        scoreText.text = score.ToString();
        waveNumberText.text = "WAVE " + EnemyManagerLogic.waveNumber;

        if ((EnemyManagerLogic.currentWaveTime < 6 || alwaysShowTimer) && EnemyManagerLogic.currentWaveTime > 0)
        {
            waveCountDownText.text = "Next Wave In: " + ((int)EnemyManagerLogic.currentWaveTime).ToString();
        }
        else
        {
            waveCountDownText.text = "";
        }

        if (Input.GetButtonDown("Pause") && (!gameOverMenu.activeInHierarchy && !mainMenu.activeInHierarchy))
        {
            if (pauseMenu.activeInHierarchy)
            {
                pauseMenu.SetActive(false);
                Time.timeScale = 1.0f;
            }
            else
            {
                pauseMenu.SetActive(true);
				GameObject.Find("Button (Resume)").GetComponent<UnityEngine.UI.Button>().Select();
                Time.timeScale = 0.0f;
            }
        }
        if (playerHealthLogic.currentHealth <= 0)
        {
                gameOverMenu.SetActive(true);
                Time.timeScale = 0.0f;
        }
    }

	public void ResumeGame()
	{
		pauseMenu.SetActive(false);
		Time.timeScale = 1.0f;
	}

	public void QuitGame()
	{
		Application.Quit();
	}

    public static void Reload()
    {
        EnemyManagerLogic.Reload();
        score = 0;
        Application.LoadLevel(Application.loadedLevel);
        Time.timeScale = 1.0f;
    }

    public static void StartGame()
    {
        Time.timeScale = 1.0f;
        mainMenu.SetActive(false);
    }
}
