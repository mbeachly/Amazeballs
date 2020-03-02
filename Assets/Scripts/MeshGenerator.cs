using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// MeshGenerator loads an texture from an image file,
// loops through pixels in the image to detect dark areas (walls)
// light areas (floors), and edge pixels (boundaries).
// Builds a 3D mesh with vertices corresponding to the sampled
// pixels and assigns vertex height based on whether the pixel
// is detected as wall, floor, or boundary. 
// Source: https://answers.unity.com/questions/1033085/heightmap-to-mesh.html
public class MeshGenerator : MonoBehaviour
{ 
    // Start is called before the first frame update
    void Start()
    {
        Globals.pickStep = 0; // Reset start/end point detection

        Texture2D hMap = Globals.tex; // Load the selected texture (maze photo)

        List<Vector3> verts = new List<Vector3>(); // Mesh vertice coordinates
        List<int> tris = new List<int>(); // Mesh triangle to vertex assignments

        // Maximum mesh vertices is 256 * 265 - 1 = 65535 vertices
        int maxSize = 250;

        // Sample pixels on intervals to avoid sampling more than vertex limit
        int pixelSkipX = (int)Mathf.Ceil((float)hMap.width / (float)maxSize);
        // How many vertices will be sampled in X dimension
        int meshSizeX = hMap.width / pixelSkipX;

        // Update maxSize to allow more vertices in Z dimension based on what X dimension doesn't use
        maxSize = (int)Mathf.Floor(65535f / (float)meshSizeX);

        int pixelSkipZ = (int)Mathf.Ceil((float)hMap.height / (float)maxSize);
        int meshSizeZ = hMap.height / pixelSkipZ;

        // Orthographic camera size in pixels (Screen.width and height aren't correct when using orthographic)
        float camSizeZ = Camera.main.orthographicSize * 100; // orthographicSize is half the screen height, 100 pixels per unit
        float camSizeX = camSizeZ * Screen.width / Screen.height;

        // How much to stretch the mesh to fill the screen
        float meshScaleX = ((float)camSizeX / (float)meshSizeX) / 50; // (100 pixels per unit)/2 = 50
        float meshScaleZ = ((float)camSizeZ / (float)meshSizeZ) / 50;

        // Height-map value determined from pixel grayscale value
        float vertHeight;

        // Store color of a pixel
        Color pixel;

        // Loop through all the pixels in the image
        // Sample pixels on intervals
        // Assign heights to vertices based on greyscale values of pixels
        for (int i = 0; i < meshSizeX; i++) // X dimension
        {
            for (int j = 0; j < meshSizeZ; j++) // Z dimension
            {   // Check for border vertices
                if (i == 0 || j == 0 || i == meshSizeX - 1 || j == meshSizeZ - 1)
                {
                    vertHeight = 2f; // Extra high border wall: height = 2
                }
                // Get grayscale value of pixel and determine if it is wall or floor
                else //if (hMap.GetPixel(i * pixelSkipX, j * pixelSkipZ).grayscale > Globals.threshBW)
                {
                    pixel = hMap.GetPixel(i * pixelSkipX, j * pixelSkipZ);
                    if (pixel.b < Globals.edgeThresh && pixel.r < Globals.edgeThresh)
                    {   // Dark areas are walls
                        vertHeight = 0f; // Wall height = 0
                    }
                    else // Colored areas and white areas are floors
                    {
                        vertHeight = -2f; // Sink 2 units below black areas (walls)
                    }
                }
                // Append each new vertex in the plane
                verts.Add(new Vector3((float)i * meshScaleX, vertHeight, (float)j * meshScaleZ));

                // Skip if a new square on the plane hasn't been formed
                if (i == 0 || j == 0) continue;
                // Adds the index of the three vertices in order to make up each of the two tris
                tris.Add(meshSizeZ * i + j); // Top right
                tris.Add(meshSizeZ * i + j - 1); // Bottom right
                tris.Add(meshSizeZ * (i - 1) + j - 1); // Bottom left - First triangle
                tris.Add(meshSizeZ * (i - 1) + j - 1); // Bottom left 
                tris.Add(meshSizeZ * (i - 1) + j); // Top left
                tris.Add(meshSizeZ * i + j); // Top right - Second triangle
            }
        }

        // uv vector will assign image coordinates to vertices in the mesh 
        // corresponding to coordinates in the applied image/shader/texture
        // ranging from x=0 (left) to x=1 (right) and y=0 (bottom) to y=1 (top) of image
        Vector2[] uvs = new Vector2[verts.Count];
        for (var i = 0; i < uvs.Length; i++) // Multiply 50 because 50 = (100 pixels/unit)/2
            uvs[i] = new Vector2(50 * verts[i].x / camSizeX, 50 * verts[i].z / camSizeZ);

        GameObject plane = new GameObject("ProcPlane"); // Create game object

        // Center the mesh
        plane.transform.position = new Vector3(-camSizeX / 100, 0, -camSizeZ / 100); // 100 pixels per unit

        plane.AddComponent<MeshFilter>(); // Add Mesh Filter to Game Object
        plane.AddComponent<MeshRenderer>(); // Add Mesh Renderer to Game Object
        plane.AddComponent<MeshCollider>(); // Need collider for ball to roll in mesh

        Mesh procMesh = new Mesh(); // Instantiate a mesh

        procMesh.vertices = verts.ToArray(); // Assign verts, uvs, and tris to the mesh
        procMesh.uv = uvs; // Assign uv vector for mapping image to mesh vertices
        procMesh.triangles = tris.ToArray(); // Assign vertices to triangles
        procMesh.RecalculateNormals(); // Make sure triangles are facing upwards

        plane.GetComponent<MeshFilter>().mesh = procMesh; // Assign Mesh object to MeshFilter
        plane.GetComponent<Renderer>().material.mainTexture = hMap; // Display the maze image
        plane.GetComponent<MeshCollider>().sharedMesh = procMesh; // Assign collider mesh or ball will fall through

        Globals.gameSaved = true;  // Set gameSaved for load previous game functionality
    }
}
