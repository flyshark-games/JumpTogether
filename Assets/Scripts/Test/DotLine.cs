using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class DotLine : MonoBehaviour
{
    LineRenderer line;
    List<Vector3> listPath = new List<Vector3>();
    // Use this for initialization
    void Start()
    {
        line = GetComponent<LineRenderer>();
        //listPath = iTween.GetCrvePaths(iTweenPath.GetPath("1"));
        listPath.Add(new Vector3(0, 0, 0));
        //listPath.Add(new Vector3(1, 0, 0));
        listPath.Add(new Vector3(1, 0, 0));

        line.positionCount = listPath.Count;
        line.startWidth = 0.1f;
        line.endWidth = 0.1f;
        
        line.receiveShadows = false;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < listPath.Count; i++)
        {
            line.SetPosition(i, listPath[i]);
        }
    }

}
