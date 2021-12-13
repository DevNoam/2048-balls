using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class instantiaterChild : MonoBehaviour
{
    [SerializeField]
    public Instantiater instantiater;
    public Rigidbody rb;

    public Transform leftLimit;
    public Transform rightLimit;
    public float moveSpeed = 1f;
    private Touch touch;

    private void Start()
    {
        Ball ball = GetComponent<Ball>();
        //rightLimit = ball.rightLimit;
        //leftLimit = ball.leftLimit;
        rb = GetComponent<Rigidbody>();
        leftLimit = GameObject.Find("LeftLimit").GetComponent<Transform>();
        rightLimit = GameObject.Find("RightLimit").GetComponent<Transform>();
    }

    public void Update()
    {
            if (Input.touchCount > 0)
            {
                touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Moved)
                {
                transform.position = new Vector3(transform.position.x + touch.deltaPosition.x * moveSpeed * Time.deltaTime, transform.position.y, transform.position.z);
                transform.position = new Vector3(Mathf.Clamp(transform.position.x, leftLimit.position.x + transform.localScale.x / 2, rightLimit.position.x - transform.localScale.x / 2), transform.position.y, transform.position.z);
                }

            }
            if (touch.phase == TouchPhase.Ended)
            {
                Drop();
            }
    }

    private void Drop()
    {
        instantiater.holdsBall = false;
        instantiater.inCoolDown = instantiater.coolDown;
        rb.isKinematic = false;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        GetComponent<Ball>().enabled = true;
        Destroy(GetComponent<instantiaterChild>());
        transform.parent = null;
    }

}
