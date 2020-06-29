using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[ExecuteInEditMode]
//分辨率适配
public class AdaptorManager : MonoBehaviour
{

    [SerializeField]
    private float orthograhpicSize;

    [SerializeField]
    [Rename("屏幕高度(单位是像素)")]
    private float screenHeight;

    [SerializeField]
    [Rename("屏幕宽度(单位是像素)")]
    private float screenWidth;

    [SerializeField]
    private float aspectRatio;

    [SerializeField]
    [Rename("摄像机视野高度(单位是unit)")]
    private float cameraHeight;

    [SerializeField]
    [Rename("摄像机视野宽度(单位是unit)")]
    private float cameraWidth;

    //写在这里，是因为，更改Camera组件中的size，该函数会自动调用更新数值
    void OnGUI()
    {
        
        var camera = GetComponent<Camera>();
        orthograhpicSize = camera.orthographicSize;

        screenHeight = Screen.height;
        screenWidth = Screen.width;

        aspectRatio = Screen.width * 1.0f / Screen.height;

        cameraHeight = orthograhpicSize * 2;
        cameraWidth = cameraHeight * aspectRatio;

    }
}


