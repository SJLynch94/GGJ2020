using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Gamestate {MainMenu, Paused, Play}

public enum GamePlayState { MainMenu, Building, Defending}
public enum Elements { Fire, Water, Earth, Lightning };

public enum EnemyState { Idle, Choosing, Moving, Attacking, Dead };

public struct Damage
{
    public int mBuildingDamage;
    public int mUnitDamage;
    public Damage(int bd, int ud)
    {
        mBuildingDamage = bd;
        mUnitDamage = ud;
    }
}
