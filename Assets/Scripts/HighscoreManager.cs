using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HighscoreManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveHighscore()
    { 
        var inputName = gameObject.GetComponent<InputField>().text;
        MainManager.Instance.SaveData(inputName);

        // Load scene 0
        SceneManager.LoadScene(0);
    }
}
