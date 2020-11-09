package com.zeissgonzalo.gztools;

import android.util.Log;

import java.util.ArrayList;

public class GZMain {
    private static final String LOG_TAG = "GZMain";
    private static final String GAME_TAG = "TPNro2";

    private static final GZMain _instance = new GZMain();
    public  static GZMain getInstance() { return _instance; }
    /*public static GZMain getInstance(){
        if(_instance == null){
            Log.d(LOG_TAG, "GZMain created");
            _instance = new GZMain();
        }
        return _instance;
    }*/

    private ArrayList<String> allLogs = new ArrayList<String>();

    public void sendLog(String msj){
        Log.d(GAME_TAG, msj);
        allLogs.add(msj);
    }

    private static final String SEPARATOR = "\n";
    public String getAllLogs(){
        Log.d(LOG_TAG,"getAllLogs()");
        StringBuilder logs = new StringBuilder();
        for(int a = 0; a < allLogs.size();a++){
            logs.append(allLogs.get(a)).append(SEPARATOR);
        }
        return "que miras";
        //return logs.toString();
    }
}
