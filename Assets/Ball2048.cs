using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball2048 : MonoBehaviour
{
    private GameManager GM;
    void Start()
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        Invoke("addScore", 0.2f);
        Invoke("autoDestroy", 0.8f);
    }
    void addScore()
    {
        GM.get2048++;
    }
    void autoDestroy()
    {
        //Particle
        Destroy(this.gameObject);
    }
}
