using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Color gizmosColour = Color.red;

    public enum EEnemyType
    {
        Knight,
        Goblin,
        Golem,
        Wizard
    }

    EEnemyType mEnemyType = EEnemyType.Knight;

    public GameObject mKnightPrefab;
    public GameObject mGolemPrefab;
    public GameObject mGoblinPrefab;
    public GameObject mWizardPrefab;

    private Dictionary<int, GameObject> mEnemies = new Dictionary<int, GameObject>(4);

    public int mTotalEnemies;
    private int mNumEnemies = 0;
    private int mSpawnedEnemies = 0;
    public int mNumEnemiesMod = 1;

    private uint mSpawnID;
    public List<uint> mSpawnIDList;

    private bool mWaveSpawn = true;
    public bool mSpawn = false;

    public float mWaveTimer;
    private float mTimeTillWave = 0.0f;


    public int mTotalWaves;
    private int mNumWaves = 0;

    private GameObject mTerrain;

    GameObject[] mSpawnPoints;

    public Vector3 centre;
    public Vector3 size;

    AISpawn mAISpawner;
    EnemyAI mEnemyAI;

    ParticleSystem mParticleSystem;

    float mParticleTimer = 1;

    EnemyAI[] mEnemiesAlive;

    // Start is called before the first frame update
    void Start()
    {
        mAISpawner = GetComponentInChildren(typeof(AISpawn)) as AISpawn;
        mTerrain = FindObjectOfType<Terrain>().gameObject;
        mSpawnPoints = GameObject.FindGameObjectsWithTag("SpawnArea");
        mEnemies.Add(0, mKnightPrefab);
        mEnemies.Add(1, mGoblinPrefab);
        mEnemies.Add(2, mGolemPrefab);
        mEnemies.Add(3, mWizardPrefab);
    }

    // Update is called once per frame
    void Update()
    {
        GameObject temp = GameObject.FindGameObjectWithTag("GameManager");
        

        

        if (mSpawn)
        {
            SpawnEnemy();

            if(mNumEnemies >= mTotalEnemies)
            {
                mSpawn = false;    
            }   
        }

        mEnemiesAlive = FindObjectsOfType<EnemyAI>();//GameObject.FindGameObjectsWithTag("Enemy");

        if (mEnemiesAlive.Length == 0 && temp.GetComponent<GameManager>().currentGamePlayState == GamePlayState.Defending)
        {
            ++mNumWaves;
            mTotalEnemies += mNumEnemiesMod;
            temp.GetComponent<GameManager>().currentGamePlayState = GamePlayState.Building;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = gizmosColour;
        Gizmos.DrawCube(transform.position, new Vector3(0.5f, 0.5f, 0.5f));
        Gizmos.DrawWireCube(centre, size);
    }

    private void SpawnEnemy()
    {
        ++mNumEnemies;
        ++mSpawnedEnemies;
        GameObject b; //= mSpawnPoints[Random.Range(0, mSpawnPoints.Length - 1)];
        if(mNumWaves < 3)
        {
            b = mSpawnPoints[0];
        }
        else if (mNumWaves < 6)
        {
            b = mSpawnPoints[Random.Range(0, 2)];
        }
        else if (mNumWaves < 9)
        {
            b = mSpawnPoints[Random.Range(0, 3)];
        }
        else
        {
            b = mSpawnPoints[Random.Range(0, mSpawnPoints.Length - 1)];
        }
        float randomX = b.GetComponent<BoxCollider>().size.x / 2 + b.transform.position.x;
        float randomZ = b.GetComponent<BoxCollider>().size.z / 2 + b.transform.position.z;
        Vector3 point = new Vector3(b.transform.position.x, mTerrain.transform.position.y + 0.5f, b.transform.position.z);
        var index = Random.Range(0, 4);
        GameObject e = Instantiate(mEnemies[index], point, Quaternion.identity);
        //PlaySpawnEffect(e);
        SetID();
        mAISpawner.SetName(mSpawnID);
        mEnemyAI = e.GetComponent<EnemyAI>();
        mEnemyAI.ID = mSpawnID;
    }

    void PlaySpawnEffect(GameObject e)
    {
        mParticleSystem.transform.position = e.transform.position;
        while ((mParticleTimer -= (Time.deltaTime * 2)) > 0)
        {
            mParticleSystem.Play();
        }
    }

    void StopSpawnEffect()
    {
        mParticleSystem.Stop();
    }

    public void SetID()
    {
        mSpawnID = (uint)Random.Range(1, 1000);
        if (!mSpawnIDList.Contains(mSpawnID))
        {
            mSpawnIDList.Add(mSpawnID);
        }
        else
        {
            SetID();
        }
    }

    public void KillEnemy(uint sID)
    {
        for (var i = 0; i < mSpawnIDList.Count; ++i)
        {
            if (sID == mSpawnIDList[i])
            {
                --mNumEnemies;
                mSpawnIDList.Remove(sID);
            }
        }
    }

    public void EnableSpawner(uint sID)
    {
        if (mSpawnID == sID)
        {
            mSpawn = true;
        }
    }

    public void DisableSpawner(uint sID)
    {
        if (mSpawnID == sID)
        {
            mSpawn = false;
        }
    }

    public float TimeTillWave
    {
        get { return mTimeTillWave; }
        set { mTimeTillWave = value; }
    }

    public void EnableTrigger()
    {
        mSpawn = true;
    }

    public uint GetSpawnID
    {
        get { return mSpawnID; }
    }
}
