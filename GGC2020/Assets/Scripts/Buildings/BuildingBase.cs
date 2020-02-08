using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

[RequireComponent(typeof(NavMeshObstacle))]
[RequireComponent(typeof(HealthComponent))]
public class BuildingBase : MonoBehaviour
{
    [Range(0, 1000)]
    public uint mOccupancy;

    public GameObject visionLightPrefab;

    [SerializeField]
    public bool IsActive = false;

    [SerializeField]
    public bool IsBought = false;

    Damage mDamage;

    [SerializeField]
    public string mTag;

    public int mMaxOccupancy;
    public int mCurrentOccupancy;
    public int mBuildingPrice;
    private float fireRate;
    private float fireTimer;
    private float fireTimerMax = 3.0f;
    public int range;
    [SerializeField]
    private Transform ProjectileDummy;
    [SerializeField]
    private GameObject Arrow;
    private GameObject[] enemies;
    private GameObject closestEnemy;
    private float closeDist;
    GameObject visionLight;
    public int Level = 0;
    [SerializeField]
    Slider healthBarSlider;
    [SerializeField]
    GameObject healthBar;
    private Transform camera;

    public int UnitDamage;
    public int BuildingDamage;



    // Start is called before the first frame update
    void Awake()
    {
        camera = Camera.main.transform;
        visionLight = Instantiate(visionLightPrefab, new Vector3(0, 0.5f, 0), Quaternion.Euler(90.0f, 0.0f, 0.0f)) as GameObject;
        visionLight.transform.parent = transform;
        fireTimer = fireTimerMax;
        //if (!IsActive)
        //{
        //    IsActive = !IsActive;
        //}
        //else
        //{
        //    gameObject.SetActive(true);
        //}
        mDamage.mBuildingDamage = BuildingDamage;
        mDamage.mUnitDamage = UnitDamage;
    }

    private void Start()
    {
        if(healthBarSlider)
        {
            healthBarSlider.value = GetComponent<HealthComponent>().HealthBarValue;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(healthBarSlider)
        {
            if(GetComponent<HealthComponent>().HealthBarValue < 1.0f)
            {
                int i = 1;
            }
            healthBarSlider.value = GetComponent<HealthComponent>().HealthBarValue;
            healthBarSlider.transform.LookAt(camera);
        }
        fireRate = fireTimerMax / mCurrentOccupancy;

        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        for (int i = 0; i < enemies.Length; i++)
        {
            GameObject tmp = enemies[i];
            float dist = Vector3.Distance(tmp.transform.position, transform.position);
            if (closestEnemy != null) { closeDist = Vector3.Distance(closestEnemy.transform.position, transform.position); }
            if (closestEnemy == null) { closestEnemy = tmp; }
            if (dist < closeDist) { closestEnemy = tmp; }
        }

        if (closestEnemy != null)
        {
            closeDist = Vector3.Distance(closestEnemy.transform.position, transform.position);


            if (closeDist <= range)
            {
                if (fireTimer >= 0.0f)
                {
                    fireTimer -= Time.fixedDeltaTime;
                }
                else
                {
                    fireTimer = fireRate;
                    ProjectileDummy.LookAt(closestEnemy.transform.position);
                    Instantiate(Arrow, ProjectileDummy.position, ProjectileDummy.rotation);

                    //Vector3 projectileVector = closestEnemy.transform.position - transform.position;

                    GameObject projectile;
                    projectile = Instantiate(Arrow) as GameObject;

                    PleaseWait();
                    closestEnemy.GetComponent<HealthComponent>().Damage(mDamage.mUnitDamage);
                }
            }
        }
    }

    public void BuyBuilding()
    {
        if (IsActive)
        {
            if (!IsBought)
            {
                IsBought = !IsBought;
            }
        }
    }

    IEnumerator PleaseWait()
    {
        yield return new WaitForSeconds(2.0f);
    }
}
