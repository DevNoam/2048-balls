using UnityEngine;
using TMPro;
public class GameManager : MonoBehaviour
{
    public GameObject[] balls;
    private bool gameOver = false;
    private long toReach = 256;
    private long highestBall = 0;
    [SerializeField]
    private long score = 0;
    private long bestScore = 0;
    [SerializeField]
    private TMP_Text scoreText;

    public void Merging(string newsize)
    {
        int size;
        int.TryParse(newsize,out size);
        score += size / 2;
        scoreText.text = score.ToString();
        if (bestScore < score)
            bestScore = score;
        if (size > highestBall)
            highestBall = size;
        if (highestBall >= toReach)
        {
            newLevel();
        }
    }

    private void newLevel()
    {
        toReach *= 2;
    }

    public void GameOver()
    { 
        //Stop game,
        //Dark and delete balls.
    }
}
