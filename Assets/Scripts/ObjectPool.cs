using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//对象池
public class ObjectPool : MonoBehaviour
{
#region component

    //单例模式
    private static ObjectPool s_instance;
    public static ObjectPool Instance
    {
        get { return ObjectPool.s_instance; }
        set { ObjectPool.s_instance = value; }
    }
#endregion

#region property
    [SerializeField]
    [Rename("PoolName")]
    private string Pool_name = "Stage";

    [SerializeField]
    [Rename("对象池的大小")]
    private int Pool_capacity;

    private static Dictionary <string, Queue<GameObject>> s_pool;

    private int _max_count = int.MaxValue;
    public int Max_count
    {
        get { return _max_count; }
        set
        {
            Max_count = Mathf.Clamp(value, 0, int.MaxValue);
        }
    }
    #endregion

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void OnBeforeSceneLoadRuntimeMethod()
    {
        Debug.Log("OnBeforeSceneLoadRuntimeMethod");
        

    }

    void Awake()
    {
        Instance = this;
        //该对象池可以是多种物体的对象池
        s_pool = new Dictionary<string, Queue<GameObject>> { };
    }
    void Start()
    {
        
    }

    //取物体
    public GameObject GetObject(GameObject go,Vector3 position,Quaternion rotation)
    {
        
        //如果未初始化过，初始化对象池
        if(!s_pool.ContainsKey(go.name))
        {
            s_pool.Add(go.name, new Queue<GameObject>());
        }
        //如果池空了就创建新物体
        if(s_pool[go.name].Count == 0)
        {
            GameObject newObject = Instantiate(go, position, rotation);
            newObject.name = go.name;

            return newObject;
        }
        //从对象池中获取物体
        GameObject nextObject = s_pool[go.name].Dequeue();
        nextObject.SetActive(true); //要先启动再设置属性，否则可能会被OnEnable重置
        nextObject.transform.position = position;
        nextObject.transform.rotation = rotation;
        Debug.Log("取走物体");
        return nextObject;
        
    }
    //把物体放回池里
    public void PutObject(GameObject go,float time)
    {
        
        if (s_pool[go.name].Count >= Max_count)
        {
            Destroy(go, time);
        }
        else
        {
            StartCoroutine(ExecutePut(go, time));
        }
    }
    private IEnumerator ExecutePut(GameObject go,float time)
    {
        
        yield return new WaitForSeconds(time);
        go.SetActive(false);
        s_pool[go.name].Enqueue(go);
        Debug.Log("放回物体");
    }

    //物体预加载
    public void Preload(GameObject go,int number)
    {
        if(!s_pool.ContainsKey(go.name))
        {
            s_pool.Add(go.name, new Queue<GameObject>());
        }
        for(int i = 0; i < number; i++)
        {
            GameObject newObject = Instantiate(go);
            newObject.name = go.name;
            newObject.SetActive(false);
            s_pool[go.name].Enqueue(newObject);

            //Debug.Log(go.name);
        }
    }
    void Update()
    {
        //Pool_capacity = s_pool["Stage"].Count;
    }

    void OnDestroy()
    {
        s_pool.Clear();
    }

}
