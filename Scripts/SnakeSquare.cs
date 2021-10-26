using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeSquare : MonoBehaviour
{
    public GameObject God;

    void Update()
    {
        God=GameObject.Find("Map");
    }

    //If collides with itself, end the game
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag=="Snake")
        {
            print("-------------------GAME OVER------------------");
            God.GetComponent<God>().DestroySnake();
            //Application.Quit();
        }
              
    }

    //If collides with apple then destroy apple and spawn new one
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag=="Apple")
        {
            Destroy(col.gameObject);
            God.GetComponent<God>().SpawnApple();
        }     
    }
}
