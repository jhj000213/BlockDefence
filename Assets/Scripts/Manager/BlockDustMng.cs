using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//TODO : 연출만 됨. 어떻게 연출할지 등등 수정해야됨
public class BlockDustMng : MonoBehaviour
{
    private static BlockDustMng _instance;
    public static BlockDustMng Data
    {
        get
        {
            if (!_instance)
            {
                _instance = FindObjectOfType(typeof(BlockDustMng)) as BlockDustMng;
                if (_instance == null)
                    Debug.Log("no Singleton obj");
            }
            return _instance;
        }
    }


    List<BlockDust> _BlockDustList = new List<BlockDust>();
    [SerializeField] GameObject _BlockDust;
    [SerializeField] Transform _BlockDustParent;
    int _UsingDustNum;
    readonly int _MAXDUSTNUM = 300;

    [SerializeField] Text _DustCountText;
    float _DustCountTextTargetScale;
    int _DustCount;
    int _TargetDustCount;
    float _CountUpTimer;
    readonly float _CountUpDelay = 0.02f;
    

    private void Start()
    {
        _DustCount = 0;
        _TargetDustCount = 0;
        _CountUpTimer = 0;

        _UsingDustNum = 0;
        for (int i = 0; i < _MAXDUSTNUM; i++)
        {
            GameObject obj = Instantiate(_BlockDust, _BlockDustParent);
            _BlockDustList.Add(obj.GetComponent<BlockDust>());
            _BlockDustList[i].Init();
        }
    }

    private void Update()
    {
        if (_DustCount < _TargetDustCount)
        {
            _CountUpTimer += Time.smoothDeltaTime;
            if (_CountUpTimer >= _CountUpDelay)
            {
                _CountUpTimer -= _CountUpDelay;
                _DustCount += (_TargetDustCount - _DustCount) / 70 > 0 ? (_TargetDustCount - _DustCount) / 70 : 1;
            }
            _DustCountTextTargetScale = 1.4f;

            if (_DustCount >= _TargetDustCount)
                _DustCount = _TargetDustCount;
        }
        else
            _DustCountTextTargetScale = 1;
        
        _DustCountText.text = _DustCount.ToString();
        _DustCountText.transform.localScale = Vector3.MoveTowards(
            _DustCountText.transform.localScale,
            new Vector3(_DustCountTextTargetScale, _DustCountTextTargetScale, 1),
            Time.smoothDeltaTime * 5);
        
    }

    public void MakeDust(Vector2 pos, int num)
    {
        _TargetDustCount += num;
        for (int i = 0; i < num; i++)
        {
            _BlockDustList[_UsingDustNum].gameObject.SetActive(true);
            _BlockDustList[_UsingDustNum].Use(pos);
            _UsingDustNum++;
            if (_UsingDustNum >= _MAXDUSTNUM)
                _UsingDustNum = 0;
        }
    }


    [SerializeField] Transform DustTargetPos;
    public Vector2 _DustTargetPos
    {
        get { return DustTargetPos.transform.localPosition; }
    }
}
