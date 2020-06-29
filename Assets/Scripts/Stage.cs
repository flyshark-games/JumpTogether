using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    public float Life_time = 10f;
    void Awake()
    {

    }
    //物体是active状态，就调用这个函数
    void OnEnable()
    {
        StartCoroutine(GCStage(Life_time));
    }
    private IEnumerator GCStage(float life_time)
    {
        yield return new WaitForSeconds(0f);
        ObjectPool.Instance.PutObject(this.gameObject, life_time);
    }
    //
}
