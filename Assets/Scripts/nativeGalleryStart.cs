using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


/// <summary>
/// Allows user to access Photos or Gallery on Android device
/// and load the photo. Creates a readable copy of the photo 
/// to be used for the maze mesh.
/// </summary>
public class nativeGalleryStart : MonoBehaviour
{
	/*
	void Update()
	{
		// Don't attempt to pick media from Gallery/Photos if
		// another media pick operation is already in progress
		if( NativeGallery.IsMediaPickerBusy() )
			return;
		else
		{
			// Pick a PNG image from Gallery/Photos
			// If the selected image's width and/or height is greater than 512px, down-scale the image
			PickImage( 512 );
		}
	}
	*/
	// Sprite variables that will be attached user selected texture
	private Sprite mySprite;
	
	public void PickImage( int maxSize )
	{
		if( NativeGallery.IsMediaPickerBusy() )
			return;
		else
		{
			maxSize = 1024;
			
			NativeGallery.Permission permission = NativeGallery.GetImageFromGallery( ( path ) =>
			{
				Debug.Log( "Image path: " + path );
				if( path != null )
				{	
					Globals.backgroundFile = path;
					// Create Texture from selected image
					Texture2D texture = NativeGallery.LoadImageAtPath( path, maxSize );
					if (texture == null)
					{
						Debug.Log("Couldn't load texture from " + path);
						return;
					}

					// Make a readable copy of texture since texture loaded by native gallery is not
					// The texture must be readable/writeable in order to be applied to a mesh
					// Source: https://support.unity3d.com/hc/en-us/articles/206486626-How-can-I-get-pixels-from-unreadable-textures-
					// Create a temporary RenderTexture of the same size as the texture
					RenderTexture tmp = RenderTexture.GetTemporary(
										texture.width,
										texture.height,
										0,
										RenderTextureFormat.Default,
										RenderTextureReadWrite.Linear);

					// Blit the pixels on texture to the RenderTexture
					Graphics.Blit(texture, tmp);
					// Backup the currently set RenderTexture
					RenderTexture previous = RenderTexture.active;
					// Set the current RenderTexture to the temporary one we created
					RenderTexture.active = tmp;
					// Create a new readable Texture2D to copy the pixels to it
					Texture2D myTexture2D = new Texture2D(texture.width, texture.height);
					// Copy the pixels from the RenderTexture to the new Texture
					myTexture2D.ReadPixels(new Rect(0, 0, tmp.width, tmp.height), 0, 0);
					myTexture2D.Apply();
					// Reset the active RenderTexture
					RenderTexture.active = previous;
					// Release the temporary RenderTexture
					RenderTexture.ReleaseTemporary(tmp);
					// "myTexture2D" now has the same pixels from "texture" and it's readable.

					// Display the selected picture in the background for a preview
					mySprite = Sprite.Create(myTexture2D, new Rect(0.0f, 0.0f, myTexture2D.width, myTexture2D.height), new Vector2(0.5f, 0.5f), 100.0f);
					GameObject.Find("Space").GetComponent<Image>().sprite = mySprite;

					Globals.gameSaved = false;
					Globals.tex = myTexture2D;
				}
			}, "Select a PNG image", "image/png" );

			Debug.Log( "Permission result: " + permission );
		}
	}
}