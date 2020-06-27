using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEditor;
using TMPro;


public class Player : MonoBehaviour
{

#region component
    private Rigidbody m_rigidBody;

    //当前盒子
    private GameObject m_currentStage;

    private Collider m_lastCollisionCollider;

    public GameObject Stage;

    public Transform Camera;

    public Transform Head;

    public Transform Body;

    public TextMeshProUGUI ScoreText;

    public GameObject Particle;

#endregion

#region propertity
    private float _state_time;

    public float Factor = 1;

    public float Max_distance = 4;

    //相机的相对位置
    public Vector3 Camera_relative_position;

    private Vector3 _direction = new Vector3(1,0,0);

    private int _score;
#endregion


    void Start()
    {
        m_rigidBody = GetComponent<Rigidbody>();

        //修改物理组件的重心到body底部
        m_rigidBody.centerOfMass = Vector3.zero;
        
        //找到游戏最开始的当前盒子
        //Stage = GameObject.Find("Stage");
        m_currentStage = Stage;
        m_lastCollisionCollider = m_currentStage.GetComponent<Collider>();

        SpawnStage();

        Camera_relative_position = Camera.position - transform.position;

        Particle = GameObject.Find("Particle System");
        Particle.SetActive(false);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            _state_time = Time.time;

            Particle.SetActive(true);
        }
        if(Input.GetKeyUp(KeyCode.Space))
        {
            var elapse = Time.time - _state_time;
            OnJump(elapse);
            Particle.SetActive(false);

            Body.transform.DOScale(0.1f, 1);
            Head.transform.DOLocalMoveY(0.29f, 0.5f);

            m_currentStage.transform.DOLocalMoveY(0.25f, 0.2f);
            m_currentStage.transform.DOScale(new Vector3(1, 0.5f, 1), 0.2f);
        }

        //按下空格键，小人身体进行缩放,盒子缩放--蓄力效果
        if(Input.GetKey(KeyCode.Space))
        {
            Body.transform.localScale += new Vector3(1, -1, 1) * 0.05f * Time.deltaTime;
            Head.transform.localPosition += new Vector3(0, -1, 0) * 0.1f * Time.deltaTime;

            //盒子沿着轴心缩放
            m_currentStage.transform.localScale += new Vector3(0, -1, 0) * 0.15f * Time.deltaTime;
            m_currentStage.transform.localPosition += new Vector3(0, -1, 0) * 0.15f * Time.deltaTime;

            //预测小人运动轨迹
            PredictTrajectory();
            //画虚线

            
        }
    }

    void OnJump(float elapse)
    {
        m_rigidBody.AddForce((new Vector3(0, 1, 0) + _direction) * elapse * Factor, ForceMode.Impulse);
    }

    void SpawnStage()
    {
        var stage = Instantiate(Stage);
        stage.transform.position = m_currentStage.transform.position + _direction * Random.Range(1.1f, Max_distance);

        var random_scale = Random.Range(0.5f, 1f);
        stage.transform.localScale = new Vector3(random_scale, 0.5f, random_scale);

        stage.GetComponent<Renderer>().material.color = new Color(Random.Range(0.01f, 1), Random.Range(0.01f, 1), Random.Range(0.01f, 1));

    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name.Contains("Stage") && collision.collider != m_lastCollisionCollider)
        {
            m_lastCollisionCollider = collision.collider;
            m_currentStage = collision.gameObject;

            RandomDirection();
            SpawnStage();

            MoveCamera();

            _score++;
            ScoreText.text = _score.ToString();
        }
        if(collision.gameObject.name == "Ground")
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
    void RandomDirection()
    {
        var seed = Random.Range(0, 2);
        if(seed == 0)
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
        Camera.DOMove(transform.position + Camera_relative_position, 1);
    }

    void PredictTrajectory()
    {

    }
}
