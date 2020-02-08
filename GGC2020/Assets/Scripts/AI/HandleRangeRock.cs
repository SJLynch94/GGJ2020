using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleRangeRock : MonoBehaviour
{
    public int BuildingDamage;
    public int UnitDamage;
    private Damage mDamage;

    public float mMaxDist;
    public float hSpeed;
    public float g;
    public float mTotalTime;
    public float vSpeed;

    public Rigidbody mRB;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        mDamage.mBuildingDamage = BuildingDamage;
        mDamage.mUnitDamage = UnitDamage;
        mRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other)
        {
            if(other.GetComponent<HealthComponent>() && other.GetComponent<BuildingBase>().IsActive)
            {
                other.GetComponent<HealthComponent>().Damage(mDamage.mBuildingDamage);
            }
        }
    }
}
