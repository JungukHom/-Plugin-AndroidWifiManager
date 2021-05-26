// System
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

// Unity
using UnityEngine;
using UnityEngine.UI;

// Project
// Alias

public class WifiInfoReader : MonoBehaviour
{
    private readonly string FileName = "WifiInfo.csv";

    private void Start()
    {
        GetWifiData(out string name, out string password);
        Debug.Log(name);
        Debug.Log(password);
    }

    public bool GetWifiData(out string name, out string password)
    {
        string path = Path.Combine(Directory.GetParent(Application.persistentDataPath).ToString(), FileName);
        Debug.Log($"path : {path}");
        if (File.Exists(path))
        {
            List<Dictionary<string, object>> list = CSVReader.Read(path);
            name = list[0]["name"].ToString();
            password = list[0]["password"].ToString();
            return true;
        }
        else
        {
            name = "";
            password = "";
            return false;
        }
    }
}