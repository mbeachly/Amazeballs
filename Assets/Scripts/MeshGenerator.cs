using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshGenerator : MonoBehaviour
{
    

    // Start is called before the first frame update
    void Start()
    {
        Texture2D hMap = Resources.Load("Maze") as Texture2D;
        // Code based on https://answers.unity.com/questions/1033085/heightmap-to-mesh.html
        List<Vector3> verts = new List<Vector3>();
        List<int> tris = new List<int>();
        int meshSize = 250; // How many vertices/pixels wide, maximum is around 265 I think
        int vertScale = 40; // I don't know what I'm doing, there must be a smarter way than this
        int imageScale = 20;

        //Bottom left section of the map, other sections are similar
        for (int i = 0; i < meshSize; i++)
        {
            for (int j = 0; j < meshSize; j++)
            {
                //Add each new vertex in the plane
                verts.Add(new Vector3((float)i/vertScale, -1 * hMap.GetPixel(i, j).grayscale, (float)j/vertScale));
                // Added -1 multiplyer to make black areas 1 unit higher than white areas
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
        plane.AddComponent<MeshFilter>();
        plane.AddComponent<MeshRenderer>();
        Mesh procMesh = new Mesh();
        procMesh.vertices = verts.ToArray(); //Assign verts, uvs, and tris to the mesh
        procMesh.uv = uvs;
        procMesh.triangles = tris.ToArray();
        procMesh.RecalculateNormals(); //Determines which way the triangles are facing
        plane.GetComponent<MeshFilter>().mesh = procMesh; //Assign Mesh object to MeshFilter

        plane.GetComponent<Renderer>().material.mainTexture = hMap; // Display the maze image
        // Need to figure out how to make the height map mesh the collider mesh as well
        plane.GetComponent<MeshCollider>().sharedMesh = procMesh; 
    }
}
