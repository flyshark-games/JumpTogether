using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEditor;
using TMPro;


public class Dumpling : MonoBehaviour
{

#region component
    private Rigidbody2D m_rigidBody;

    //当前盒子
    private GameObject m_currentStage;

    private Collider2D m_lastCollisionCollider;

    private LineRenderer m_dotLine;

    private Transform mainCamera;

    public GameObject Stage;

    public GameObject Particle;

    #endregion

    #region propertity
    private float _state_time;

    [SerializeField]
    [Rename("盒子塌缩速度")]
    private float _stageCollapseSpeed = 0.15f;

    [SerializeField]
    [Rename("按键时间")]
    private float _elapse;

    public float Factor = 1;
    public float Max_distance = 4;

    //相机的相对位置
    public Vector3 Camera_relative_position;

    private Vector3 _direction = Vector3.right;

    private int _score;

    //private Vector3 _currentStageScale = new

#endregion

    void Awake()
    {
        mainCamera = Camera.main.transform;
    }
    // Start is called before the first frame update
    void Start()
    {
        m_rigidBody = GetComponent<Rigidbody2D>();
        //修改物理组件的重心到body底部
        m_rigidBody.centerOfMass = new Vector3(0,-0.5f,0);

        //找到游戏最开始的当前盒子
        m_currentStage = Stage;
        m_lastCollisionCollider = m_currentStage.GetComponent<Collider2D>();

        SpawnStage();

        Camera_relative_position = mainCamera.position - transform.position;

        Particle.SetActive(false);

        m_dotLine = GetComponent<LineRenderer>();


    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            _state_time = Time.time;

            Particle.SetActive(true);
            

        }
        if(Input.GetKeyUp(KeyCode.Space))
        {
            _elapse = Time.time - _state_time;
            transform.DOScale(1f,1); //恢复原状
            OnJump(_elapse);
            Particle.SetActive(false);
        }
        if(Input.GetKey(KeyCode.Space))
        {
            transform.localScale += Vector3.down * _stageCollapseSpeed * Time.deltaTime;
            
            //预测小人运动轨迹
            PredictTrajectory();
            //画虚线
            //m_dotLine.SetPosition(0,m_currentStage.transform.position);
            
            
        }
        DropOut();
    }
    void DropOut()
    {
        if (gameObject.transform.position.y < -0.5f)
        {
            SceneManager.LoadScene("2D");
        }
    }

    void OnJump(float elapse)
    {
        m_rigidBody.AddForce((new Vector3(0, 1, 0) + _direction) * elapse * Factor, ForceMode2D.Impulse);
        //重置轨迹线的起始位置
        
    }

    void SpawnStage()
    {
        //取物体
        var stage = ObjectPool.Instance.GetObject(Stage,m_currentStage.transform.position + _direction * Random.Range(1.1f,Max_distance),Quaternion.identity);

    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer.Equals(LayerMask.NameToLayer("Stage")) && collision.collider != m_lastCollisionCollider)
        {
            
            m_lastCollisionCollider = collision.collider;
            m_currentStage = collision.gameObject;

            GameManager.Instance.ShowScore();

            //RandomDirection();
            SpawnStage();
            MoveCamera();
            Particle.transform.position = transform.position;

        }
    }
    void RandomDirection()
    {
        var seed = Random.Range(0, 2);
        if (seed == 0)
        {
            _direction = new Vector3(1, 0, 0);
        }
        else
        {
            _direction = new Vector3(0, 0, 1);
        }
    }
    void MoveCamera()
    {
        //相机1s从当前位置移动到新位置
        mainCamera.DOMove(transform.position + Camera_relative_position, 1);
    }
    void PredictTrajectory()
    {
        _elapse = Time.time - _state_time;
        m_dotLine.SetPosition(0, transform.position + new Vector3(_elapse, 0, 0));
    }
}
