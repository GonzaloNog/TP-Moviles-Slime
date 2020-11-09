using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PluginControler : MonoBehaviour
{
    public Text outputText;
    const string PLUGUIN_NAME = "com.zeissgonzalo.gztools.GZMain";
    static AndroidJavaClass _pluginClass = null;
    
    public static AndroidJavaClass PluginClass
    {
        get
        {
            if (_pluginClass == null)
                _pluginClass = new AndroidJavaClass(PLUGUIN_NAME);
            return _pluginClass;
        }
    }

    static AndroidJavaObject _pluginInstace = null;
    public AndroidJavaObject PluginInstace
    {
        get
        {
            if(_pluginInstace == null)
            {
                _pluginInstace = PluginClass.CallStatic<AndroidJavaObject>("getInstance");
            }
            return _pluginInstace;
        }
    }
  
    void SendLog(string msj)
    {
        PluginInstace.Call("sendLog", msj);
    }
    string GetLogs()
    {
        return PluginInstace.Call<string>("getAllLogs");
    }

    public void TestPluginBtn()
    {
        if(Application.platform != RuntimePlatform.Android)
        {
            Debug.LogWarning("You are not in android plataform");
            outputText.text = "You are not in android plataform";
            return;
        }

        SendLog("hola");
        if(_pluginClass == null)
        {
            outputText.text = "Plugin Class null";
        }
        else if (_pluginInstace == null)
        {
            outputText.text = "Plugin Object null";
        }
        else if(_pluginClass != null)
        {
            outputText.text = "Problema de android";
            //outputText.text = GetLogs();
        }
        outputText.text = "Problema de android";
    }
}
