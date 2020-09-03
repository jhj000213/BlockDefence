using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatMng : MonoBehaviour
{
    public void SetAuto()
    {
        BallMng.Data.AutoShoot = !BallMng.Data.AutoShoot;
    }

    public void SetAttackSpeedHigh()
    {
        BallMng.Data.ShootDelay = 0.1f;
    }

    public void SetAttackDamageHigh()
    {
        BallMng.Data.BallDamage += 3;
    }

    public void SetBlockMakeSpeedUp()
    {
        BlockMng.Data.MakeTimeDelay -= 0.15f;
        if (BlockMng.Data.MakeTimeDelay < 0.1f)
            BlockMng.Data.MakeTimeDelay = 2.0f;
    }

    public void SetTrippleShot()
    {
        BallMng.Data.TrippleShot = !BallMng.Data.TrippleShot;
    }
}
