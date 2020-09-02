using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;


public class Block : Object
{
    protected BLOCKTYPE _BlockType = BLOCKTYPE.NULL;
    [SerializeField] protected Block_TypeParent _TypePerent = null;

    //블럭 피격시 필요한 자식 객체들
    [SerializeField] protected TextMesh _HPMesh;
    [SerializeField] protected ObjectShake _Shaker;
    [SerializeField] protected SpriteRenderer _HitEffect;

    [SerializeField] SpriteRenderer _BlockBodySprite;
    [SerializeField] SpriteAtlas _BlockAtlas;

    //일정 턴 이상 생존하면 게임오버
    int _LiveRound;
    readonly int _MaxLiveRound = 6;

    //턴 마다 이동하는 위치값
    protected float _MoveDownPos;

    /// <summary>
    /// 최초 생성 시 한번만 호출
    /// </summary>
    public void MakeInit()
    {
        _LiveRound = 0;
    }

    public virtual void UseBlock(Vector2 pos,int hp,float movedownpos,BLOCKTYPE blocktype)
    {
        base.Init(pos);

        ChangeBlockType(blocktype,hp);
        _MoveDownPos = movedownpos;
    }

    public void ChangeBlockType(BLOCKTYPE type,int hp)
    {
        if (_BlockType != type)
        {
            DestroyImmediate(_TypePerent, true);//붙어있는 컴포넌트를 제거하기 위함

            _BlockType = type;
            _BlockBodySprite.sprite = _BlockAtlas.GetSprite(type.ToString());

            //BLOCKADD : case 추가
            switch (_BlockType)
            {
                case BLOCKTYPE.Normal:
                    _TypePerent = gameObject.AddComponent<Block_Normal>();
                    break;
            }
        }
        if(_TypePerent)
        {
            _TypePerent.Init_Common(hp, _Shaker, _HPMesh, _HitEffect);
            _TypePerent.Init_Independent();
        }
    }

    /// <summary>
    /// 턴이 끝날 때 블럭 이동
    /// </summary>
    public void MoveDown()
    {
        _Lerping = true;
        _LerpTargetPos = transform.localPosition + new Vector3(0, -_MoveDownPos);

        _LiveRound++;
    }

    /// <summary>
    /// 타격 이펙트, 흔들림 이펙트 초기화
    /// </summary>
    public void SetChileInit()
    {
        _HitEffect.color = new Color(1, 1, 1, 0);
        _HitEffect.transform.localScale = new Vector3(1, 1, 1);
        _Shaker.ObjectPosReset();
    }

    /// <summary>
    /// dmg만큼 체력에서 뺌
    /// </summary>
    public void SetDmg(int dmg)
    {
        _TypePerent.SetDmg(dmg);
    }

    /// <summary>
    /// 현재 체력을 hp값으로 바꿈
    /// </summary>
    public void SetHP(int hp)
    {
        _TypePerent.SetHP(hp);
    }
    public int GetHP() { return _TypePerent.GetHP(); }
    public int GetBlockType() { return (int)_BlockType; }
}
