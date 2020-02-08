using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem : EnemyAI
{
    public GameObject mRockPrefab;

    float mProjectileFireTimer = 4.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    protected override void Attack()
    {
        if(mProjectileFireTimer < 0.0f)
        {
            mAnimator.SetBool("Special Attacking", true);
            mProjectileFireTimer = 4.0f;
            GameObject g = Instantiate(mRockPrefab, transform.position, Quaternion.identity);
            g.GetComponent<HandleRangeRock>().g = Physics.gravity.magnitude;
            g.GetComponent<HandleRangeRock>().mTotalTime = g.GetComponent<HandleRangeRock>().mMaxDist / g.GetComponent<HandleRangeRock>().hSpeed;
            g.GetComponent<HandleRangeRock>().vSpeed = (g.GetComponent<HandleRangeRock>().mTotalTime * g.GetComponent<HandleRangeRock>().g) / 2;
            g.GetComponent<HandleRangeRock>().mRB.velocity = new Vector3(g.transform.forward.x * g.GetComponent<HandleRangeRock>().hSpeed,
            g.GetComponent<HandleRangeRock>().vSpeed, g.transform.forward.z * g.GetComponent<HandleRangeRock>().hSpeed);
        }
        else
        {
            mAnimator.SetBool("Special Attacking", false);
        }

        mProjectileFireTimer -= Time.deltaTime;

        base.Attack();
    }
}
