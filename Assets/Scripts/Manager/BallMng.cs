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
    Vector2 _BallInitPos = new Vector2(5.4f, 1.6f);
    readonly int _MaxBallCount = 100;
    int _NowUsingBall = 0;


    //기타
    int _BallDmg;

    //TODO : 이후 게임 시작 함수를 제작하여 씬에 들어왔을 경우 초기화 되도록 바꿔야함
    private void Start()
    {
        _NowUsingBall = 0;
        _BallDmg = 1;//임시 데미지

        for (int i = 0; i < _MaxBallCount; i++)
        {
            GameObject obj = Instantiate(_Ball, _BallParent);
            _BallList.Add(obj.GetComponent<Ball>());
            _BallList[i].Init(_BallInitPos);
            obj.SetActive(false);
        }
    }


    private void Update()
    {

    }

    /// <summary>
    /// 입력받은 각도로 공 발사
    /// </summary>
    /// <param name="angle">Degree값을 입력받음. 오른쪽을 겨냥하면 0도, 수직을 겨냥하면 90도 </param>
    public void Shoot(float angle)
    {
        for (int i = 0; i < _BallList.Count; i++)
            _BallList[i].gameObject.SetActive(false);


        _BallList[_NowUsingBall].transform.localPosition = _BallInitPos;
        _BallList[_NowUsingBall].gameObject.SetActive(true);
        _BallList[_NowUsingBall].SetVelocity(angle);
        _NowUsingBall++;
        if (_NowUsingBall >= _MaxBallCount)
            _NowUsingBall = 0;
    }


    public int GetBallDmg() { return _BallDmg; }
    public Vector2 GetMainBallPos() { return _BallList[0].GetPos(); }//TODO : 삭제하거나 수정해야됨
    
}
