using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectShake : MonoBehaviour
{
    [SerializeField] Transform _ShakeObject;
    bool _FirstInit = false;

    Vector3 _ShakeOriginPos;
    public bool _ShakeOn;
    bool _NowShaking;
    float _NowShakeAmount;
    float _ShakeAmountMax;
    float _ShakeAmountMin;
    float _ShakeAmount;
    float _ShakeTime;


    public void Init(float max,float min,float amount,float time)
    {
        if(!_FirstInit)
        {
            _ShakeOriginPos = _ShakeObject.localPosition;
            _FirstInit = true;
        }
        _ShakeObject.localPosition = _ShakeOriginPos;
        _NowShakeAmount = _ShakeAmountMin;
        _ShakeAmountMax = max;
        _ShakeAmountMin = min;
        _ShakeAmount = amount;
        _ShakeTime = time;
    }

    private void Update()
    {
        if (!_NowShaking)
        {
            _NowShakeAmount -= Time.smoothDeltaTime * 0.6f;
            if (_NowShakeAmount <= _ShakeAmountMin)
                _NowShakeAmount = _ShakeAmountMin;
        }
    }

    public void ObjectPosReset()
    {
        _ShakeObject.localPosition = _ShakeOriginPos;

    }

    public void Shake()
    {
        if (!_ShakeOn)
            return;

        _NowShaking = true;
        _NowShakeAmount += _ShakeAmount;
        if (_NowShakeAmount >= _ShakeAmountMax)
            _NowShakeAmount = _ShakeAmountMax;
        StartCoroutine(Shake(_NowShakeAmount, _ShakeTime));
    }
    

    IEnumerator Shake(float _amount, float _duration)
    {
        float timer = 0;
        while (timer <= _duration)
        {
            _ShakeObject.localPosition = (Vector3)Random.insideUnitCircle * _amount + _ShakeOriginPos;

            timer += Time.deltaTime;
            yield return null;
        }
        _ShakeObject.localPosition = _ShakeOriginPos;
        _NowShaking = false;
    }
}
