using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AdaptorManager))]
[ExecuteInEditMode]
public class AdaptorManagerEditor : Editor
{
    void OnEnable()
    {

    }
    public override void OnInspectorGUI()
    {
        EditorGUILayout.LabelField("用于显示asset的相关参数，不支持修改,更改asset文件参数，该面板也随之更新");
        base.OnInspectorGUI();
    }
}
