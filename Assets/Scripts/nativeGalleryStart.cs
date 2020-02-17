using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

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
			maxSize = 512;
			
			NativeGallery.Permission permission = NativeGallery.GetImageFromGallery( ( path ) =>
			{
				Debug.Log( "Image path: " + path );
				if( path != null )
				{	
					Globals.backgroundFile = path;
					// Create Texture from selected image
					Texture2D texture = NativeGallery.LoadImageAtPath( path, maxSize );
					if( texture == null )
					{
						Debug.Log( "Couldn't load texture from " + path );
						return;
					}
					mySprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);
					//Space.GetComponent<Image>().sprite = mySprite;
					GameObject.Find("Space").GetComponent<Image>().sprite = mySprite;
					
					byte[] bytes = texture.EncodeToPNG();
					//var tex = new Texture2D(1, 1);
					Globals.tex = texture;
					Globals.tex.LoadImage(bytes);
				}
			}, "Select a PNG image", "image/png" );

			Debug.Log( "Permission result: " + permission );
		}
	}
}