using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
//using UnityEditor.Overlays;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public Text HighScoreText;
    public GameObject GameOverText;
    
    private bool m_Started = false;
    private int m_Points;
    private int m_HighScore;
    
    private bool m_GameOver = false;

    const string savefileName = "savefile.json";

    // Start is called before the first frame update
    void Start()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }

        LoadData();
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = UnityEngine.Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";

        if (m_Points > m_HighScore)
        {
            m_HighScore = m_Points;
            HighScoreText.text = $"High Score: {m_HighScore}";
            SaveData();
        }
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);
    }

    public void SaveData()
    {
        SaveDataVm data = new()
        {
            HighScore = m_HighScore,
            HighScoreText = HighScoreText.text
        };

        string json = JsonUtility.ToJson(data, true);

        File.WriteAllText(Application.persistentDataPath + "/" + savefileName, json);
    }

    public void LoadData()
    {
        string path = Application.persistentDataPath + "/" + savefileName;
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveDataVm data = JsonUtility.FromJson<SaveDataVm>(json);

            m_HighScore = data.HighScore;
            HighScoreText.text = data.HighScoreText;
        }
    }

    [Serializable]
    class SaveDataVm
    {
        public int HighScore;
        public string HighScoreText;
    }
}
