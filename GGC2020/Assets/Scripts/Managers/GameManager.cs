using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Gamestate currentGamestate;
    public GamePlayState currentGamePlayState;
    public int currency = 100;
    public int citizens= 15;
    public int killCount;
    public int wave;

    // Start is called before the first frame update
    void Awake()
    {
        currency = 100;
        citizens = 15;
        killCount = 0;
        wave = 0;
        currentGamestate = Gamestate.MainMenu;
        currentGamePlayState = GamePlayState.MainMenu;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
