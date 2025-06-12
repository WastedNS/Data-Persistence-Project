using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HighscoreManager : MonoBehaviour
{
	public TMP_InputField inputField;

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
		//var inputName = inputField.text;
		// Get the input name from the InputField in the Canvas
		//InputField inputField = GameObject.Find("InputField").GetComponent<InputField>();
		//InputField inputField = GameObject.Find("Canvas").GetComponent<InputField>();
		string inputName = inputField.text;
		// Check if the input name is empty
		if (string.IsNullOrEmpty(inputName))
		{
			Debug.LogWarning("Input name is empty. Please enter a valid name.");
			return;
		}

		// Save the high score data using MainManager
		MainManager.Instance.SaveData(inputName);

        // Load scene 0
        SceneManager.LoadScene(0);
		MainManager.Instance.Init();
    }
}
