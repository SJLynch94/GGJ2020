using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Job : ThreadedJob
{

    public Vector3 mDestintation { get; set; }
    public Vector3 mMyTransform { get; set; }
    public List<Vector3> pastPositions { get; set; }
    public List<BuildingBase> buildings { get; set; }
    public float mMaxDistance { get; set; }

    protected override void ThreadFunction()
    {
        // This is where we do the pathing within the NavMesh
        buildings = new List<BuildingBase>();
        buildings.AddRange(FindObjectsOfType<BuildingBase>());
        GameObject buildingToGo = new GameObject();
        buildingToGo = FindClosestBuilding(mMyTransform);


    }

    public GameObject FindClosestBuilding(Vector3 target)
    {
        GameObject closest = null;
        float closestDist = Mathf.Infinity;
        int index = 0;
        foreach(var b in buildings)
        {
            var dist = Vector3.Distance(mMyTransform, b.transform.position);
            if(dist < closestDist)
            {
                closest = b.gameObject;
                closestDist = dist;
            }
            ++index;
        }
        if(!closest)
        {
            return closest;
        }
        return null;
    }

    protected override void OnFinished()
    {
        Debug.Log("I got to OnFinished In Job");
        base.OnFinished();
    }
}
