using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : Object
{
    Rigidbody2D _Rigidbody;

    readonly float _Speed = 20;
    //공의 현재 속도
    float _Magnitude;

    float _StopYPos;

    //좌, 우로 이동할 때 가지는 최소전
    readonly float _MinArrowAngle = 10.0f;


    public void Init(Vector2 pos)
    {
        base.Init(pos);
        _Rigidbody = GetComponent<Rigidbody2D>();
        _StopYPos = pos.y;
    }

    private void FixedUpdate()
    {
            if (_Rigidbody.velocity.magnitude < _Magnitude ||
                _Rigidbody.velocity.magnitude > _Magnitude)
                SpeedReset();
            HorizontalAngleCheck();
        
    }

    void SpeedReset()
    {
        float angle = Mathf.Atan2(_Rigidbody.velocity.y, _Rigidbody.velocity.x);
        float dx = Mathf.Cos(angle) * _Speed;
        float dy = Mathf.Sin(angle) * _Speed;
        _Rigidbody.velocity = new Vector2(dx, dy);
    }

    public void SetVelocity(float angle)
    {
        SetCanMove();
        angle *= Mathf.Deg2Rad;
        float dx = Mathf.Cos(angle) * _Speed;
        float dy = Mathf.Sin(angle) * _Speed;
        _Rigidbody.velocity = new Vector2(dx,dy);
        _Magnitude = _Rigidbody.velocity.magnitude;
    }
    public void SetVelocity(Vector2 pos)
    {
        SetCanMove();
        _Rigidbody.velocity = pos;
        _Magnitude = _Rigidbody.velocity.magnitude;
    }
    void SetCanMove()
    {
        _Rigidbody.constraints = RigidbodyConstraints2D.None;
        _Rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
        _Rigidbody.isKinematic = false;
    }

    //좌, 우로 이동할떄 가지는 최소 각도 보정 
    void HorizontalAngleCheck()
    {
        float nowangle = Mathf.Atan2(_Rigidbody.velocity.y, _Rigidbody.velocity.x) * Mathf.Rad2Deg;
        if (nowangle >= 0 && nowangle < _MinArrowAngle)
            nowangle = _MinArrowAngle;
        else if (nowangle > 180 - _MinArrowAngle && nowangle <= 180)
            nowangle = 180 - _MinArrowAngle;
        else if (nowangle < 0 && nowangle > -_MinArrowAngle)
            nowangle = -_MinArrowAngle;
        else if (nowangle > -180 + _MinArrowAngle && nowangle < -180)
            nowangle = -180 + _MinArrowAngle;

        nowangle *= Mathf.Deg2Rad;
        float dx = Mathf.Cos(nowangle) * _Speed;
        float dy = Mathf.Sin(nowangle) * _Speed;
        _Rigidbody.velocity = new Vector2(dx, dy);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Paddle"))
        {
            LandingBall();

        }
    }

    void LandingBall()
    {

    }

    public void Other_LandingBall()
    {
        _Rigidbody.velocity = Vector2.zero;
        _Rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
        _Rigidbody.isKinematic = true;
        transform.localPosition = new Vector3(transform.localPosition.x, _StopYPos);
    }

    public Vector2 GetVelocity() { return _Rigidbody.velocity; }
}
