using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ScoreboardLogic : MonoBehaviour
{
    class Scores
    {
        public int score;
        public string name;
    }

    static readonly List<Scores> scoreboard = new List<Scores>();

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < 10; ++i)
        {
            Scores temp = new Scores
            {
                name = "AAA",
                score = 0
            };
            scoreboard.Add(temp);
        }
        MakeScoreboard();
    }

    static void MakeScoreboard()
    {
        for (int i = 0; i < 10; ++i)
        {
            if (PlayerPrefs.HasKey("Score" + i))
            {
                string scoreAsText = PlayerPrefs.GetString("Score" + i);
                string scoreNumber = scoreAsText.Substring(3);
                string scoreName = scoreAsText.Substring(0, 3);


                scoreboard[i].score = int.Parse(scoreNumber);
                scoreboard[i].name = scoreName;
            }
            else
            {
                scoreboard[i].name = "AAA";
                scoreboard[i].score = 0;
            }
        }
    }

    static void SaveScoreboard()
    {
        for (int i = 0; i < 10; ++i)
        {
            PlayerPrefs.SetString("Score" + i, scoreboard[i].name + scoreboard[i].score);
        }
    }

    public static void UpdateScoreboard(string name, int score)
    {
        if (score > scoreboard[0].score)
        {
            Scores newScore = new Scores();
            newScore.name = name;
            newScore.score = score;
            int index = 0;
            bool newHighScore = false;
            foreach (Scores s in scoreboard)
            {
                if (newScore.score < s.score)
                {
                    newHighScore = true;
                    index = scoreboard.IndexOf(s);
                }
                else if (s == scoreboard.Last())
                {
                    newHighScore = true;
                    index = scoreboard.Count;
                }
            }
            if (newHighScore)
            {
                scoreboard.Insert(index, newScore);
            }
            scoreboard.RemoveAt(0);
            SaveScoreboard();
        }
    }
}