using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] sounds;

    AudioSource sound;

    // Start is called before the first frame update
    void Start()
    {

        sound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] temp = GameObject.FindGameObjectsWithTag("GameManager");
        if (temp[0].GetComponent<GameManager>().currentGamePlayState == GamePlayState.MainMenu)
        {
            sound.clip = sounds[0];
        }
        else if (temp[0].GetComponent<GameManager>().currentGamePlayState == GamePlayState.Building)
        {
            sound.clip = sounds[1];
            if (!sound.isPlaying)
                sound.Play();
        }
        else if (temp[0].GetComponent<GameManager>().currentGamePlayState == GamePlayState.Defending)
        {
            sound.clip = sounds[2];
            if (!sound.isPlaying)
                sound.Play();
        }


    }



}