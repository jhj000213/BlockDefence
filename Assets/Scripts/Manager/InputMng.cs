using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputMng : MonoBehaviour
{
    [SerializeField] CheatMng _CheatMng;//temp

    Vector2 _MouseDownPos;
    Vector2 _MouseUpPos;
    Vector2 _MouseNowPos;
    bool _IsMouseDown;

    bool _MouseDown;
    float _Angle;



    private void Start()
    {
        _IsMouseDown = false;
    }

    private void Update()
    {
        //터치위치 해상도 대응
        _MouseNowPos = Input.mousePosition * new Vector2(1080.0f / Screen.width, 1920.0f / Screen.height);

        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.X))
        {
            _MouseDownPos = _MouseNowPos;

            BallMng.Data.Action_Shoot(_MouseDownPos);
        }
        if(Input.GetKeyDown(KeyCode.A))
        {
            _CheatMng.SetAuto();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            _CheatMng.SetAttackSpeedHigh();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            _CheatMng.SetAttackDamageHigh();
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            _CheatMng.SetBlockMakeSpeedUp();
        }
    }
}
