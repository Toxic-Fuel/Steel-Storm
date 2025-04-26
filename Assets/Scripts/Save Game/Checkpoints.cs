using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class CheckpointData
{
    public string CheckpointLevel;
    public int checkpointId;
}

public class Checkpoints : MonoBehaviour
{
    public static Checkpoints Instance;
    public Transform[] checkpointPositions;

    private string savePath;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        savePath = Path.Combine(Application.persistentDataPath, "checkpoint.json");
        LoadCheckpoint();
    }

    public void SaveCheckpoint(int id)
    {
        CheckpointData data = new CheckpointData
        {
            CheckpointLevel = SceneManager.GetActiveScene().name,
            checkpointId = id,
        };
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(savePath, json);
    }

    public void LoadCheckpoint()
    {
        if (!File.Exists(savePath))
            return;

        string json = File.ReadAllText(savePath);
        CheckpointData data = JsonUtility.FromJson<CheckpointData>(json);

        if (data.checkpointId >= 0 && data.checkpointId < checkpointPositions.Length)
        {
            Debug.Log(data.checkpointId);
            Transform player = GameObject.FindGameObjectWithTag("Player").transform;
            player.position = checkpointPositions[data.checkpointId].position;
        }
    }

    private void OnApplicationQuit()
    {
        if (File.Exists(savePath))
        {
            File.Delete(savePath);
            Debug.Log("Checkpoint file deleted");
        }
    }
}
