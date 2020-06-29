using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//没有用unity自带的sprite插件，相当于自己写了一个sprite性质的gameObject做ground
//[ExecuteInEditMode]
public class Ground : MonoBehaviour
{
#region propertity
    public float Camera_size = 1f; //相机的size

    public float Screen_height = 768; //标准屏幕的高度

    private float Gl_to_pixel_ration; //屏幕unit和屏幕像素的比值

    public Material spriteMaterial;

    private int _vertice_count = 4;
#endregion

    void Start()
    {
        //屏幕unit和屏幕像素的比值
        Gl_to_pixel_ration = Camera_size * 2.0f / Screen_height;
        InitSprite();

        //transform.Rotate(90, 0, 0, Space.Self);
    }
    void Update()
    {

    }
    public void InitSprite()
    {
        //获取图片的像素比宽高
        int pixel_height = spriteMaterial.mainTexture.height;
        int pixel_width = spriteMaterial.mainTexture.width;

        Debug.Log(pixel_width + " " + pixel_height);

        //得到MeshFilter对象
        MeshFilter meshFilter = gameObject.GetComponent<MeshFilter>();
        if(!meshFilter)
        {
            meshFilter = gameObject.AddComponent<MeshFilter>();
            MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
            meshRenderer.sharedMaterial = spriteMaterial;

            //meshRenderer.material = spriteMaterial;
            meshRenderer.sharedMaterial.color = Color.white;

            //meshRenderer.sharedMaterials[0] = spriteMaterial;

        }
        //得到对应的网格对象
        //Mesh mesh = meshFilter.sharedMesh;
        Mesh mesh = meshFilter.mesh;
        //网格的顶点做标数组
        Vector3[] vertices = new Vector3[_vertice_count];
        //得到三角形的数量
        int _triangle_count = _vertice_count - 2;
        //三角形顶点数组
        int[] triangles = new int[_vertice_count * 3];
        
        //如果用自带的Sprite插件，一个pixels per unit参数就搞定了
        
        float glHeight = pixel_height * Gl_to_pixel_ration; //得到的是图片高度占据的unit大小
        float glWidth = pixel_width * Gl_to_pixel_ration; //得到的是图片宽度占据的unit的大小


        Debug.Log(glWidth + " " + glHeight);


        vertices[0] = new Vector3(0,0,0);
        vertices[1] = new Vector3(glWidth,0, 0);
        vertices[2] = new Vector3(0,glHeight,0);
        vertices[3] = new Vector3(glWidth,glHeight,0);

        mesh.vertices = vertices;

        //绑定顶点顺序--顺时针
        triangles[0] = 0;
        triangles[1] = 3;
        triangles[2] = 1;
        triangles[3] = 3;
        triangles[4] = 0;
        triangles[5] = 2;

        //triangles[0] = 0;
        //triangles[1] = 2;
        //triangles[2] = 1;
        //triangles[3] = 2;
        //triangles[4] = 3;
        //triangles[5] = 1;

        mesh.triangles = triangles;
        //mesh.uv = new Vector2[] { new Vector2(0, 0), new Vector2(0, 1), new Vector2(1, 0), new Vector2(1, 1) };

        
    }
}
