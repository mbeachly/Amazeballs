using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameController : MonoBehaviour
{
    public static GameController control;

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
            Debug.Log(Globals.gameSaved);
            Debug.Log(Globals.endPosition.y);

        }
    }

    public void DeleteMazeData()
    {
        if (File.Exists(Application.persistentDataPath + "/mazeInfo.dat"))
        {
            File.Delete(Application.persistentDataPath + "/mazeInfo.dat");
        }
    }
}

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
