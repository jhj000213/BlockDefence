using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchMng : MonoBehaviour
{
    Vector2 _MouseDownPos;
    Vector2 _MouseUpPos;
    Vector2 _MouseNowPos;
    bool _IsMouseDown;

    bool _MouseDown;
    float _Angle;
    readonly float _MinArrowAngle = 10.0f;


    private void Start()
    {
        _IsMouseDown = false;
    }

    private void Update()
    {
        //터치위치 해상도 대응
        _MouseNowPos = Input.mousePosition * new Vector2(1080.0f / Screen.width, 1920.0f / Screen.height);

        if (Input.GetMouseButtonDown(0))
        {
            if (!_MouseDown)
            {
                _MouseDown = true;
                _MouseDownPos = _MouseNowPos;
            }
        }
        else if (_MouseDown)
        {
            _Angle = Mathf.Atan2(_MouseNowPos.y - _MouseDownPos.y, _MouseNowPos.x - _MouseDownPos.x) * Mathf.Rad2Deg;
            if ((_Angle < _MinArrowAngle && _Angle >= 0) || (_Angle < 0 && _Angle > -90))
                _Angle = _MinArrowAngle;
            else if (_Angle > 180 - _MinArrowAngle || (_Angle < 0 && _Angle <= -90))
                _Angle = 180 - _MinArrowAngle;



            if (Input.GetMouseButtonUp(0))
            {
                _MouseUpPos = _MouseNowPos;
                ShootAction();
                _MouseDown = false;
            }
        }

    }
 

    void ShootAction()
    {
        BallMng.Data.Shoot(_Angle);
    }
}
