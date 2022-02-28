using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;
public class GameManager : MonoBehaviour
{
    public GameObject[] balls;
    private bool gameOver = false;
    [SerializeField]
    private float score = 0;
    [SerializeField]
    private int level = 1;
    [SerializeField]
    private float remainingForNextLevel = 25;
    public int howmany2048;
    public int get2048   // property
    {
        get { return howmany2048; }
        set { howmany2048 = value;
            howmany2048Text.text = howmany2048.ToString();
            PlayerPrefs.SetInt("howmany2048", howmany2048);
        }
    }
    //[SerializeField]
    //private long howmany2048best = 0;
    [SerializeField]
    private TMP_Text scoreText;
    [SerializeField]
    private TMP_Text LevelText;
    [SerializeField]
    private TMP_Text NextLevelText;
    [SerializeField]
    private TextMesh howmany2048Text;
    public Slider scoreSlider;
    //public float shrinkBallSizes;

    private void Start()
    {
        //long.TryParse(balls[balls.Length - 1].gameObject.name, out toReach);
        //toReach *= 2;
        //shrinkSizes();

        if (PlayerPrefs.GetInt("level") > 0)
        {
            score = PlayerPrefs.GetFloat("score");
            level = PlayerPrefs.GetInt("level");
            remainingForNextLevel = PlayerPrefs.GetFloat("remainingForNextLevel");
            howmany2048 = PlayerPrefs.GetInt("howmany2048");
        }

        LevelText.text = level.ToString();
        NextLevelText.text = (level + 1).ToString();
        scoreSlider.maxValue = remainingForNextLevel;
        scoreSlider.value = score;
        howmany2048Text.text = howmany2048.ToString();
        scoreText.text = (remainingForNextLevel - score).ToString();
    }

    void shrinkSizes()
    {
        /*if (shrinkBallSizes == 0 || shrinkBallSizes == 1)
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
        }*/
    }

    public void Merging(string newsize)
    {
        int size;
        int.TryParse(newsize,out size);

        score += size / 2;

        if (score >= remainingForNextLevel)
        {
            score -= remainingForNextLevel;
            if (score < 0)
                score = 0;
            level++;
            remainingForNextLevel *= level / 2;


            scoreSlider.maxValue = remainingForNextLevel;
            LevelText.text = level.ToString();
            NextLevelText.text = (level + 1).ToString();
            PlayerPrefs.SetFloat("remainingForNextLevel", remainingForNextLevel);
            PlayerPrefs.SetInt("level", level);
        }
        scoreSlider.value = score;
        scoreText.text = (remainingForNextLevel - score).ToString();
        PlayerPrefs.SetFloat("score", score);
    }


    public void GameOver()
    {
        Debug.Log("GAME OVER");
        Destroy(GameObject.Find("Instantiater"));
        PlayerPrefs.DeleteAll();
        gameOver = true;
        StartCoroutine(darkenBalls());
        //Open game over screen
    }
    IEnumerator darkenBalls() {
      //Declare a yield instruction.
      WaitForSeconds wait = new WaitForSeconds(0.03f);
 
        object[] obj = GameObject.FindSceneObjectsOfType(typeof (GameObject));
        foreach (object o in obj)
        {
            GameObject g = (GameObject) o;
            if (g.tag == "Ball")
            {
                g.gameObject.GetComponent<MeshRenderer>().material.color = Color.grey;
                g.transform.GetChild(0).gameObject.SetActive(false);
            }
            yield return wait; //Pause the loop for 3 seconds.
        }
   }
    
}

