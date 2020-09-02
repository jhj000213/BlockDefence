using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//공, 블럭 등 화면에 있는 객체들의 움직임등을 관리하기 위한 상위 클래스
public class Object : MonoBehaviour
{
    protected Vector3 _OriginPos;

    [SerializeField] protected bool _Lerping;
    [SerializeField] protected Vector3 _LerpTargetPos;


    virtual public void Init(Vector3 pos)
    {
        _OriginPos = pos;
        transform.localPosition = pos;
    }

    protected void Update()
    {
        if (_Lerping)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, _LerpTargetPos, 0.3f);
            if (Vector3.Distance(transform.localPosition, _LerpTargetPos) < 0.1f)
            {
                _Lerping = false;
                transform.localPosition = _LerpTargetPos;
            }
        }
    }


    public void SetLerp(Vector3 pos)
    {
        _LerpTargetPos = pos;
        _Lerping = true;
    }

    public void SetLerpZero() { _Lerping = false;_LerpTargetPos = Vector2.zero; }
    public void SetPos(Vector2 pos) { transform.localPosition = pos; }
    public void SetPos(float x,float y) { transform.localPosition = new Vector3(x,y); }
    public Vector2 GetPos() { return transform.localPosition; }
}
