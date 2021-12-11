using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiater : MonoBehaviour
{
    //public bool canShot = true;
    public bool holdsBall = false;
    public float coolDown = 1.2f;
    private float inCoolDown = 0;
    [SerializeField]
    private GameManager GM;
    [SerializeField]
    Transform ts;
    private Vector3 mousePosition;
    public float moveSpeed = 0.01f;

    private void Start()
    {
        pickBall();
    }

    private Vector3 mousePos;
    public Camera camera;
    private Touch touch;
    public void Update()
    {
#if UNITY_STANDALONE || UNITY_EDITOR || UNITY_WEBGL
        mousePos = Input.mousePosition;
        mousePos = camera.ScreenToWorldPoint(mousePos);
#endif
        if (holdsBall == true && inCoolDown <= 0)
        {
#if UNITY_STANDALONE || UNITY_EDITOR || UNITY_WEBGL
            //rb.MovePosition(new Vector3(mousePos.x, transform.position.y, transform.position.z));
            // called the first time mouse button is pressed
            if (Input.touchCount > 0)
            {
                touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Moved)
                {
                    ts.position = new Vector3(ts.position.x + touch.deltaPosition.x * moveSpeed, ts.position.y, ts.position.z);
                }
                if (touch.phase == TouchPhase.Ended)
                {
                    Drop();
                    ts.position = new Vector3(0, ts.position.y, ts.position.z);
                }
            }


            //if (Input.GetMouseButtonDown(0))
            //{
             //   Drop();
           // }
#elif UNITY_IOS || UNITY_ANDROID

#endif
        }
        else if (holdsBall == false)
        {
            if (inCoolDown > 0)
                inCoolDown -= Time.deltaTime;
            if (inCoolDown <= 0)
            {
                pickBall();
            }
        }
    }


    private void Drop()
    {
        holdsBall = false;
        inCoolDown = coolDown;
        Transform ball = gameObject.transform.GetChild(0);
        ball.GetComponent<Rigidbody>().isKinematic = false;
        ball.GetComponent<Ball>().enabled = true;
        ball.parent = null;
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

        GameObject ball = Instantiate(GM.balls[index], ts) as GameObject;
        ball.GetComponent<Rigidbody>().isKinematic = true;
        ball.transform.parent = this.transform;
        holdsBall = true;
    }
}
