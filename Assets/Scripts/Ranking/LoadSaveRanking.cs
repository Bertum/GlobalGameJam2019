using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class LoadSaveRanking : MonoBehaviour
{
    public void SaveFile(RankingData rankings)
    {
        string destination = Application.persistentDataPath + "/save.dat";
        FileStream file;

        if (File.Exists(destination)) file = File.OpenWrite(destination);
        else file = File.Create(destination);

        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, rankings);
        file.Close();
    }

    public RankingData LoadFile()
    {
        string destination = Application.persistentDataPath + "/save.dat";
        RankingData data = new RankingData();
        FileStream file;

        if (File.Exists(destination)) file = File.OpenRead(destination);
        else
        {
            Debug.Log("File not found");
            return new RankingData();
        }

        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            data = (RankingData)bf.Deserialize(file);
        }
        catch (Exception ex)
        {
            data = new RankingData();
        }
        finally
        {
            file.Close();
        }
        return data;
    }
}
