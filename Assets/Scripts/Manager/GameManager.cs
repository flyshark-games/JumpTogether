using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//游戏管理器
public class GameManager : MonoBehaviour
{
    public GameObject Stage;
    public TextMeshProUGUI ScoreText;

    [SerializeField]
    private Camera cameraBackground;

    public int Score = 0;

    //单例模式
    private static GameManager s_instance;
    public static GameManager Instance
    {
        get { return GameManager.s_instance; }
        set { GameManager.s_instance = value; }
    }
    void Awake()
    {
        Instance = this;
        

    }
    void Start()
    {
        Debug.Log("Start");
        //预热对象池
        ObjectPool.Instance.Preload(Stage, 10);
    }
    void Update()
    {
        
    }
    public void ShowScore()
    {
        Score++;
        ScoreText.text = Score.ToString();
        if (Score > 0 && Score % 5 == 0)
        {
            cameraBackground.backgroundColor = new Color(Random.Range(0.1f, 1), Random.Range(0.1f, 1), Random.Range(0.1f, 1),1.0f);
            
        }
        
    }
    

    
}
