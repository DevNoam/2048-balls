using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiater : MonoBehaviour
{
    //public bool canShot = true;
    public bool holdsBall = false;
    public bool canMove = true;
    public float coolDown = 1.2f;
    public float inCoolDown = 0;
    [SerializeField]
    private GameManager GM;
    [SerializeField]
    public float moveSpeed = 0.01f;

    private void Start()
    {
        pickBall();
    }

    public Camera camera;
    private Touch touch;
    public Transform ts;

    private void Update()
    {
        if (holdsBall == false)
        {
            if (inCoolDown > 0)
                inCoolDown -= Time.deltaTime;
            if (inCoolDown <= 0)
            {
                pickBall();
            }
        }
    }

    private void pickBall()
    {
        int index;
        float randValue = Random.value;
        if (randValue < .5f)
        {
            index = 0;
        }
        else if (randValue < .7f) 
        {
            index = 1;
        }
        else if(randValue < .85f)
        {
            index = 2;
        }
        else // 10% of the time
        {
            index = 3;
        }

        GameObject ball = Instantiate(GM.balls[index], transform) as GameObject;
        ball.GetComponent<Rigidbody>().isKinematic = true;
        ball.AddComponent<instantiaterChild>();
        ball.GetComponent<instantiaterChild>().instantiater = this;

        ball.GetComponent<Rigidbody>().interpolation = RigidbodyInterpolation.None;
        ball.transform.parent = this.transform;
        holdsBall = true;
    }
}
