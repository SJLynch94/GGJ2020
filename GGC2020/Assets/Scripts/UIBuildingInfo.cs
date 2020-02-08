using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBuildingInfo : MonoBehaviour
{
    public Text BuildingNameText;
    public Slider HealthPerCentage;
    public Text OccupencyText;
    public Text CostText;    

    public Button OccupencyIncreaseButton;
    public Button LevelUpButton;

    // Start is called before the first frame update
    void Start()
    {
        BuildingNameText.text = transform.parent.GetComponent<BuildingBase>().mTag;
        CostText.text = transform.parent.GetComponent<BuildingBase>().mBuildingPrice.ToString() + " / " + transform.parent.GetComponent<BuildingBase>().mMaxOccupancy;

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.parent.GetComponent<BuildingBase>().Level < 3)
        {
            CostText.text = (transform.parent.GetComponent<BuildingBase>().mBuildingPrice * transform.parent.GetComponent<BuildingBase>().Level).ToString();
        }
        else
        {
            CostText.text = "Max";
        }
        OccupencyText.text = transform.parent.GetComponent<BuildingBase>().mCurrentOccupancy.ToString() + " / " + transform.parent.GetComponent<BuildingBase>().mMaxOccupancy;

    }


    //public void AddOccupency()
    //{
    //    ++transform.parent.GetComponent<BuildingBase>().mOccupancy;
    //    string text = transform.parent.GetComponent<BuildingBase>().mOccupancy.ToString() + " / " + transform.parent.GetComponent<BuildingBase>().mMaxOccupancy;
    //    OccupencyText.text = text;
    //}




}
