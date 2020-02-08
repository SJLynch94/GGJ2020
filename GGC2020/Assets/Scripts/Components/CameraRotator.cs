using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotator : MonoBehaviour
{
    public float speed; 

    // Update is called once per frame
    void Update()
    {
        GameObject[] temp = GameObject.FindGameObjectsWithTag("GameManager");
        if (temp[0].GetComponent<GameManager>().currentGamestate == Gamestate.MainMenu)
        {
            transform.Rotate(0, speed * Time.deltaTime, 0);
        }
    }
}
