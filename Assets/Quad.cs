using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quad : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var meshFilter = gameObject.GetComponent < MeshFilter>();

        for (int i = 0; i < meshFilter.mesh.vertices.Length;i++)
        {
            Debug.Log(meshFilter.mesh.vertices[i]);
        }
        
        
        for(int i=0;i<meshFilter.mesh.triangles.Length;i++)
        {
            Debug.Log(meshFilter.mesh.triangles[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
