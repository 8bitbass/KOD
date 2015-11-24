using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameOver : MonoBehaviour
{
    char letter1 = 'A', letter2 = 'A', letter3 = 'A';
    int value1 = 0, value2 = 0, value3 = 0;
    private short selected = 1;
    bool done = false;

    private bool stickCenter;

    public Text gameOverName;
    public Text gameOverScore;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GameOverScreen();
    }

    public void GameOverScreen()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            selected += 1;
        }
        switch (selected)
        {
            case 1:
                value1 += GetStickMod();
                value1 = (int)Mathf.Repeat(value1, 27);
                if (value1 == 26)
                {
                    letter1 = (char)(32);
                }
                else
                {
                    letter1 = (char)(65 + value1);
                }
                break;
            case 2:
                value2 += GetStickMod();
                value2 = (int)Mathf.Repeat(value2, 27);
                if (value2 == 26)
                {
                    letter2 = (char)(32);
                }
                else
                {
                    letter2 = (char)(65 + value2);
                }
                break;
            case 3:
                value3 += GetStickMod();
                value3 = (int)Mathf.Repeat(value3, 27);
                if (value3 == 26)
                {
                    letter3 = (char)(32);
                }
                else
                {
                    letter3 = (char)(65 + value3);
                }
                break;
            case 4:
                selected = 1;
                done = true;
                break;
        }

        string nam = letter1.ToString() + letter2.ToString() + letter3.ToString();
        gameOverName.text = nam;
        gameOverScore.text = GameManager.score.ToString();

        if (done)
        {
            ScoreboardLogic.UpdateScoreboard(nam, GameManager.score);
            done = false;
            GameManager.Reload();
        }
    }

    int GetStickMod()
    {
        float vert = Input.GetAxis("Vertical");

        if (vert < 0.3f && vert > -0.3f)
        {
            stickCenter = true;
        }
        else if (stickCenter && (vert > 0.5f || vert < -0.5f))
        {
            stickCenter = false;
            return Mathf.RoundToInt(vert);
        }

        return 0;
    }
}