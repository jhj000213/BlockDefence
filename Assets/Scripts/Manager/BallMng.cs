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


    readonly int _MaxBallCount = 100;
    int _NowUsingBall = 0;
    readonly float _MinArrowAngle = 10.0f;

    //Auto
    public float _Auto_NowAngle;
    public int _Auto_AngleSign = 1;
    public float _Auto_AngleRotateSpeedPerSecond = 0.66f;


    //TODO : 이후 게임 시작 함수를 제작하여 씬에 들어왔을 경우 초기화 되도록 바꿔야함
    private void Start()
    {
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
            if(_Auto_NowAngle>=180- _MinArrowAngle)
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
        float angle;
        touchdownpos /= 100.0f;
        angle = Mathf.Atan2(touchdownpos.y - _ShootPos.y, touchdownpos.x - _ShootPos.x) * Mathf.Rad2Deg;
        if ((angle < _MinArrowAngle && angle >= 0) || (angle < 0 && angle > -90))
            angle = _MinArrowAngle;
        else if (angle > 180 - _MinArrowAngle || (angle < 0 && angle <= -90))
            angle = 180 - _MinArrowAngle;

        Shoot(angle);
    }

    /// <summary>
    /// 입력받은 각도로 공 발사
    /// </summary>
    /// <param name="angle">Degree값을 입력받음. 오른쪽을 겨냥하면 0도, 수직을 겨냥하면 90도 </param>
    void Shoot(float angle)
    {
        if (_ShootTime <= 0.0f)
        {
            _ShootAngleLine.transform.localEulerAngles = new Vector3(0, 0, angle);
            _ShootTime = _ShootDelay;

            _BallList[_NowUsingBall].transform.localPosition = _ShootPos;
            _BallList[_NowUsingBall].gameObject.SetActive(true);
            _BallList[_NowUsingBall].SetVelocity(angle);
            _NowUsingBall++;
            if (_NowUsingBall >= _MaxBallCount)
                _NowUsingBall = 0;
        }
        
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
}
