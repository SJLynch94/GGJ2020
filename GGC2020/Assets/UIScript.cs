using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIScript : MonoBehaviour
{
    public Text mKillCount;
    public Text mWave;
    public Text mUICurrency;
    public Text mCitezens;
    GameObject temp;

    // Start is called before the first frame update
    void Start()
    {
        GameObject temp = GameObject.FindGameObjectWithTag("GameManager");
        mKillCount.text = "0";
        mWave.text = "0";
        mUICurrency.text = temp.GetComponent<GameManager>().currency.ToString();
        mCitezens.text = temp.GetComponent<GameManager>().citizens.ToString();

    }

    private void Update()
    {
        GameObject temp = GameObject.FindGameObjectWithTag("GameManager");
        mKillCount.text = temp.GetComponent<GameManager>().killCount.ToString();
        mWave.text = temp.GetComponent<GameManager>().wave.ToString();
        mUICurrency.text = temp.GetComponent<GameManager>().currency.ToString();
        mCitezens.text = temp.GetComponent<GameManager>().citizens.ToString();
    }


}
