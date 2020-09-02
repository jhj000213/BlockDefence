using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block_Normal : Block_TypeParent
{
    protected float _HitEffectAlpha;
    protected float _HitEffectScale;

    override public void Init_Independent()
    {
        _HPMesh.gameObject.SetActive(true);
    }

    private void Update()
    {
        if (_HitEffectAlpha > 0)
        {
            _HitEffectAlpha -= Time.smoothDeltaTime * 1.75f;
            _HitEffect.color = new Color(1, 1, 1, _HitEffectAlpha);
        }
        _HitEffect.transform.localScale = Vector3.MoveTowards(
            _HitEffect.transform.localScale,
            new Vector3(_HitEffectScale, _HitEffectScale),
            Time.smoothDeltaTime * 3.5f);
    }

    protected void HitEffect()
    {
        _Shaker.Shake();
        _HitEffectAlpha = 0.5f;
        _HitEffectScale = 1.5f;
        _HitEffect.transform.localScale = new Vector3(1, 1, 1);
    }

    protected override void Hit()
    {
        base.Hit();
        _HPMesh.text = _HP.ToString();
        if (_HP > 0)
            HitEffect();
    }

    public override void SetDmg(int dmg)
    {
        base.SetDmg(dmg);
        _HPMesh.text = _HP.ToString();
        HitEffect();
    }

    protected override void Crash()
    {
        CameraMng.Data.CameraAction();
        BlockDustMng.Data.MakeDust(transform.localPosition, 15);
        base.Crash();
    }

    public override void SetHP(int h)
    {
        base.SetHP(h);
        _HPMesh.text = _HP.ToString();
    }
}