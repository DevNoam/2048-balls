using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endGame : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            if (collision.gameObject.GetComponent<Rigidbody>().velocity.magnitude >= 1)
            {
                //end game
                Debug.Log("END!");
            }
        }
    }
}
