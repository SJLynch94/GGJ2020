using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    Rigidbody rigidbody;

    float projectileSpeed = 10.0f;
    float finalProjectileSpeed;
    float life = 1.5f;

    // Start is called before the first frame update
    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate((Vector3.forward * Time.fixedDeltaTime) * projectileSpeed);
        life -= Time.fixedDeltaTime;

        if(life <= 0.0f)
        {
            Destroy(gameObject);
        }
        //finalProjectileSpeed = projectileSpeed * Time.fixedDeltaTime;
        //rigidbody.velocity = transform.forward * finalProjectileSpeed; 
    }
}
