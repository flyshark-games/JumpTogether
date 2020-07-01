using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[SerializeField]
public class GameSettings : ScriptableObject
{
    public float orthograhpicSize;


    [Rename("屏幕高度(单位是像素)")]
    public float screenHeight = Screen.height;


    [Rename("屏幕宽度(单位是像素)")]
    public float screenWidth = Screen.width;


    public float aspectRatio;


    [Rename("摄像机视野高度(单位是unit)")]
    public float cameraHeight;


    [Rename("摄像机视野宽度(单位是unit)")]
    public float cameraWidth;

    [Rename("pixels per unit:")]
    public float pxPerUnit;
}
