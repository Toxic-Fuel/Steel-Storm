using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class Setting
{
    public int quality;
    public float volume;
}
public class MainMenu : MonoBehaviour
{
    string savePath;
    string savePathQuality;
    public GameObject settingsUI;
    public TMP_Dropdown dropDown;
    int quality;
    private void Start()
    {
        savePath = Path.Combine(Application.persistentDataPath, "checkpoint.json");
        savePathQuality = Path.Combine(Application.persistentDataPath, "settings.json");
        if (!File.Exists(savePathQuality))
        {
            Setting setting = new Setting { quality = dropDown.value, volume = 0 };
            string jsonRead = JsonUtility.ToJson(setting);
            File.WriteAllText(savePath, jsonRead);
        }
            

        string json = File.ReadAllText(savePathQuality);
        Setting settingLoad = JsonUtility.FromJson<Setting>(json);
        QualitySettings.SetQualityLevel(settingLoad.quality);
        quality = settingLoad.quality;
        
    }
    public void PlayButton()
    {
        if (!File.Exists(savePath))
        {
            SceneManager.LoadScene("Main");
            return;
        }
            

        string json = File.ReadAllText(savePath);
        CheckpointData data = JsonUtility.FromJson<CheckpointData>(json);
        SceneManager.LoadScene(data.CheckpointLevel);

    }
    public void OptionButton()
    {
        settingsUI.SetActive(true);
        dropDown.value = quality;
    }
    public void CloseOption()
    {
        settingsUI.SetActive(false);
    }
    public void ChangedSettigns()
    {
        QualitySettings.SetQualityLevel(dropDown.value);
        Setting setting = new Setting{quality = dropDown.value, volume = 0};
        string json = JsonUtility.ToJson(setting);
        File.WriteAllText(savePathQuality, json);
    }
    public void ResetProg()
    {
        CheckpointData data = new CheckpointData { CheckpointLevel = "Main", checkpointId = 0 };
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(savePath, json);
    }
    public void QuitButton()
    {
        Application.Quit();
    }
}
