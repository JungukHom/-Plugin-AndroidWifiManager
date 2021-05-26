package com.developer0223.androidwifimanager;

import java.util.List;
import android.util.Log;
import android.os.Environment;
import android.content.Context;
import android.net.wifi.ScanResult;
import android.net.wifi.WifiManager;
import android.net.wifi.WifiConfiguration;
import android.widget.Toast;

public class AndroidWifiManager {
    private static final String TAG = "Unity";
    private static final String ERROR_CONTEXT_NULL = "AndroidWifiManager : AndroidWifiManager.context is null.";
    private static final String ERROR_WIFI_DISABLED = "AndroidWifiManager : Wifi is disabled.";

    private static final String WIFI_STATE_DISABLING = "WIFI_STATE_DISABLING";
    private static final String WIFI_STATE_DISABLED = "WIFI_STATE_DISABLED";
    private static final String WIFI_STATE_ENABLING = "WIFI_STATE_ENABLING";
    private static final String WIFI_STATE_ENABLED = "WIFI_STATE_ENABLED";
    private static final String WIFI_STATE_UNKNOWN = "WIFI_STATE_UNKNOWN";

    // private application context
    private Context applicationContext = null;

    public AndroidWifiManager(Context context) {
        initialize(context);
    }

    public void initialize(Context context) {
        applicationContext = context;
    }

    // get wifi enabled
    public boolean getWifiEnabled() {
        return getWifiManager().getWifiState() == WifiManager.WIFI_STATE_ENABLED;
    }

    // get wifi state
    public int getWifiState() {
        if (applicationContext == null) {
            Log.e(TAG, ERROR_CONTEXT_NULL);
            return 1;
        }
        return getWifiManager().getWifiState();
    }

    // get wifi state to string
    public String getWifiStateString() {
        if (applicationContext == null) {
            Log.e(TAG, ERROR_CONTEXT_NULL);
            return WIFI_STATE_UNKNOWN;
        }

        int wifiState = getWifiManager().getWifiState();
        switch (wifiState) {
            case WifiManager.WIFI_STATE_DISABLING: // 0
                return WIFI_STATE_DISABLING;

            case WifiManager.WIFI_STATE_DISABLED: // 1
                return WIFI_STATE_DISABLED;

            case WifiManager.WIFI_STATE_ENABLING: // 2
                return WIFI_STATE_ENABLING;

            case WifiManager.WIFI_STATE_ENABLED: // 3
                return WIFI_STATE_ENABLED;

            default: // WifiManager.WIFI_STATE_UNKNOWN // 4
                return WIFI_STATE_UNKNOWN;
        }
    }

    // set wifi enabled
    public void setWifiEnabled(boolean enabled) {
        if (applicationContext == null) {
            Log.e(TAG, ERROR_CONTEXT_NULL);
            return;
        }
        getWifiManager().setWifiEnabled(enabled);
    }

    // get existing wifi list
    public String[] getWifiList() {
        String[] result = new String[0];

        if (applicationContext == null) {
            Log.e(TAG, ERROR_CONTEXT_NULL);
            return result;
        }

        WifiManager wifiManager = getWifiManager();
        if (!getWifiEnabled()) {
            Log.e(TAG, ERROR_WIFI_DISABLED);
            return result;
        }

        List<ScanResult> scanResults = wifiManager.getScanResults();
        result = new String[scanResults.size()];
        for (int i = 0; i < scanResults.size(); i++) {
            result[i] = scanResults.get(i).SSID;
        }

        return result;
    }

    public boolean connect(String networkSSID, String networkPassword, boolean wpa) throws SecurityException {
        if (applicationContext == null) {
            Log.e(TAG, ERROR_CONTEXT_NULL);
            return false;
        }
        if (!getWifiEnabled()) {
            Log.e(TAG, ERROR_WIFI_DISABLED);
        }

        WifiConfiguration configuration = new WifiConfiguration();
        configuration.SSID = "\"" + networkSSID + "\"";
        if (wpa) {
            configuration.preSharedKey = "\"" + networkPassword + "\"";
            configuration.allowedKeyManagement.set(WifiConfiguration.KeyMgmt.WPA_PSK);
        } else {
            configuration.allowedKeyManagement.set(WifiConfiguration.KeyMgmt.NONE);
        }

        WifiManager wifiManager = getWifiManager();
        wifiManager.addNetwork(configuration);

        List<WifiConfiguration> list = wifiManager.getConfiguredNetworks();
        for (WifiConfiguration _configuration : list) {
            String _ssid = _configuration.SSID;
            int _networkID = _configuration.networkId;
            if (_ssid != null && _ssid.equals("\"" + networkSSID + "\"")) {
                wifiManager.disconnect();
                wifiManager.enableNetwork(_networkID, true);
                return wifiManager.reconnect();
            }
        }

        return false;
    }

    public boolean disconnect() {
        if (applicationContext == null) {
            Log.e(TAG, ERROR_CONTEXT_NULL);
            return false;
        }
        return getWifiManager().disconnect();
    }

    public void showToast(String message) {
        Toast.makeText(applicationContext, message, Toast.LENGTH_SHORT).show();
    }

    public String getRootPath() {
        return Environment.getExternalStorageDirectory().getAbsolutePath();
    }

    private WifiManager getWifiManager() {
        return (WifiManager) getApplicationContext().getSystemService(Context.WIFI_SERVICE);
    }

    private Context getApplicationContext() {
        return applicationContext;
    }
}
