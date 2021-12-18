using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiater : MonoBehaviour
{
    private bool holdsBall = false;
    [SerializeField]
    private float coolDown = 0.8f;
    [SerializeField]
    private float inCoolDown = 0;
    [SerializeField]
    private GameManager GM;
    [SerializeField]
    public float moveSpeed = 1f;
    public Transform leftLimit;
    public Transform rightLimit;

    private void Start()
    {
        pickBall();
    }

    public void Drop()
    {
        holdsBall = false;
        inCoolDown = coolDown;
    }

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
        if (GM.shrinkBallSizes > 1)
            ball.transform.localScale /= GM.shrinkBallSizes;
        else if (GM.shrinkBallSizes < 0)
            ball.transform.localScale *= -GM.shrinkBallSizes;
        ball.name = GM.balls[index].name;
        ball.GetComponent<Rigidbody>().isKinematic = true;
        ball.transform.parent = this.transform;
        ball.AddComponent<instantiaterChild>();
        ball.GetComponent<instantiaterChild>().instantiater = this;
        ball.GetComponent<TrailRenderer>().startWidth = ball.transform.localScale.x;
        ball.GetComponent<TrailRenderer>().endWidth = (ball.transform.localScale.x / 2f);
        ball.GetComponent<Rigidbody>().interpolation = RigidbodyInterpolation.None;
        holdsBall = true;
    }
}
