// System
using System;
using System.Collections;
using System.Collections.Generic;

// Unity
using UnityEngine;
using UnityEngine.UI;

// Project
// Alias

public class WifiConnector : MonoBehaviour
{
    #region SingleTon
    public static WifiConnector Instance = null;
    #endregion

    private AndroidJavaClass unityPlayer;
    private AndroidJavaObject currentActivity;
    private AndroidJavaObject wifiConnector;

    private void Awake()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        Initialize();
#endif
    }

    private void Initialize()
    {
        Instance = this;

        unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        wifiConnector = new AndroidJavaObject("com.developer0223.androidwifimanager.AndroidWifiManager");
        wifiConnector.Call("initialize", currentActivity);
    }

    public void Toast(string text)
    {
        wifiConnector.Call("showToast", text);
    }

    public void SetWifiEnabled(bool enable)
    {
        wifiConnector.Call("setWifiEnabled", enable);
    }

    public string[] GetWifiList()
    {
        return wifiConnector.Call<string[]>("getWifiList");
    }

    public bool Connect(string ssid, string password, bool wpa = true)
    {
        return wifiConnector.Call<bool>("connect", ssid, password, wpa);
    }

    public bool DisconnectWifi()
    {
        return wifiConnector.Call<bool>("disconnect");
    }

    public string GetAndroidRootPath()
    {
        return wifiConnector.Call<string>("getRootPath");
    }
}