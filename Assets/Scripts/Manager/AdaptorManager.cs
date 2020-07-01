using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//分辨率适配
[ExecuteInEditMode]
public class AdaptorManager : MonoBehaviour
{
    GameSettings gameSettings;
    //[SerializeField]
    public float orthographicSize;

    void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start");
        gameSettings = AssetDatabase.LoadAssetAtPath<GameSettings>("Assets/Resources/GameSettings.asset");
        if (!gameSettings)
        {
            Debug.Log("寻找asset文件失败！");
        }
        Debug.Log(gameSettings.orthograhpicSize);
        orthographicSize = gameSettings.orthograhpicSize;
    }

    void Update()
    {
        
    }

    void OnGUI()
    {
        //EditorGUILayout.LabelField("用于显示asset的相关参数，不支持修改,更改asset文件参数，该面板也更新");
        orthographicSize = gameSettings.orthograhpicSize;
    }
}


