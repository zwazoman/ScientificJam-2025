using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
public static ScoreManager instance;

    public Text scoreText;
    public Text highscoreText;

    public int score= 0;
    int highscore = 0;

private void Awake() {
    instance = this;
}
 // Start is called before the first frame update
    void Start()
    {
        scoreText.text = score.ToString() + " POINTS"; 
        highscoreText.text = "HIGHSCORE: " + highscore.ToString();

        
    }

   public void AddPoint(int pointsToAdd) 
   {
        score += pointsToAdd;
        scoreText.text = score.ToString() + " POINTS";

        if(score > highscore)
        {
            highscore = score;
            highscoreText.text = "HIGHSCORE: " + highscore.ToString();
        }
   }


}
