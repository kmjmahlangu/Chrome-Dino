using TMPro;
using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public float initialGameSpeed = 5f;
    public float gameSpeedIncrease = 0.1f;
    public float gameSpeed { get; private set; }

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI hiscoreText;
    [SerializeField] private TextMeshProUGUI gameOverText;
    [SerializeField] private Button retryButton;

    private Character player;
    private Spawner spawner;

    private float score;
    public float Score => score;
    private float scoreMultiplier = 1f;
private float scoreMultiplierTimer = 0f;

public bool HasShield { get; private set; }

    private void Awake()
    {
        if (Instance != null) {
            DestroyImmediate(gameObject);
        } else {
            Instance = this;
        }
    }

    private void OnDestroy()
    {
        if (Instance == this) {
            Instance = null;
        }
    }

    private void Start()
    {
        player = FindObjectOfType<Character>();
        spawner = FindObjectOfType<Spawner>();

        NewGame();
    }

    public void NewGame()
{
    Obstacle[] obstacles = FindObjectsOfType<Obstacle>();

    foreach (var obstacle in obstacles) {
        Destroy(obstacle.gameObject);
    }

    score = 0f;
    gameSpeed = initialGameSpeed;

    scoreMultiplier = 1f;
    scoreMultiplierTimer = 0f;
    HasShield = false;

    enabled = true;

    player.gameObject.SetActive(true);
    spawner.gameObject.SetActive(true);
    gameOverText.gameObject.SetActive(false);
    retryButton.gameObject.SetActive(false);

    UpdateHiscore();
}

    public void GameOver()
    {
        gameSpeed = 0f;
        enabled = false;

        player.gameObject.SetActive(false);
        spawner.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(true);
        retryButton.gameObject.SetActive(true);

        UpdateHiscore();
    }

    private void Update()
{
    gameSpeed += gameSpeedIncrease * Time.deltaTime;

    score += gameSpeed * scoreMultiplier * Time.deltaTime;

    if (scoreMultiplierTimer > 0f)
    {
        scoreMultiplierTimer -= Time.deltaTime;

        if (scoreMultiplierTimer <= 0f)
        {
            scoreMultiplier = 1f;
        }
    }

    scoreText.text = Mathf.FloorToInt(score).ToString("D5");
}

    private void UpdateHiscore()
    {
        float hiscore = PlayerPrefs.GetFloat("hiscore", 0);

        if (score > hiscore)
        {
            hiscore = score;
            PlayerPrefs.SetFloat("hiscore", hiscore);
        }

        hiscoreText.text = Mathf.FloorToInt(hiscore).ToString("D5");
    }
    public void ActivateDoubleScore(float duration)
{
    scoreMultiplier = 2f;
    scoreMultiplierTimer = duration;

    Debug.Log("DOUBLE SCORE ACTIVATED!");
}

public void ActivateShield()
{
    HasShield = true;
}

public bool UseShield()
{
    if (!HasShield)
        return false;

    HasShield = false;
    return true;
}
}

