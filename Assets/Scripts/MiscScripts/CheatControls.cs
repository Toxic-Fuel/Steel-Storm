using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class CheatControls : MonoBehaviour
{
    private string savePath;
    // Start is called before the first frame update
    void Start()
    {
        savePath = Path.Combine(Application.persistentDataPath, "checkpoint.json");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            SceneManager.LoadScene("BossBattle");
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            CheckpointData data = new CheckpointData { CheckpointLevel = "Main", checkpointId = 0 };
            string json = JsonUtility.ToJson(data);
            File.WriteAllText(savePath, json);
            SceneManager.LoadScene("Main");
        }
    }
}
