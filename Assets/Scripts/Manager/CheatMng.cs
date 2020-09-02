using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatMng : MonoBehaviour
{
    public void SetAuto()
    {
        BallMng.Data.AutoShoot = true;
    }

    public void SetAttackSpeedHigh()
    {
        BallMng.Data.ShootDelay = 0.05f;
    }

    public void SetAttackDamageHigh()
    {
        BallMng.Data.BallDamage = 5;
    }

    public void SetBlockMakeSpeedUp()
    {
        BlockMng.Data.MakeTimeDelay = 0.7f;
    }
}
