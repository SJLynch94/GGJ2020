using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

[RequireComponent(typeof(HealthComponent))]
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAI : MonoBehaviour
{

    [SerializeField]
    protected Damage mDamageValues;

    [SerializeField]
    Slider healthBarSlider;
    [SerializeField]
    GameObject healthBar;
    private Transform camera;

    //public Damage DamageValues
    //{
    //    get { return mDamageValues; }
    //    set
    //    {
    //        mDamageValues = value;
    //    }
    //}

    EnemyState mEnemyState = EnemyState.Idle;

    [SerializeField]
    private string mEnemyName;

    [SerializeField]
    private uint mID;

    public uint ID
    {
        get { return mID; }
        set { mID = value; }
    }

    [SerializeField]
    private NavMeshAgent mNavAgent;

    [SerializeField]
    private uint mMovementSpeed;

    [SerializeField]
    private float mAttackRate;
    private float mAttackTimer;

    [SerializeField]
    private float mAttackRange;

    [SerializeField]
    private float mSpecialAttackRange;


    public int BuildingDmaage;

    public int UnitDamage;

    private float pathingTimer = 3.5f;

    public float AttackRate
    {
        get { return mAttackRate; }
        set { mAttackRate = value; }
    }

    public float AttackRange
    {
        get { return mAttackRange; }
        set { mAttackRange = value; }
    }

    private GameObject mTarget;

    public GameObject Target
    {
        get { return mTarget; }
        set { mTarget = value; }
    }


    [SerializeField]
    private float mIdleTime = 2.0f;

    [SerializeField]
    protected Animator mAnimator;

    [SerializeField]
    private uint mMovementMod;

    float distance;

    [SerializeField]
    public int mDeathCurrency;


    // Start is called before the first frame update
    public virtual void Awake()
    {
        camera = Camera.main.transform;
        mAttackRange = 1.5f;
        mSpecialAttackRange = 10.0f;
        mNavAgent = GetComponent<NavMeshAgent>();
        Target = new GameObject();
        mAnimator = GetComponent<Animator>();
        mDamageValues.mBuildingDamage = BuildingDmaage;
        mDamageValues.mUnitDamage = UnitDamage;
        mAttackTimer = mAttackRate;
    }

    private void Start()
    {
        healthBarSlider.value = GetComponent<HealthComponent>().HealthBarValue;
    }

    // Update is called once per frame
    public virtual void Update()
    {
        healthBarSlider.value = GetComponent<HealthComponent>().HealthBarValue;
        healthBarSlider.transform.LookAt(camera);

        switch (mEnemyState)
        {
            case EnemyState.Idle:
                mAnimator.SetBool("Attacking", false);
                mAnimator.SetBool("Moving", false);
                mAnimator.SetBool("Idling", true);
                if ((mIdleTime -= (Time.fixedDeltaTime * 2)) <= 0.0f) mEnemyState = EnemyState.Choosing;
                break;
            case EnemyState.Choosing:
                if (PickBestBuilding())
                {
                    mEnemyState = EnemyState.Moving;
                }
                else
                {
                    mEnemyState = EnemyState.Choosing;
                }
                break;
            case EnemyState.Moving:
                pathingTimer -= Time.deltaTime;
                if(pathingTimer<0.0f)
                {
                    mEnemyState = EnemyState.Choosing;
                    pathingTimer = 3.5f;
                }

                mAnimator.SetBool("Attacking", false);
                mAnimator.SetBool("Moving", true);
                mAnimator.SetBool("Idling", false);
                Movement();
                break;
            case EnemyState.Attacking:
                mAnimator.SetBool("Attacking", true);
                mAnimator.SetBool("Moving", false);
                mAnimator.SetBool("Idling", false);
                Attack();
                break;
        }
    }

    protected virtual void Attack()
    {
        if (distance < mAttackRange)
        {
            if (Target && Target.GetComponent<HealthComponent>())
            {
                if (Target.GetComponent<BuildingBase>())
                {
                    if(Target.GetComponent<BuildingBase>().IsActive)
                    {
                        if (mAttackTimer < mAttackRate)
                        {
                            mAttackTimer = mAttackRate;
                            Target.GetComponent<HealthComponent>().Damage(mDamageValues.mBuildingDamage);
                        }
                        else
                        {
                            mAttackTimer -= Time.fixedDeltaTime;
                        }
                    }
                    else
                    {
                        mEnemyState = EnemyState.Choosing;
                    }
                }
                else
                {
                    Target.GetComponent<HealthComponent>().Damage(mDamageValues.mUnitDamage);
                }
            }
           
        }
        
    }

    private void Movement()
    {
        //mAnimator.SetBool("Moving", true);
        NavAgent.speed = mMovementSpeed * mMovementMod;
        if (NavAgent)
        {
            if (Target)
            {
                distance = Vector3.Distance(Target.transform.position, NavAgent.transform.position);
                if (distance < mAttackRange)
                {
                    mEnemyState = EnemyState.Attacking;
                }
                else if (NavAgent.hasPath && !NavAgent.isPathStale)
                {
                    NavAgent.SetDestination(Target.transform.position);
                    mEnemyState = EnemyState.Moving;
                }
                else
                {
                    NavAgent.SetDestination(Target.transform.position);
                    mEnemyState = EnemyState.Moving;
                }
                
            }
        }
    }

    private bool PickBestBuilding()
    {
        Target = null;
        Target = FindClosestBuilding(transform.position);
        if (Target && Target.GetComponent<BuildingBase>().IsActive)
        {
            return true;
        }
        return false;
    }

    public GameObject FindClosestBuilding(Vector3 target)
    {
        GameObject closest = null;
        float closestDist = Mathf.Infinity;
        int index = 0;
        foreach (var b in FindObjectsOfType<BuildingBase>())
        {
            var dist = Vector3.Distance(transform.position, b.transform.position);
            if (dist < closestDist)
            {
                if(b.GetComponent<BuildingBase>().IsActive)
                {
                    closest = b.gameObject;
                    closestDist = dist;
                }
            }
            ++index;
        }
        if (closest)
        {
            return closest;
        }
        return null;
    }

    public NavMeshAgent NavAgent
    {
        get { return mNavAgent; }
        set { mNavAgent = value; }
    }
}
