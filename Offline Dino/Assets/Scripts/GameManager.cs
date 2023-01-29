using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set;}
    public float initialGameSpeed = 5f;
    public float gameSpeedIncrease = 0.1f;
    public float gameSpeed { get; private set;}

    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    public Button retryButton;

    private Player player;
    private Spawner spawner;

    private float score;
    private void Awake(){
        if(Instance == null)
            Instance = this;
        else
            DestroyImmediate(gameObject);
    }

    private void OnDestroy(){
        if(Instance == this)
            Instance = null;
    }

    private void Start(){
        player = FindObjectOfType<Player>();
        spawner = FindObjectOfType<Spawner>();
        NewGame();
    }

    public void NewGame(){
        
        Obstacles[] obstacles = FindObjectsOfType<Obstacles>();
        foreach(var obstacle in obstacles){
            Destroy(obstacle.gameObject);
        }

        gameSpeed = initialGameSpeed;
        score = 0f;
        enabled = true;

        player.gameObject.SetActive(true);
        spawner.gameObject.SetActive(true);

        gameOverText.gameObject.SetActive(false);
        retryButton.gameObject.SetActive(false);

        UpdateHighScore();
    }

    private void Update(){
        gameSpeed += gameSpeedIncrease * Time.deltaTime;
        score += gameSpeed * Time.deltaTime;
        scoreText.text = Mathf.FloorToInt(score).ToString("D5");
    }

    public void GameOver(){
        gameSpeed = 0f;
        enabled = false;

        player.gameObject.SetActive(false);
        spawner.gameObject.SetActive(false);

        gameOverText.gameObject.SetActive(true);
        retryButton.gameObject.SetActive(true);

        UpdateHighScore();
    }

    private void UpdateHighScore(){
        float hiscore = PlayerPrefs.GetFloat("hiscore", 0);
        
        if(score > hiscore){
            hiscore = score;
            PlayerPrefs.SetFloat("hiscore", hiscore);
        }

        highScoreText.text = Mathf.FloorToInt(hiscore).ToString("D5");
    }
}
