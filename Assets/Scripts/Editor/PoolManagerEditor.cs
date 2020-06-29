using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//自定义ObjectPool的Inspector面板样式
[CustomEditor(typeof(ObjectPool))]
public class PoolManagerEditor : Editor
{
    SerializedProperty PoolCapacity;

    void OnEnable()
    {
        //获取当前编辑自定义Inspector对象
        //PoolCapacity = serializedObject.FindProperty("Pool_capacity");

    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();


        //设置界面以垂直方向布局
        EditorGUILayout.BeginVertical();

        //serializedObject.Update();

        //EditorGUILayout.PropertyField(PoolCapacity);

        //EditorGUILayout.TextField("PoolName","StagePool");
        //EditorGUILayout.TextField("PoolName",ObjectPool.Instance.Pool_name);

        //EditorGUILayout.IntField("MaxCount", ObjectPool.Instance.Max_count);
        //ObjectPool.Instance.Pool_name = EditorGUILayout.TextField("PoolName: ", ObjectPool.Instance.Pool_name);



        //serializedObject.ApplyModifiedProperties();
        EditorGUILayout.EndVertical();
    }
}
