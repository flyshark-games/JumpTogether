using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//游戏管理器
public class GameManager : MonoBehaviour
{
    public GameObject Stage;
    public TextMeshProUGUI ScoreText;

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

    public void ShowScore()
    {
        Score++;
        ScoreText.text = Score.ToString();
    }
    
}
