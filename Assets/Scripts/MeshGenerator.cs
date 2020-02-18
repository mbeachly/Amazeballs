using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshGenerator : MonoBehaviour
{
    // Code based on https://answers.unity.com/questions/1033085/heightmap-to-mesh.html

    // Start is called before the first frame update
    void Start()
    {
        Texture2D hMap = Resources.Load("Maze512") as Texture2D; // Test image
        
        List<Vector3> verts = new List<Vector3>();
        List<int> tris = new List<int>();
        int meshSize = 256; // How many vertices wide? Maximum is around 265
        int meshScale = 20; // I don't know what I'm doing, there must be a smarter way than this
        float imageScale = 12.75f; // Again this is bad
        int pixelSkipX = 2; 
        int pixelSkipY = 2;

        //Bottom left section of the map, other sections are similar
        for (int i = 0; i < meshSize; i++)
        {
            for (int j = 0; j < meshSize; j++)
            {
                //Add each new vertex in the plane
                //-1 multiplyer sinks white areas 1 unit below black areas
                verts.Add(new Vector3((float)i/meshScale, 
                                        -1 * hMap.GetPixel(i*pixelSkipX, j*pixelSkipY).grayscale, 
                                        (float)j/meshScale));

                //Skip if a new square on the plane hasn't been formed
                if (i == 0 || j == 0) continue;
                //Adds the index of the three vertices in order to make up each of the two tris
                tris.Add(meshSize * i + j); //Top right
                tris.Add(meshSize * i + j - 1); //Bottom right
                tris.Add(meshSize * (i - 1) + j - 1); //Bottom left - First triangle
                tris.Add(meshSize * (i - 1) + j - 1); //Bottom left 
                tris.Add(meshSize * (i - 1) + j); //Top left
                tris.Add(meshSize * i + j); //Top right - Second triangle
            }
        }

        Vector2[] uvs = new Vector2[verts.Count];
        for (var i = 0; i < uvs.Length; i++) //Give UV coords X,Z world coords
            uvs[i] = new Vector2(verts[i].x/imageScale, verts[i].z/imageScale);

        GameObject plane = new GameObject("ProcPlane"); //Create GO and add necessary components
        plane.transform.position = new Vector3(-5, 0, -5); // Center maze
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
