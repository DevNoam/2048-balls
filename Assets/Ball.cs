using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public bool isLocked = false;
    private GameObject targetObj;
    private GameManager GM;

    private void Start()
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == gameObject.tag)
        {
            Debug.Log("Found Matching ball!");
            if (isLocked == false)
            {
                if (other.gameObject.GetComponent<Ball>().isLocked == true)
                {
                    return; //Abort, other ball already locked!
                }
                isLocked = true;
                other.gameObject.GetComponent<Ball>().isLocked = true;
                Debug.Log("Locked");
                targetObj = other.gameObject;
            }
            else
            {
                return;
            }
        }
    }

    private void Update()
    {
        if (isLocked == true && targetObj.transform != null)
        {
            float step = 5f * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, targetObj.transform.position, step);
        }
        if (isLocked == true && Vector3.Distance(transform.position, targetObj.transform.position) <= 0.1f)
        {
            instantiateNew();
        }
    }

    private void instantiateNew()
    {
        Destroy(targetObj);
        GameObject ballToInstantiate = null;
        for (int i = 0; i < GM.balls.Length; i++)
        {
            if (this.tag == GM.balls[i].tag)
            {
                if (i++ > GM.balls.Length)
                {
                    Debug.Log("This is the max value ball can have");
                }
                else
                {
                    ballToInstantiate = GM.balls[i++];
                }
            }
        }
        if (ballToInstantiate != null)
        {
            GameObject ball = Instantiate(ballToInstantiate, this.transform.position, this.transform.rotation) as GameObject;
            ball.GetComponent<Ball>().enabled = true;
            Debug.Log("Instantaiated");
        }
        //merge
        Destroy(this.gameObject);
    }
}
