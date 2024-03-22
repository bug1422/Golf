using Newtonsoft.Json;
using System;
using System.IO;
using UnityEngine;

public static class DataService
{
    public static string savePath = Directory.GetCurrentDirectory() + "\\SaveFolder\\";
    private static JsonSerializerSettings _settings = new JsonSerializerSettings()
    {
        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
    };
    public static bool SaveData<T>(string filename, T data)
    {
        try
        {
            string fileAbsolutePath = savePath + filename; //user.json
            if (File.Exists(fileAbsolutePath))
            {
                File.Delete(fileAbsolutePath);
            }
            File.Create(fileAbsolutePath).Close();
            File.WriteAllText(fileAbsolutePath, JsonConvert.SerializeObject(data, _settings));
            return true;
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
            return false;
        }
    }
    public static T LoadData<T>(string filename)
    {
        string fileAbsolutePath = savePath + filename;
        if (File.Exists(fileAbsolutePath) == false)
        {
            Debug.Log("file not exist");
            throw new FileNotFoundException();
        }
        try
        {
            T data = JsonConvert.DeserializeObject<T>(File.ReadAllText(fileAbsolutePath));
            return data;
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
            throw ex;
        }
    }
}
