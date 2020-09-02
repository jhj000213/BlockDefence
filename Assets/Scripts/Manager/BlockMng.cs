using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// BLOCKADD : 이름 추가
//TODO : ENUM 등 데이터값을 저장하는 클래스 추강
public enum BLOCKTYPE
{
    NULL = 0,
    Normal = 1,

    Boss = 100
}

public class BlockMng : MonoBehaviour
{
    private static BlockMng instance;
    public static BlockMng Data
    {
        get
        {
            if (!instance)
            {
                instance = FindObjectOfType(typeof(BlockMng)) as BlockMng;
                if (instance == null)
                    Debug.Log("no Singleton obj");
            }
            return instance;
        }
    }
   
    [SerializeField] GameObject _Block;
    [SerializeField] Transform _BlockParent;
    readonly int _MAXBLOCKNUM = 42;
    List<Block> _BlockList = new List<Block>();

    //HACK : BallMng와는 다르게 상대방 객체의 갯수를 배열로 관리함.. 싱글 플레이를 설계할 떄 잘못 구상해서 꼬임. 언젠간 수정 요망
    int _UsingBlockNum;
    
    float _MakeBlockChance;
    readonly float _MakeBlockChanceMax = 4;
    int _NowTurnsBlockHp;

    /// <summary>
    /// 생성될 블럭 데이터가 저장되는 배열
    /// </summary>
    int[] _IsBlockArray = new int[6];

    //임시 보스 관련 객체
    [SerializeField] Block _Boss;
    int _BossHp;

    //TODO : 씬 전환되어 게임 시작할 떄 호출되는 함수로 빼야됨
    private void Awake()
    {
        _NowTurnsBlockHp = 1;
        _BossHp = 1000;

        _UsingBlockNum = 0;
        
        _MakeBlockChance = 2;

        for (int i = 0; i < _MAXBLOCKNUM; i++)
        {
            GameObject obj = Instantiate(_Block, _BlockParent);
            obj.transform.localPosition = Vector3.zero;
            obj.SetActive(false);
            _BlockList.Add(obj.GetComponent<Block>());
        }
    }

    public void MakeBlock()
    {
        int temphp = _NowTurnsBlockHp;
        float base_y = 9.6f;
        float rate = 1.1f;

        CreateNewBlockArray();

        for (int i = 0; i < 6; i++)
        {
            if(_IsBlockArray[i]!=0)
            {
                BLOCKTYPE nowtype = (BLOCKTYPE)_IsBlockArray[i];
                _BlockList[_UsingBlockNum].gameObject.SetActive(true);
                _BlockList[_UsingBlockNum].MakeInit();
                _BlockList[_UsingBlockNum].UseBlock(new Vector2(0.9f + i * 1.8f, base_y), temphp, rate, nowtype);
                _BlockList[_UsingBlockNum].SetChileInit();
                _UsingBlockNum++;
                if (_UsingBlockNum >= _MAXBLOCKNUM)
                    _UsingBlockNum = 0;
            }
        }
    }

    public void NextTurn()
    {
        _NowTurnsBlockHp += 1;
        _MakeBlockChance += 0.1f;
        if (_MakeBlockChance >= _MakeBlockChanceMax)
            _MakeBlockChance = _MakeBlockChanceMax;
    }

    void CreateNewBlockArray()
    {
        for (int i = 0; i < 6; i++)
            _IsBlockArray[i] = 0;

        RandomLinearArray<int> array = new RandomLinearArray<int>(6,0);

        int normalblockcount = Random.Range((int)(_MakeBlockChance - 1), (int)_MakeBlockChance);
        for (int i = 0; i < normalblockcount; i++)
            array.Insert((int)BLOCKTYPE.Normal);

        _IsBlockArray = array.GetArray();
    }


    public int GetBlockHP() { return _NowTurnsBlockHp; }
    public int[] GetBlockArray() { return _IsBlockArray; }
}
