using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameSettings))]
public class GameSettingsEditor : Editor
{

    SerializedProperty m_orthographicsSize;
    SerializedProperty m_screenHeight;
    SerializedProperty m_screenWidth;
    SerializedProperty m_aspectRatio;
    SerializedProperty m_cameraHeight;
    SerializedProperty m_cameraWidth;
    SerializedProperty m_pxPerUnit;


    void OnEnable()
    {
        m_orthographicsSize = serializedObject.FindProperty("orthograhpicSize");
        m_screenHeight = serializedObject.FindProperty("screenHeight");
        m_screenWidth = serializedObject.FindProperty("screenWidth");
        m_aspectRatio = serializedObject.FindProperty("aspectRatio");
        m_cameraHeight = serializedObject.FindProperty("cameraHeight");
        m_cameraWidth = serializedObject.FindProperty("cameraWidth");
        m_pxPerUnit = serializedObject.FindProperty("pxPerUnit");

    }

    override public void OnInspectorGUI()
    {
        
        GUILayout.Button("屏幕分辨率和摄像机参数配置");
        

        EditorGUILayout.BeginVertical();
        EditorGUILayout.LabelField("以下内容支持更改,请尽情调试，默认100px = 1unit");
        EditorGUILayout.LabelField("如果摄像机支持的视野高度和屏幕高度对应的unit一致，说明该游戏在此屏幕设备上运行像素不会存在放大缩小问题");
        EditorGUILayout.Space();
        EditorGUILayout.EndVertical();

        //m_cameraHeight.floatValue = m_orthographicsSizeProperty.floatValue * 2;
        //检查用户是否更改了数值
        EditorGUI.BeginChangeCheck();
        serializedObject.Update();
        //base.OnInspectorGUI();
        EditorGUILayout.PropertyField(m_orthographicsSize);
        EditorGUILayout.PropertyField(m_screenHeight);
        EditorGUILayout.PropertyField(m_screenWidth);
        m_aspectRatio.floatValue = m_screenWidth.floatValue / m_screenHeight.floatValue;
        m_cameraHeight.floatValue = m_orthographicsSize.floatValue * 2;
        m_cameraWidth.floatValue = m_cameraHeight.floatValue * m_aspectRatio.floatValue;
        m_pxPerUnit.floatValue = m_screenHeight.floatValue / m_cameraHeight.floatValue;
        EditorGUILayout.PropertyField(m_aspectRatio);
        EditorGUILayout.PropertyField(m_cameraHeight);
        EditorGUILayout.PropertyField(m_cameraWidth);
        EditorGUILayout.PropertyField(m_pxPerUnit);

        serializedObject.ApplyModifiedProperties();
        //如果用户结束了更改,更新asset文件
        if(EditorGUI.EndChangeCheck())
        {
            //Debug.Log("结束了属性更改,写入硬盘");
            //写入硬盘，也就是永久写入
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            //AssetDatabase.StartAssetEditing();
        }

        EditorGUILayout.BeginHorizontal(); //加这两个可以使按钮居中
        GUILayout.FlexibleSpace();
        // 打印数据
        if (GUILayout.Button("打印数据"))
        {
            Debug.Log("orthograhpicSize :" + m_orthographicsSize.floatValue);
            
        }
        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();
            
        
        //if (GUI.changed)
        //{
        //    EditorUtility.SetDirty(target);
        //}
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        GUILayout.Button("其他参数");
    }
}
