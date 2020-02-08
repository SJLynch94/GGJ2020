using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIBuyingInfo : MonoBehaviour
{


    public Text BuildingNameText;
    public Text CostText;
    //Quaternion startRot;


    // Start is called before the first frame update
    void Start()
    {
        //startRot = transform.parent.transform.rotation;
        BuildingNameText.text = transform.parent.GetComponent<BuildingBase>().mTag;
        CostText.text = transform.parent.GetComponent<BuildingBase>().mBuildingPrice.ToString();

    }

    
}