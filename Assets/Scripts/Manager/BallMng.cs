using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallMng : MonoBehaviour
{
    private static BallMng instance;
    public static BallMng Data
    {
        get
        {
            if (!instance)
            {
                instance = FindObjectOfType(typeof(BallMng)) as BallMng;
                if (instance == null)
                    Debug.Log("no Singleton obj");
            }
            return instance;
        }
    }


    [SerializeField] GameObject _Ball;
    [SerializeField] Transform _BallParent;

    //플레이어 공 객체
    List<Ball> _BallList = new List<Ball>();


    bool _AutoShoot;
    int _BallDamage;
    float _ShootTime;
    float _ShootDelay;
    [SerializeField] GameObject _ShootAngleLine;


    readonly int _MaxBallCount = 300;
    int _NowUsingBall = 0;
    readonly float _MinArrowAngle = 10.0f;

    //Auto
    public float _Auto_NowAngle;
    public int _Auto_AngleSign = 1;
    public float _Auto_AngleRotateSpeedPerSecond = 0.66f;

    bool _TrippleShot;


    //TODO : 이후 게임 시작 함수를 제작하여 씬에 들어왔을 경우 초기화 되도록 바꿔야함
    private void Start()
    {
        _TrippleShot = false;
        _NowUsingBall = 0;
        _BallDamage = 1;//임시 데미지
        ShootDelay = 0.2f;

        for (int i = 0; i < _MaxBallCount; i++)
        { 
            GameObject obj = Instantiate(_Ball, _BallParent);
            _BallList.Add(obj.GetComponent<Ball>());
            _BallList[i].Init(_ShootPos);
            obj.SetActive(false);
        }
    }


    private void Update()
    {

        _ShootTime -= Time.smoothDeltaTime;
        
        if (_AutoShoot)
        {
            _Auto_NowAngle += Time.smoothDeltaTime * _Auto_AngleSign * _Auto_AngleRotateSpeedPerSecond * 180;
            _ShootAngleLine.transform.localEulerAngles = new Vector3(0, 0, _Auto_NowAngle);
            if (_Auto_NowAngle>=180- _MinArrowAngle)
            {
                _Auto_NowAngle = 180 - _MinArrowAngle;
                _Auto_AngleSign = -1;
            }
            else if(_Auto_NowAngle<= _MinArrowAngle)
            {
                _Auto_NowAngle = _MinArrowAngle;
                _Auto_AngleSign = 1;
            }
            Shoot(_Auto_NowAngle);
        }
        
    }

    public void Action_Shoot(Vector2 touchdownpos)
    {
        float angle,a1,a2;
        touchdownpos /= 100.0f;
        angle = Mathf.Atan2(touchdownpos.y - _ShootPos.y, touchdownpos.x - _ShootPos.x) * Mathf.Rad2Deg;
        a1 = angle + 30;
        a2 = angle - 30;

        if ((angle < _MinArrowAngle && angle >= 0) || (angle < 0 && angle > -90))
            angle = _MinArrowAngle;
        else if (angle > 180 - _MinArrowAngle || (angle < 0 && angle <= -90))
            angle = 180 - _MinArrowAngle;

        if ((a1 < _MinArrowAngle && a1 >= 0) || (a1 < 0 && a1 > -90))
            a1 = _MinArrowAngle;
        else if (a1 > 180 - _MinArrowAngle || (a1 < 0 && a1 <= -90))
            a1 = 180 - _MinArrowAngle;

        if ((a2 < _MinArrowAngle && a2 >= 0) || (a2 < 0 && a2 > -90))
            a2 = _MinArrowAngle;
        else if (a2 > 180 - _MinArrowAngle || (a2 < 0 && a2 <= -90))
            a2 = 180 - _MinArrowAngle;

        //Debug.Log("a1 : " + a1 + " a2 : " + a2 + "an : " + angle);
        Debug.Log(_ShootTime);
        if (_TrippleShot)
        {
            if(Shoot(a1))
            {
                _ShootTime = 0;
                Shoot(a2);
                _ShootTime = 0;
            }
        }
        Shoot(angle);
    }

    /// <summary>
    /// 입력받은 각도로 공 발사
    /// </summary>
    /// <param name="angle">Degree값을 입력받음. 오른쪽을 겨냥하면 0도, 수직을 겨냥하면 90도 </param>
    bool Shoot(float angle)
    {
        _ShootAngleLine.transform.localEulerAngles = new Vector3(0, 0, angle);
        if (_ShootTime <= 0.0f)
        {
            _ShootTime = _ShootDelay;

            _BallList[_NowUsingBall].transform.localPosition = _ShootPos;
            _BallList[_NowUsingBall].gameObject.SetActive(true);
            _BallList[_NowUsingBall].SetVelocity(angle);
            _NowUsingBall++;
            if (_NowUsingBall >= _MaxBallCount)
                _NowUsingBall = 0;
            return true;
        }
        return false;
    }

    public Vector2 GetMainBallPos() { return _BallList[0].GetPos(); }//TODO : 삭제하거나 수정해야됨
    [SerializeField] Transform ShootPosObj;
    public Vector3 _ShootPos
    {
        get
        {
            if (ShootPosObj != null)
                return ShootPosObj.localPosition;
            return Vector3.zero;
        }
    }
    public bool AutoShoot
    {
        set { _AutoShoot = value; }
        get { return _AutoShoot; }
    }
    public float ShootDelay
    {
        set { _ShootDelay = value; }
        get { return _ShootDelay; }
    }
    public int BallDamage
    {
        set { _BallDamage = value; }
        get { return _BallDamage; }
    }
    public bool TrippleShot
    {
        set { _TrippleShot = value; }
        get { return _TrippleShot; }
    }
}
