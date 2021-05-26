// System
using System;
using System.Collections;
using System.Collections.Generic;

// Unity
using UnityEngine;
using UnityEngine.UI;

// Project
// Alias

public class WifiConnectorSample : MonoBehaviour
{
    public Button btn_enable_wifi;
    public Button btn_disable_wifi;
    public Button btn_connect_wifi;
    public Button btn_disconnect_wifi;
    public Button btn_show_wifi_list;

    public Text txt_wifi_list;

    public InputField input_wifi_ssid;
    public InputField input_wifi_password;

    public Button btn_wifi_sample_vrcms;
    public Button btn_wifi_sample_fe_network_5g;

    private void Awake()
    {
        AddListeners();
    }

    private void AddListeners()
    {
        btn_enable_wifi.onClick.AddListener(OnEnableWifiButtonPressed);
        btn_disable_wifi.onClick.AddListener(OnDisableWifiButtonPressed);
        btn_connect_wifi.onClick.AddListener(OnConnectWifiButtonPressed);
        btn_disconnect_wifi.onClick.AddListener(OnDisconnectWifiButtonPressed);
        btn_show_wifi_list.onClick.AddListener(OnShowWifiListButtonPressed);

        btn_wifi_sample_vrcms.onClick.AddListener(OnSampleVrCmsButtonPressed);
        btn_wifi_sample_fe_network_5g.onClick.AddListener(OnSampleFeNetwork5GButtonPressed);
    }

    private void OnEnableWifiButtonPressed()
    {
        WifiConnector.Instance.SetWifiEnabled(true);
    }

    private void OnDisableWifiButtonPressed()
    {
        WifiConnector.Instance.SetWifiEnabled(false);
    }

    private void OnConnectWifiButtonPressed()
    {
        bool result = WifiConnector.Instance.Connect(input_wifi_ssid.text.Trim(), input_wifi_password.text.Trim());
        if (result)
            WifiConnector.Instance.Toast("와이파이가 성공적으로 연결되었습니다");
        else
            WifiConnector.Instance.Toast("와이파이 연결에 실패하였습니다");
    }

    private void OnDisconnectWifiButtonPressed()
    {
        WifiConnector.Instance.DisconnectWifi();
    }

    private void OnShowWifiListButtonPressed()
    {
        string[] wifiList = WifiConnector.Instance.GetWifiList();
        txt_wifi_list.text = "";
        foreach (string wifi in wifiList)
        {
            txt_wifi_list.text += $"{wifi}\n";
        }
    }

    private void OnSampleVrCmsButtonPressed()
    {
        input_wifi_ssid.text = "vrcms";
        input_wifi_password.text = "DoGeon94";
    }

    private void OnSampleFeNetwork5GButtonPressed()
    {
        input_wifi_ssid.text = "FE_NETWORK_5G";
        input_wifi_password.text = "fakeeyes!0906";
    }
}