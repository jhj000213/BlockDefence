using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO : 카메라 매니저라 쓰고 슬로무 모션 매니저라 읽음. 해상도 대응 코드 제외하고는 모두 슬로우 관련 코드라 따로 클래스 생성 필요
public class CameraMng : MonoBehaviour
{
    private static CameraMng _instance;
    public static CameraMng Data
    {
        get
        {
            if (!_instance)
            {
                _instance = FindObjectOfType(typeof(CameraMng)) as CameraMng;
                if (_instance == null)
                    Debug.Log("no Singleton obj");
            }
            return _instance;
        }
    }

    [SerializeField] GameObject _MainCamera;
    [SerializeField] ObjectShake _Shaker;
    Camera _Camera;

    public bool _TimeSlowOn;
    readonly float _SlowRate = 0.3f;
    readonly float _SlowReturnSpeed = 1.0f;

    void Start()
    {
        _Camera = GetComponent<Camera>();
        _Shaker.Init(0.6f, 0.05f, 0.05f, 0.4f);

#if UNITY_STANDALONE
        Screen.SetResolution(360, 640, false);
#endif

        _Camera.orthographicSize = (Screen.height / 200.0f) / (Screen.width/1080.0f) ;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            CameraAction();
        Time.timeScale = Mathf.MoveTowards(Time.timeScale, 1, _SlowReturnSpeed*Time.smoothDeltaTime);
    }

    public void CameraAction()
    {
        _Shaker.Shake();
        if (_TimeSlowOn)
            Time.timeScale = _SlowRate;
    }
}
