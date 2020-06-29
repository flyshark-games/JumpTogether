using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AdaptorManager))]
public class AdaptorManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        
        EditorGUILayout.BeginVertical();

        EditorGUILayout.LabelField("以下内容不支持更改,仅支持更新显示：");
        EditorGUILayout.LabelField("默认100px = 1unit");
        EditorGUILayout.LabelField("如果摄像机支持的视野高度和屏幕高度对应的unit一致，说明该游戏在此屏幕设备上运行像素不会存在放大缩小问题");

        EditorGUILayout.Space();


        EditorGUILayout.EndVertical();


        base.OnInspectorGUI();

    }
}
