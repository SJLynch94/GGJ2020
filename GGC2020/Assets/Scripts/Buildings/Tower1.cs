using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower1 : BuildingBase
{
    public int population;
    private float fireRate;
    private float fireTimer;
    private float fireTimerMax = 2.0f;
    public int range;
    public int damage;

    private GameObject[] enemies;
    private GameObject closestEnemy;
    private float closeDist;
    // Start is called before the first frame update
    void Start()
    {
        fireTimer = fireTimerMax;
        //tower.getcomp<HealthComp>.RemoveHealth()
    }

    // Update is called once per frame
    void Update()
    {
        fireRate = fireTimerMax / population;

        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        for(int i = 0; i < enemies.Length; i++)
        {
            GameObject tmp = enemies[i];
            float dist = Vector3.Distance(tmp.transform.position, transform.position);
            if (closestEnemy != null) { closeDist = Vector3.Distance(closestEnemy.transform.position, transform.position); }
            if (closestEnemy == null) { closestEnemy = tmp; }
            if( dist < closeDist) { closestEnemy = tmp; }
        }

        closeDist = Vector3.Distance(closestEnemy.transform.position, transform.position);
        if (closeDist <= range)
        {
            if(fireTimer >= 0.0f)
            {
                fireTimer -= Time.fixedDeltaTime;

            }
            else
            {
                fireTimer = fireRate;
                Debug.Log("PEW");
                closestEnemy.GetComponent<HealthComponent>().Damage(damage);
            }
        }
    }
}
