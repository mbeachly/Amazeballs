using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

//Save and load game data so that data persists between game sessions using data serialization, writing out binary files
//Source:  https://learn.unity.com/tutorial/persistence-saving-and-loading-data#5cf18288edbc2a3094da073b 
//Source:  https://docs.unity3d.com/Manual/script-Serialization.html
public class GameController : MonoBehaviour
{
    public static GameController control;

    //Singleton design pattern, only have one instance of this game object
    void Awake()
    {
        if (control == null)
        {
            DontDestroyOnLoad(gameObject);
            control = this; 
        }
        else if (control != this)
        {
            Destroy(gameObject);
        }
    }
 
    //Save current start and endpoint position and texture2D data to /mazeInfo.dat
    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/mazeInfo.dat");
        MazeData data = new MazeData();
        data.bytes = Globals.tex.EncodeToPNG();
        //System.IO.File.WriteAllBytes(Application.persistentDataPath + "/mazeImage.png", Globals.tex.EncodeToPNG());
        data.startPositionX = Globals.startPosition.x;
        data.startPositionY = Globals.startPosition.y;
        data.startPositionZ = Globals.startPosition.z;

        data.endPositionX = Globals.endPosition.x;
        data.endPositionY = Globals.endPosition.y;
        data.endPositionZ = Globals.endPosition.z;

        data.gameSaved = Globals.gameSaved;

        bf.Serialize(file, data);
        file.Close();
    }

    //Load game data from /mazeInfo.dat, deserialize file and set Globals.tex and start and end points
    public void Load()
    {
        if(File.Exists(Application.persistentDataPath + "/mazeInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/mazeInfo.dat", FileMode.Open);
            MazeData data = (MazeData)bf.Deserialize(file);
            file.Close();

            //byte[] bytes;
            //bytes = System.IO.File.ReadAllBytes(Application.persistentDataPath + "/mazeImage.png");
            Globals.tex.LoadImage(data.bytes);

            Globals.startPosition.x = data.startPositionX;
            Globals.startPosition.y = data.startPositionY;
            Globals.startPosition.z = data.startPositionZ;

            Globals.endPosition.x = data.endPositionX;
            Globals.endPosition.y = data.endPositionY;
            Globals.endPosition.z = data.endPositionZ;

            Globals.gameSaved = data.gameSaved;

        }
    }
}

//MazeData class contains data to be serialized, allows us to have object to write to file  
[Serializable]
class MazeData
{
    public byte[] bytes;

    public float startPositionX;
    public float startPositionY;
    public float startPositionZ;

    public float endPositionX;
    public float endPositionY;
    public float endPositionZ;

    public bool gameSaved;
}
