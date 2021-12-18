using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class instantiaterChild : MonoBehaviour
{
    public Instantiater instantiater;
    public Rigidbody rb;

    private Transform leftLimit;
    private Transform rightLimit;
    private Touch touch;
    private LineRenderer lineRenderer;

    private void Start()
    {
        leftLimit = instantiater.leftLimit;
        rightLimit = instantiater.rightLimit;
        Ball ball = GetComponent<Ball>();
        ball.isLocked = true;
        rb = GetComponent<Rigidbody>();
        lineRenderer = GetComponent<LineRenderer>();
    }


    public void Update()
    {
            if (Input.touchCount > 0)
            {
                touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Moved)
                {
                transform.position = new Vector3(transform.position.x + touch.deltaPosition.x * instantiater.moveSpeed * Time.deltaTime, transform.position.y, transform.position.z);
                transform.position = new Vector3(Mathf.Clamp(transform.position.x, leftLimit.position.x + transform.localScale.x / 2, rightLimit.position.x - transform.localScale.x / 2), transform.position.y, transform.position.z);
                }

            }
            if (touch.phase == TouchPhase.Ended)
            {
                Drop();
            }
        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, hit.point);
        }

    }

    private void Drop()
    {
        instantiater.Drop();
        rb.isKinematic = false;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        GetComponent<Ball>().enabled = true;
        GetComponent<Ball>().isLocked = false;
        transform.parent = null;
        Destroy(GetComponent<instantiaterChild>());
    }

}
