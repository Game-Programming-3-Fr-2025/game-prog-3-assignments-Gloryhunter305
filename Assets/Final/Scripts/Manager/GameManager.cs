using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int requiredKills = 10;
    private int currentKills = 0;

    public TextMeshProUGUI killCountText;
    public GameObject winScreen;

    [SerializeField] private GameObject player;

    private int _enemiesRemaining = 0;
    private HashSet<HealthManager> _subscribedEnemies = new HashSet<HealthManager>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        // rescan when a new scene is loaded
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Start()
    {
        if (winScreen != null)
        {
            winScreen.SetActive(false);
        }
        player = GameObject.FindGameObjectWithTag("Player");
        ScanForEnemies();
        UpdateKillsUI();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetGame();
        }

        //if (player == null)
        //{
        //    Time.timeScale = 0f; // Pause the game if player is null (dead)
        //}
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // clear known subscriptions and rescan for the new scene
        _subscribedEnemies.Clear();
        ScanForEnemies();
    }

    private void ScanForEnemies()
    {
        HealthManager[] enemies = FindObjectsOfType<HealthManager>();
        _enemiesRemaining = enemies.Length;

        foreach (HealthManager enemyHealth in enemies)
        {
            if (_subscribedEnemies.Add(enemyHealth))
            {
                enemyHealth.OnDeath += OnEnemyKilled;
            }
        }

        // if there are no enemies in the scene, immediately win
        if (_enemiesRemaining <= 0)
        {
            WinGame();
        }
    }

    public void RegisterKill()
    {
        currentKills++;
        //if (currentKills >= requiredKills)
        //{
        //    WinGame();
        //}
        UpdateKillsUI();
    }

    private void OnEnemyKilled(GameObject enemy)
    {
        // called when an enemy dies
        RegisterKill();

        _enemiesRemaining--;
        if (_enemiesRemaining <= 0)
        {
            WinGame();
        }
    }

    public void WinGame()
    {
        if (winScreen != null)
        {
            winScreen.SetActive(true);
        }
        Time.timeScale = 0f; // Pause the game
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        currentKills = 0;

        Time.timeScale = 1f; // Resume the game
    }

    private void UpdateKillsUI()
    {
        if (killCountText != null)
        {
            killCountText.text = "Kills: " + currentKills + "/" + requiredKills;
        }
    }
}
