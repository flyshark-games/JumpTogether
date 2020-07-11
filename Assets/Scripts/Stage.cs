using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    public float Life_time = 10f;

    //[SerializeField]
    [Rename("该盒子是否已经被团子访问过")]
    public bool _visited = false;

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
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer.Equals(LayerMask.NameToLayer("Player")))
            _visited = true;
    }
}
