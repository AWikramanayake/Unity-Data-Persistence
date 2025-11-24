using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class HSHandler : MonoBehaviour
{
    public TMP_Text col1;
    public TMP_Text col2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        col1.text = "";
        col2.text = "";
        if (GameManager.Instance.highScores != null) {
            Debug.Log("HS Loaded, not null");
            for (int i = 0; i < 5; i++)
            {
                col1.text += GameManager.Instance.highScores[i].ToString() + "\n";
                col2.text += GameManager.Instance.highScores[i + 5].ToString() + "\n";
            }
        } else
        {
            Debug.Log("HS list is null");
        }
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
