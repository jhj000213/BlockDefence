using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockDust : MonoBehaviour
{
    Vector2 _TargetPos;
    bool _Lerping_1 = false;
    bool _Lerping_2 = false;
    readonly float _DustRange = 1.5f;
    readonly float _DustScale = 0.4f;
    float _LerpPlus;


    public void Init()
    {
        transform.localPosition = Vector3.zero;
        gameObject.SetActive(false);
    }

    public void Use(Vector2 pos)
    {
        _LerpPlus = 0;
        _Lerping_1 = true;
        transform.localPosition = pos;
        transform.localEulerAngles = new Vector3(0, 0, Random.Range(0, 360));
        float tempscale = Random.Range(_DustScale * 0.75f, _DustScale * 1.4f);
        transform.localScale = new Vector3(tempscale, tempscale, 1);
        _TargetPos = pos + new Vector2(Random.Range(-_DustRange, _DustRange), Random.Range(-_DustRange, _DustRange));
    }

    private void Update()
    {
        if(_Lerping_1)
        {
            transform.localPosition = Vector2.Lerp(transform.localPosition, _TargetPos, 0.06f);
            if(Vector2.Distance(transform.localPosition,_TargetPos)<0.1f)
            {
                transform.localPosition = _TargetPos;
                _Lerping_2 = true;
                _Lerping_1 = false;
            }
        }
        else if(_Lerping_2)
        {
            _LerpPlus += Time.smoothDeltaTime;

            //TODO : 어떤 위치로 이동시켜줄지 바꿔야함
            Vector2 ballPos = BallMng.Data.GetMainBallPos();

            transform.localPosition = Vector2.Lerp(transform.localPosition, ballPos, 0.15f+ _LerpPlus);
            if (Vector2.Distance(transform.localPosition, ballPos) < 0.1f)
            {
                transform.localPosition = ballPos;
                _Lerping_2 = false;
                gameObject.SetActive(false);
            }
        }
    }
}
