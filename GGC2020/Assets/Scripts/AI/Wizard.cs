using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : EnemyAI
{

    float mParticleFireTimer;
    float mProjectileFireTimer = 4.0f;
    bool firingParticles;

    private ParticleSystem particleSystem;

    private BoxCollider mBoxCollider;
    // Start is called before the first frame update
    public override void Awake()
    {
        mParticleFireTimer = 0.0f;
        firingParticles = false;
        particleSystem = GetComponentInChildren<ParticleSystem>();
        particleSystem.Stop();
        mBoxCollider = GetComponentInChildren<BoxCollider>();
        base.Awake();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    protected override void Attack()
    {
        if (mProjectileFireTimer < 0.0f)
        {
            // Fire Projectile
            mParticleFireTimer = 0.3f;
            firingParticles = true;
            mProjectileFireTimer = 4.0f;
        }    

        if(firingParticles)
        {
            particleSystem.Play();
            mParticleFireTimer -= Time.deltaTime;
            mBoxCollider.enabled = true;
        }
        else
        {
            particleSystem.Stop();
            mProjectileFireTimer -= Time.fixedDeltaTime;
            mBoxCollider.enabled = false;
        }                                  

        if(mParticleFireTimer <= 0.0f)
        {
            firingParticles = false;
        }
        base.Attack();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other)
        {
            if(other.GetComponent<HealthComponent>())
            {
                if (Target.GetComponent<BuildingBase>() && Target.GetComponent<BuildingBase>().IsActive)
                {
                    Target.GetComponent<HealthComponent>().Damage(mDamageValues.mBuildingDamage);
                }
                else
                {
                    Target.GetComponent<HealthComponent>().Damage(mDamageValues.mUnitDamage);
                }
            }
        }
    }


}
