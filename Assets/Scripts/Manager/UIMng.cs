using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMng : MonoBehaviour
{
    [SerializeField] Text _BallDamageText;
    [SerializeField] Text _AttackSpeedText;

    private void Update()
    {
        _BallDamageText.text = BallMng.Data.BallDamage.ToString();
        _AttackSpeedText.text = BallMng.Data.ShootDelay.ToString();
    }
}
