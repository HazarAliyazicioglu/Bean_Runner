using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public int score;
    public int highscore;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highscoreText;
    public TextMeshProUGUI health;
    public TextMeshProUGUI coin;
    public TextMeshProUGUI coinMeter;
    public GameObject PauseMenu;
    public GameObject RestartMenu;
    public GameObject MainMenu;
    public GameObject InGameMenu;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("HighScore"))
        {
            highscore = PlayerPrefs.GetInt("HighScore");
        }   
        InGameMenu.SetActive(false);
        if (PauseMenu.activeInHierarchy)
        {
            PauseMenu.SetActive(false);
        }
        if (RestartMenu.activeInHierarchy)
        {
            RestartMenu.SetActive(false);
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        Score();
        Health();
        Coin();
    }
    private void Score()
    {
        
        score = Mathf.RoundToInt(Time.timeSinceLevelLoad * PlayerScript.speed * 500);
        if (score > highscore)
        {
            highscore = score;
            PlayerPrefs.SetInt("HighScore",highscore);
        }
        scoreText.text = "Score: " + score.ToString();
        highscoreText.text = "High Score: " + highscore.ToString();
    }
    private void Health()
    {
        health.text = "Health: " + PlayerScript.hearts;
    }
    public void Coin()
    {
        coin.text = "Coins: " + PlayerScript.collectedCoins;
        coinMeter.text = PlayerScript.coinMeter.ToString();
    }
    public void PauseButton()
    {
        PauseMenu.SetActive(true);
        Time.timeScale = 0f;
        player.SetActive(false);
    }
    public void ResumeButton()
    {
        Time.timeScale = 1;
        PauseMenu.SetActive(false);
        player.SetActive(true);
    }
    public void QuitButton()
    {
        Application.Quit();
    }
    public void RestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
        PlayerScript.restarted = true;
        
    }
    public void StartButton()
    {
        InGameMenu.SetActive(true);
        MainMenu.SetActive(false);
        Time.timeScale = 1;
        player.SetActive(true);
    }
    
}
