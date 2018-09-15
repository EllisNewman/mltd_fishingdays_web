using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    #region Singleton
    private static SaveManager s_instance;
    public static SaveManager instance
    {
        get
        {
            if (s_instance == null)
                s_instance = new SaveManager();
            return s_instance;
        }
    }

    public SaveManager()
    {
        s_instance = this;
    }
    #endregion

    public SaveFileInfo saveFileCache;
    public SettingSave settingFile;
    private string settingFilePath;
    void Awake()
    {
        DontDestroyOnLoad(this);
        saveFileCache = new SaveFileInfo();
        settingFile = new SettingSave();
        settingFilePath = Application.dataPath + "/Resources/Setting.json";
        LoadSettingFile();
    }

    public void Save(SaveFileInfo file)
    {
        CreateSaveFileCache();
        string json = JsonUtility.ToJson(saveFileCache);
        Debug.Log(json);
    }

    public void Load(SaveFileInfo file)
    {
        saveFileCache = file;
    }

    public void AddFlag(int flag, int value)
    {
        saveFileCache.flagDict.Add(flag, value);
    }

    public void CreateSaveFileCache()
    {

    }

    public void ClearSaveFileCache()
    {
        saveFileCache = new SaveFileInfo();
    }

    public void CreateFastSave()
    {

    }

    void LoadSettingFile()
    {
        if (!File.Exists(settingFilePath))
        {
            SaveSettingFile(); // create default setting file if not exists
        }
        StreamReader sr = new StreamReader(settingFilePath);
        string json = sr.ReadToEnd();
        if (json.Length > 0)
        {
            settingFile = JsonUtility.FromJson<SettingSave>(json);
        }
        
        ApplySetting();
    }

    public void SaveSettingFile()
    {
        string json = JsonUtility.ToJson(settingFile);
        File.WriteAllText(settingFilePath, json, Encoding.UTF8);

        ApplySetting();
    }

    public void ApplySetting()
    {

    }

}

[Serializable]
public class SaveFileInfo
{
    public string playerName;
    public string bkgrdName;
    public string scenarioName;
    public int chapterId;
    public int sceneId;
    public int sentenceId;
    public Dictionary<int, int> flagDict = new Dictionary<int, int>(); 

    public SaveFileInfo() { }

    public SaveFileInfo(string player, string bkgrd, string scenario, int chapter, int scene, int sentence)
    {
        playerName = player;
        bkgrdName = bkgrd;
        scenarioName = scenario;
        chapterId = chapter;
        sceneId = scene;
        sentenceId = sentence;
    }

}

[Serializable]
public class SettingSave
{
    public double bgmVolume = 0.5f;
    public double soundVolume = 0.5f;
    public int txtPopSpeed = 7;
    public int autoPopSpeed = 13;

    public float BgmVolume
    {
        get { return (float) bgmVolume; }
        set { bgmVolume = (double) value; }
    }
    public float SoundVolume
    {
        get { return (float) soundVolume; }
        set { soundVolume = (double) value; }
    }

    public bool isLrcDisplay = true;
    public bool isAutoPop = true;
    public bool isFullScreen = false;

    public SettingSave() { }

}