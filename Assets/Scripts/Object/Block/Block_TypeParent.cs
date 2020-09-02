using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Block_TypeParent : MonoBehaviour
{
    protected int _BlockType;
    [SerializeField] protected int _HP;

    [SerializeField] protected ObjectShake _Shaker;
    [SerializeField] protected SpriteRenderer _HitEffect;
    [SerializeField] protected TextMesh _HPMesh;

    /// <summary>
    /// 블럭이 공통적으로 가지고 있어야 될 값, 자식 객체들을 전달받음
    /// </summary>
    public void Init_Common(int hp,ObjectShake shaker, TextMesh hpmesh, SpriteRenderer hite)
    {
        _HPMesh = hpmesh;
        _HPMesh.gameObject.SetActive(false);
        _HP = hp;
        _HPMesh.text = _HP.ToString();

        _HitEffect = hite;
        _Shaker = shaker;
        _Shaker.Init(0.1f, 0.02f, 0.02f, 0.4f);

    }

    /// <summary>
    /// 각각의 블록이 가지는 고유한 특성에 맞게 초기화
    /// </summary>
    virtual public void Init_Independent() { }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Hit();
        Hit(collision);
    }

    virtual protected void Hit()
    {
        _HP -= BallMng.Data.BallDamage;
        if (_HP <= 0)
            Crash();
    }
    virtual protected void Hit(Collision2D collision)
    {

    }

    virtual protected void Crash()
    {
        gameObject.SetActive(false);
    }

    virtual public void SetDmg(int dmg)
    {
        _HP -= dmg;
        if (_HP <= 0)
            Crash();
    }
    virtual public void SetHP(int h)
    {
        _HP = h;
    }
    public int GetHP() { return _HP; }
}
