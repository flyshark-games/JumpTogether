using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class CreateAssetFile : Editor
{
    [MenuItem("策划/配置文件")]
    static void CreateAsset()
    {
        //实例化类
        GameSettings gameSettings = ScriptableObject.CreateInstance<GameSettings>();
        if(!gameSettings)
        {
            Debug.Log("创建配置文件失败!");
            return;
        }

        //adaptorManager.cameraHeight = adaptorManager.orthograhpicSize * 2;
        //adaptorManager.cameraWidth = adaptorManager.cameraHeight * adaptorManager.aspectRatio;
        //把资源存放在Resources文件夹下
        string assetFilePath = Application.dataPath + "/Resources" + "/";

        //如果项目中不含有该路径，创建爱你一个
        if(!Directory.Exists(assetFilePath))
        {
            Directory.CreateDirectory(assetFilePath);
        }

        //将类名AdaptorManager转化为字符串，拼接在资源路径下
        assetFilePath = string.Format("Assets/Resources" + "/{0}.asset", (typeof(GameSettings).ToString()));

        //生成自定义资源到指定路径下
        AssetDatabase.CreateAsset(gameSettings, assetFilePath);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

    }
}
