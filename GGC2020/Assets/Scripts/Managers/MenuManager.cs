using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    private Quaternion cameraRotation;
    public GameObject currentbuilding;

    GameObject temp;
    // Start is called before the first frame update
    void Start()
    {
        temp = GameObject.FindGameObjectWithTag("GameManager");
    }

    public void ExitButton()
    {
        Application.Quit();
    }
    public void PlayButton()
    {
        //GameObject[] temp = GameObject.FindGameObjectsWithTag("GameManager");
        temp = GameObject.FindGameObjectWithTag("GameManager");
        temp.GetComponent<GameManager>().currentGamestate = Gamestate.Play;
        temp.GetComponent<GameManager>().currentGamePlayState = GamePlayState.Building;


        transform.Find("MainMenu").gameObject.SetActive(false);
        transform.Find("UICanvas").gameObject.SetActive(true);

        GameObject[] temp2 = GameObject.FindGameObjectsWithTag("CameraPosition");

        temp.GetComponent<Transform>().rotation = cameraRotation;

        foreach (var b in FindObjectsOfType<BuildingBase>())
        {
            b.IsActive = false;
            if (b.mTag == "Town Hall")
            {
                b.IsActive = true;
            }
        }



        //SetCamera Angle Again 

    }
    public void OptionsButton()
    {
        transform.Find("MainMenu").gameObject.SetActive(false);
        transform.Find("OptionsMenu").gameObject.SetActive(true);
    }

    public void NextWaveButton()
    {
        temp = GameObject.FindGameObjectWithTag("GameManager");

        if (temp.GetComponent<GameManager>().currentGamePlayState == GamePlayState.Building)
        {
            temp.GetComponent<GameManager>().currentGamePlayState = GamePlayState.Defending;
            temp.GetComponent<Spawner>().mSpawn = true;
            temp.GetComponent<GameManager>().wave++;
        }
    }

    public void BuyBuilding()
    {
        temp = GameObject.FindGameObjectWithTag("GameManager");
        if (temp.GetComponent<GameManager>().currentGamePlayState == GamePlayState.Building)
        {
            foreach (var g in GameObject.FindGameObjectsWithTag("Building"))
            {
                if (g.transform.Find("BuyingMenu(Clone)") != null && g.transform.Find("BuyingMenu(Clone)").gameObject.activeSelf)
                {
                    if (temp.GetComponent<GameManager>().currency >= g.GetComponent<BuildingBase>().mBuildingPrice)
                    {
                        temp.GetComponent<GameManager>().currency -= (int)g.GetComponent<BuildingBase>().mBuildingPrice;
                        Quaternion startRot = g.transform.rotation;
                        startRot.y = 0;
                        g.GetComponent<BuildingBase>().IsBought = true;
                        g.GetComponent<BuildingBase>().IsActive = true;
                        //g.transform.rotation = startRot;
                        g.transform.Rotate(0, 0, -180);
                        g.transform.Find("BuyingMenu(Clone)").gameObject.SetActive(false);
                        g.GetComponent<BuildingBase>().Level = 1;
                    }
                    // break;
                }
            }
        }
    }

    public void OptionsBackButton()
    {
        transform.Find("OptionsMenu").gameObject.SetActive(false);
        transform.Find("MainMenu").gameObject.SetActive(true);

    }

    public void BuyCitizen()
    {
        if (temp.GetComponent<GameManager>().currency >= 5 &&
            GameObject.FindGameObjectWithTag("Town Hall").GetComponent<BuildingBase>().mCurrentOccupancy <
            GameObject.FindGameObjectWithTag("Town Hall").GetComponent<BuildingBase>().mMaxOccupancy)
        {
            ++temp.GetComponent<GameManager>().citizens;
            ++GameObject.FindGameObjectWithTag("Town Hall").GetComponent<BuildingBase>().mCurrentOccupancy;
            temp.GetComponent<GameManager>().currency -= 5;
            // GameObject.FindGameObjectWithTag("Town Hall").GetComponent<BuildingBase>().mOccupancy

        }

    }

    public void AddCitzen()
    {
        foreach (var g in GameObject.FindGameObjectsWithTag("Building"))
        {
            if (g.transform.Find("BuildingMenuCanvas(Clone)") != null && g.transform.Find("BuildingMenuCanvas(Clone)").gameObject.activeSelf)
            {
                currentbuilding = g.gameObject;
                break;
            }
        }


        if (GameObject.FindGameObjectWithTag("Town Hall").GetComponent<BuildingBase>().mCurrentOccupancy > 0)
        {
            if (currentbuilding.GetComponent<BuildingBase>().mCurrentOccupancy < currentbuilding.GetComponent<BuildingBase>().mMaxOccupancy)
            {
                --GameObject.FindGameObjectWithTag("Town Hall").GetComponent<BuildingBase>().mCurrentOccupancy;
                ++currentbuilding.GetComponent<BuildingBase>().mCurrentOccupancy;
            }
        }
    }
    public void RemoveCitzen()
    {
        foreach (var g in GameObject.FindGameObjectsWithTag("Building"))
        {
            if (g.transform.Find("BuildingMenuCanvas(Clone)") != null && g.transform.Find("BuildingMenuCanvas(Clone)").gameObject.activeSelf)
            {
                currentbuilding = g.gameObject;
                break;
            }
        }


        if (currentbuilding.GetComponent<BuildingBase>().mCurrentOccupancy > 0)
        {
            if (GameObject.FindGameObjectWithTag("Town Hall").GetComponent<BuildingBase>().mCurrentOccupancy < GameObject.FindGameObjectWithTag("Town Hall").GetComponent<BuildingBase>().mMaxOccupancy)
            {
                ++GameObject.FindGameObjectWithTag("Town Hall").GetComponent<BuildingBase>().mCurrentOccupancy;
                --currentbuilding.GetComponent<BuildingBase>().mCurrentOccupancy;
            }
        }
    }

    public void LevelUpBuilding()
    {
        if (currentbuilding.GetComponent<BuildingBase>().Level < 3)
        {
            foreach (var g in GameObject.FindGameObjectsWithTag("Building"))
            {
                if (g.transform.Find("BuildingMenuCanvas(Clone)") != null && g.transform.Find("BuildingMenuCanvas(Clone)").gameObject.activeSelf)
                {
                    currentbuilding = g.gameObject;
                    break;
                }
            }


            if (temp.GetComponent<GameManager>().currency >= currentbuilding.GetComponent<BuildingBase>().mBuildingPrice * (currentbuilding.GetComponent<BuildingBase>().Level))
            {
                temp.GetComponent<GameManager>().currency -= currentbuilding.GetComponent<BuildingBase>().mBuildingPrice * (currentbuilding.GetComponent<BuildingBase>().Level);
                currentbuilding.GetComponent<BuildingBase>().mMaxOccupancy += currentbuilding.GetComponent<BuildingBase>().mMaxOccupancy;
                ++currentbuilding.GetComponent<BuildingBase>().Level;

            }
        }
    }


    public void LevelTownHall()
    {
        currentbuilding = GameObject.FindGameObjectWithTag("Town Hall").GetComponent<BuildingBase>().gameObject;
        if (currentbuilding.GetComponent<BuildingBase>().Level < 3)
        {
            foreach (var g in GameObject.FindGameObjectsWithTag("Building"))
            {
                if (g.transform.Find("BuildingMenuCanvas(Clone)") != null && g.transform.Find("BuildingMenuCanvas(Clone)").gameObject.activeSelf)
                {
                    currentbuilding = g.gameObject;
                    break;
                }
            }


            if (temp.GetComponent<GameManager>().currency >= currentbuilding.GetComponent<BuildingBase>().mBuildingPrice * (currentbuilding.GetComponent<BuildingBase>().Level))
            {
                temp.GetComponent<GameManager>().currency -= currentbuilding.GetComponent<BuildingBase>().mBuildingPrice * (currentbuilding.GetComponent<BuildingBase>().Level);
                currentbuilding.GetComponent<BuildingBase>().mMaxOccupancy += currentbuilding.GetComponent<BuildingBase>().mMaxOccupancy;
                ++currentbuilding.GetComponent<BuildingBase>().Level;

            }



        }


    }


}
