using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public GameObject[] balls;
    private bool gameOver = false;
    private long toReach;
    private long highestBall = 0;
    private long score = 0;
    private long bestScore = 0;
    [SerializeField]
    private TMP_Text scoreText;
    public float shrinkBallSizes;

    private void Start()
    {
        long.TryParse(balls[balls.Length - 1].gameObject.name, out toReach);
        toReach *= 2;
        shrinkSizes();
    }

    void shrinkSizes()
    {
        if (shrinkBallSizes == 0 || shrinkBallSizes == 1)
            return;
        GameObject[] ballsInGame = GameObject.FindGameObjectsWithTag("Ball");
        if (ballsInGame.Length > 0)
        {
            for (int i = 0; i < ballsInGame.Length; i++)
            {
                if (shrinkBallSizes > 0)
                    ballsInGame[i].transform.localScale /= shrinkBallSizes;
                else
                    ballsInGame[i].transform.localScale *= -shrinkBallSizes;
            }
        }
    }

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

    public void newLevel()
    {
        Debug.Log("NEW LEVEL!");
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        //Transictaion animation..
        SceneManager.LoadScene(sceneIndex++);
        //toReach *= 2;
    }

    public void GameOver()
    {
        Debug.Log("GAME OVER");
        //Stop game,
        //Dark and delete balls.
    }
}
