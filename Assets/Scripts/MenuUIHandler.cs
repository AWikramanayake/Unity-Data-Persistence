using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

#if UNITY_EDITOR
using UnityEditor;
#endif

[DefaultExecutionOrder(1000)]
public class MenuUIHandler : MonoBehaviour
{
    public TMP_InputField nameInputFieldTMP;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (PlayerPrefs.HasKey(GameManager.NamePlayerPrefsKey))
        {
            nameInputFieldTMP.text = PlayerPrefs.GetString(GameManager.NamePlayerPrefsKey);
        }
    }

    public void StartNew()
    {
        GameManager.Instance.SaveName(nameInputFieldTMP.text);
        SceneManager.LoadScene(1);
    }

    public void OpenHS()
    {
        SceneManager.LoadScene(2);
    }


    public void Exit() {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
