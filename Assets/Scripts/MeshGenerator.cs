using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshGenerator : MonoBehaviour
{
    // Code based on https://answers.unity.com/questions/1033085/heightmap-to-mesh.html

    // Start is called before the first frame update
    void Start()
    {
        //Texture2D hMap = Resources.Load("Maze512") as Texture2D; // Test image
        Texture2D hMap = Resources.Load("MazeTall") as Texture2D; // Test image

        List<Vector3> verts = new List<Vector3>();
        List<int> tris = new List<int>();
        int maxSize = 256; // Maximum mesh size is 256 by 265
        
        // Sample pixels on intervals since mesh vertices are limited
        int pixelSkipX = (int)Mathf.Ceil((float)hMap.width  / (float)maxSize); 
        int pixelSkipY = (int)Mathf.Ceil((float)hMap.height / (float)maxSize);
        // How many vertices will be sampled in each dimension
        int meshSizeX  = hMap.width / pixelSkipX;
        int meshSizeY  = hMap.height / pixelSkipY;
        // Orthographic camera size in pixels (Screen.width and height aren't correct when using orthographic)
        float camSizeY = Camera.main.orthographicSize * 100; // orthographicSize is half the screen height, 100 pixels per unit
        float camSizeX = camSizeY * Screen.width / Screen.height;
        // How much to stretch the mesh to fill the screen
        float meshScaleX = ((float)camSizeX / (float)meshSizeX) / 50; // (100 pixels per unit)/2 =50
        float meshScaleY = ((float)camSizeY / (float)meshSizeY) / 50;

        //Loop through all the pixels in the image
        //Sample pixels on intervals
        //And assign greyscale values as heights to mesh vertices
        for (int i = 0; i < meshSizeX; i++)
        {
            for (int j = 0; j < meshSizeY; j++)
            {
                //Add each new vertex in the plane
                //-1 multiplyer sinks white areas 1 unit below black areas
                verts.Add(new Vector3((float)i*meshScaleX, 
                                        -1 * hMap.GetPixel(i*pixelSkipX, j*pixelSkipY).grayscale, 
                                        (float)j*meshScaleY));

                //Skip if a new square on the plane hasn't been formed
                if (i == 0 || j == 0) continue;
                //Adds the index of the three vertices in order to make up each of the two tris
                tris.Add(meshSizeY * i + j); //Top right
                tris.Add(meshSizeY * i + j - 1); //Bottom right
                tris.Add(meshSizeY * (i - 1) + j - 1); //Bottom left - First triangle
                tris.Add(meshSizeY * (i - 1) + j - 1); //Bottom left 
                tris.Add(meshSizeY * (i - 1) + j); //Top left
                tris.Add(meshSizeY * i + j); //Top right - Second triangle
            }
        }

        Vector2[] uvs = new Vector2[verts.Count];
        for (var i = 0; i < uvs.Length; i++) //Give UV shader scaling coordinates
            uvs[i] = new Vector2(50*verts[i].x/camSizeX, 50*verts[i].z/camSizeY);

        GameObject plane = new GameObject("ProcPlane"); //Create GO and add necessary components
        // Center the mesh
        plane.transform.position = new Vector3(-camSizeX/100, 0, -camSizeY/100); 
        plane.AddComponent<MeshFilter>();
        plane.AddComponent<MeshRenderer>();
        plane.AddComponent<MeshCollider>(); //Need collider for ball to roll in mesh
        Mesh procMesh = new Mesh();
        procMesh.vertices = verts.ToArray(); //Assign verts, uvs, and tris to the mesh
        procMesh.uv = uvs;
        procMesh.triangles = tris.ToArray();
        procMesh.RecalculateNormals(); //Determines which way the triangles are facing
        plane.GetComponent<MeshFilter>().mesh = procMesh; //Assign Mesh object to MeshFilter

        plane.GetComponent<Renderer>().material.mainTexture = hMap; // Display the maze image
        // Need to figure out how to make the height map mesh the collider mesh as well
        plane.GetComponent<MeshCollider>().sharedMesh = procMesh; //Make the collider mesh 
    }
}
