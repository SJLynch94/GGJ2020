using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TownHallUIInfo : MonoBehaviour
{
    public Text BuildingNameText;
    public Text LevelUpText;
    public Text CostText;
    public Text CitizensCapacityText;
    public Slider HealthSlider;

    // Start is called before the first frame update
    void Start()
    {
        BuildingNameText.text = transform.parent.GetComponent<BuildingBase>().mTag;
        CostText.text = "5";
        CitizensCapacityText.text = transform.parent.GetComponent<BuildingBase>().mCurrentOccupancy.ToString() + " / " + transform.parent.GetComponent<BuildingBase>().mMaxOccupancy;
        LevelUpText.text = transform.parent.GetComponent<BuildingBase>().mBuildingPrice.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.parent.GetComponent<BuildingBase>().Level < 3)
        {
            LevelUpText.text = (transform.parent.GetComponent<BuildingBase>().mBuildingPrice * transform.parent.GetComponent<BuildingBase>().Level).ToString();
        }
        else
        {
            LevelUpText.text = "Max";
        }
        CitizensCapacityText.text = transform.parent.GetComponent<BuildingBase>().mCurrentOccupancy.ToString() + " / " + transform.parent.GetComponent<BuildingBase>().mMaxOccupancy;        
    }
}
