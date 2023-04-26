using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public Text PlayerNameAndHighScoreText;
    public GameObject GameOverText;
    
    private bool m_Started = false;
    private float m_Points;
    private string m_currentPlayerName = PlayerNameAndHighScore.Instance.currentPlayerName;
    private bool m_GameOver = false;
    public static float ScoreMultiplier = 1.0f;
    public static float DefaultScoreMultiplier = 1.0f;
    void Awake(){
        LoadHighScoreAndPlayer();
    }
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
        SetPlayerNameAndHighScore();
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
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
        m_Points += (point*ScoreMultiplier);
        ScoreText.text = $"Score : {(int)m_Points}";
    }

    void SetPlayerNameAndHighScore()
    {
        PlayerNameAndHighScoreText.text = $"Best Score : {PlayerNameAndHighScore.Instance.playerHighScore} Name : {PlayerNameAndHighScore.Instance.bestPlayerName}";
    }
    public void GameOver()
    {
        if (m_Points > PlayerNameAndHighScore.Instance.playerHighScore)
        {
            SaveHighScoreAndPlayer();
        }
        m_GameOver = true;
        GameOverText.SetActive(true);
    }

    [System.Serializable]
    class SaveHighScore
    {
        public string PlayerName;
        public int PlayerScore;
    }

    public void SaveHighScoreAndPlayer()
    {
        SaveHighScore data = new SaveHighScore();
        data.PlayerName = m_currentPlayerName;
        data.PlayerScore = (int)m_Points;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/highscore.json", json);        
    }

    public void LoadHighScoreAndPlayer()
    {
        string path = Application.persistentDataPath + "/highscore.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveHighScore data = JsonUtility.FromJson<SaveHighScore>(json);
            PlayerNameAndHighScore.Instance.bestPlayerName = data.PlayerName;
            PlayerNameAndHighScore.Instance.playerHighScore = data.PlayerScore;

        }
    }
}
