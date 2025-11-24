using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

#if UNITY_EDITOR
using UnityEditor;
#endif

[System.Serializable]
public struct SaveRecord
{
    public int score;
    public string name;

    public override readonly string ToString() => $"{name} : {score}";
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public List<SaveRecord> highScores;
    public const string NamePlayerPrefsKey = "Player";

    void Awake()
    {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        highScores = new();
        Debug.Log("Calling loadscores()");
        LoadScores();
    }

    [System.Serializable]
    public class SaveData
    {
        public List<SaveRecord> saveScores;
    }

    public void SaveScores() 
    {
        SaveData data = new SaveData();
        data.saveScores = highScores;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile1.json", json);
        Debug.Log("File saved to " + Application.persistentDataPath + "/savefile1.json");
    }

    public void LoadScores()
    {
        string path = Application.persistentDataPath + "/savefile1.json";
        Debug.Log("Checking for savefile at " + path);

        if (File.Exists(path)) {
            Debug.Log("Load File Exists");
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            highScores = data.saveScores;
        } else
        {
            Debug.Log("No load file, padding with 0/EMPTY");
            for (int i = 0; i < 10; i++)
            {
                highScores.Add(new SaveRecord{score = 0, name = "EMPTY"});
            }
        }
    }

    public void UpdateScores(int newScore)
    {
        string currentName = PlayerPrefs.GetString(NamePlayerPrefsKey);
        Debug.Log("Appending " + currentName + " : " + newScore.ToString());
        highScores.Add(new SaveRecord{score = newScore, name = currentName});
        highScores.Sort((x, y) => y.score.CompareTo(x.score));

        if (highScores.Count > 10)
        {
            int toRemove = highScores.Count - 10;
            highScores.RemoveRange(10, toRemove);
        }
        SaveScores();
    }

    public void SaveName(string inputName)
    {
        PlayerPrefs.SetString(NamePlayerPrefsKey, inputName);
        PlayerPrefs.Save();
        Debug.Log("Name saved: " + inputName);
    }

}
