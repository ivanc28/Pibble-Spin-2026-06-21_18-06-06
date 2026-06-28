using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Slider healthBar;
    public Slider spinChargeBar;
    public TextMeshProUGUI currencyText;
    public TextMeshProUGUI clockText;
    public GameObject deathScreen;
    public TextMeshProUGUI scoreText;
    public GameObject tutorialCanvas;
    public GameObject pauseCanvas;
    private bool tutorialHidden = false;
    public Color winColorClockText;
    public TextMeshProUGUI endText;
    [TextArea(2, 4)]
    public string loseMessage;
    [TextArea(2, 4)]
    public string winMessage;
    public static UIManager Instance;
    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameManager.Instance.SetGamePaused(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (!tutorialHidden && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            StartCoroutine(StartGame());
            tutorialHidden = true;
        }
        if (tutorialHidden)
        {
            if (Keyboard.current.pKey.wasPressedThisFrame || Keyboard.current.escapeKey.wasPressedThisFrame)
            {
                if (!GameManager.Instance.IsPaused)
                {
                    PauseGame();
                }
                else
                {
                    ResumeGame();
                }
            }
        }
        UpdateHealthBar();
        UpdateSpinBar();
        UpdateCurrency();
        UpdateGameClock();
    }

    private void UpdateHealthBar()
    {
        healthBar.value = Player.Instance.GetHealth() / Player.Instance.GetMaxHealth();
    }
    private void UpdateSpinBar()
    {
        spinChargeBar.value = Player.Instance.GetSpinCharge() / Player.Instance.GetMaxSpinCharge();
    }
    private void UpdateCurrency()
    {
        currencyText.text = Player.Instance.inventory.GetCurrency().ToString();
    }
    private void UpdateGameClock()
    {
        clockText.text = Mathf.FloorToInt(GameManager.Instance.GetGameTime()).ToString("0");
        if (GameManager.Instance.HasWon)
        {
            clockText.color = winColorClockText;
        }
    }
    public void UpdateEndMessage()
    {
        if (GameManager.Instance.HasWon)
        {
            endText.text = winMessage;
        }
        else
        {
            endText.text = loseMessage;
        }
    }
    public void ShowDeathScreen()
    {
        scoreText.text = $"You lasted {Mathf.FloorToInt(GameManager.Instance.GetGameTime())} seconds!";
        deathScreen.SetActive(true);
    }
    public void ResetButton()
    {
        GameManager.Instance.SetGamePaused(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    private IEnumerator StartGame()
    {
        yield return null;  
        tutorialCanvas.SetActive(false);
        GameManager.Instance.SetGamePaused(false);
    }
    public void PauseGame()
    {
        GameManager.Instance.SetGamePaused(true);
        pauseCanvas.SetActive(true);
    }
    public void ResumeGame()
    {
        GameManager.Instance.SetGamePaused(false);
        pauseCanvas.SetActive(false);
    }
}
