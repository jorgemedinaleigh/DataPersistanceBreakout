using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public Text nameText;
    public GameObject GameOverText;
    public Text highScoreText;
    private string highScoreString;
    private DataManager dataManager = DataManager.dataManager;
    
    private bool m_Started = false;
    private int m_Points;
    private int initialHighScore;
    private int newHighscore;
    
    private bool m_GameOver = false;
    private string playername;
    private string previousScoreholder;
    private bool highScoreSet = false;
   


    
    // Start is called before the first frame update
    void Start()
    {
        // get name and highscore
        // DisplayName(dataManager.GetName());
        playername = dataManager.GetName();
        DisplayName(playername);
        LoadScoreInfo();
        highScoreText.text = $"Best Score: {previousScoreholder} : {initialHighScore}";
        highScoreString = highScoreText.text;
        initialHighScore = GetHighScore(highScoreString);

        // set up the rest of the gaem
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

    void DisplayName(string name)
    {
        nameText.text = $"Name: {name}";

    }

    int GetHighScore(string highScoreString)
    {
        
        Debug.Log(highScoreString);
        char[] separator = { ':' };
        string[] splitString = highScoreString.Split(separator);
        Debug.Log(splitString[splitString.Length - 1]);
        return int.Parse((splitString[splitString.Length - 1]));


    }
    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
        

        // update highscore if necessary
        if (m_Points > initialHighScore)
        {
            Debug.Log($"{m_Points} > {initialHighScore}");
            UpdateHighscore(m_Points);
            newHighscore = m_Points;
        }

    }

    void UpdateHighscore(int currentScore)
    {
        highScoreText.text = $"Best Score: {playername} Score: {currentScore}";
        highScoreSet = true;
    }

    [System.Serializable]
    class SaveData
    {
        public int score;
        public string playername;
    }

    public void SaveScoreInfo()
    {
        SaveData data = new SaveData();
        data.score = newHighscore;
        data.playername = playername;
        Debug.Log($"Score saved: {newHighscore} acheived by {playername}");
        string savedJson = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", savedJson);
    }

    public void LoadScoreInfo()
    {
        // specify a path, then read using file IO and assign that to string,
        // use that string as a param to create a SaveData object
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            //
            string jsonToLoad = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(jsonToLoad);
            initialHighScore = data.score;
            previousScoreholder = data.playername;
            Debug.Log($"Score loaded: {initialHighScore} achieved by {previousScoreholder}");
        }
        else
        {
            Debug.Log("No file found!");
        }
    }

    public void GameOver()
    {
        
        m_GameOver = true;
        GameOverText.SetActive(true);
        if (highScoreSet)
        {
            SaveScoreInfo();
        }
        

    }
}
