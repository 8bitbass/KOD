using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    private short selected = 0;
    private short dif = 2;
    private bool showTimer = true;
    bool done = false;

    private bool vertCenter;
    private bool horizCenter;

    public Text newGame;
    public Text difficulty;
    public Text timer;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MenuScreen();
    }

    void MenuScreen()
    {
        Time.timeScale = 0.0f;

        short val = 0;
        bool play = false;
        if (Input.GetButtonDown("Fire1"))
        {
            val += 1;
            play = true;
        }

        val += (short)GetHorizMod();

        newGame.color = Color.white;
        difficulty.color = Color.white;
        timer.color = Color.white;

        selected -= (short)GetVertMod();
        selected = (short)Mathf.Repeat(selected, 3);
        switch (selected)
        {
            case 0:
                if (play)
                {
                    GameManager.StartGame();
                    Time.timeScale = 1.0f;
                }
                newGame.color = Color.cyan;
                break;
            case 1:
                dif += val;
                dif = (short)Mathf.Repeat(dif, 5);
                EnemyManagerLogic.difficultyLevel = dif +1;
                SetDifficultyText();
                difficulty.color = Color.cyan;
                break;
            case 2:
                if (val == 1 || val == -1)
                {
                    showTimer = !showTimer;
                    GameManager.alwaysShowTimer = showTimer;
                }
                timer.color = Color.cyan;
                SetTimerText();
                break;
        }
    }

    void SetDifficultyText()
    {
        switch (dif)
        {
            case 0:
                difficulty.text = "DIFFICULTY:VERY EASY";
                break;
            case 1:
                difficulty.text = "DIFFICULTY:EASY";
                break;
            case 2:
                difficulty.text = "DIFFICULTY:NORMAL";
                break;
            case 3:
                difficulty.text = "DIFFICULTY:HARD";
                break;
            case 4:
                difficulty.text = "DIFFICULTY:VERY HARD";
                break;
            default:
                difficulty.text = "error";
                break;
        }
    }

    void SetTimerText()
    {
        if (showTimer)
        {
            timer.text = "TIMER:ALWAYS";
        }
        else
        {
            timer.text = "TIMER:5 SECONDS";
        }
    }

    int GetVertMod()
    {
        float vert = Input.GetAxis("Vertical");

        if (vert < 0.3f && vert > -0.3f)
        {
            vertCenter = true;
        }
        else if (vertCenter && (vert > 0.5f || vert < -0.5f))
        {
            vertCenter = false;
            return Mathf.RoundToInt(vert);
        }

        return 0;
    }

    int GetHorizMod()
    {
        float Horiz = Input.GetAxis("Horizontal");

        if (Horiz < 0.3f && Horiz > -0.3f)
        {
            horizCenter = true;
        }
        else if (horizCenter && (Horiz > 0.5f || Horiz < -0.5f))
        {
            horizCenter = false;
            return Mathf.RoundToInt(Horiz);
        }

        return 0;
    }
}
