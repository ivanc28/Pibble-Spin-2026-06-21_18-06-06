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
    private bool tutorialHidden = false;
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
        clockText.text = GameManager.Instance.GetGameTime().ToString("0");
    }
    public void ShowDeathScreen()
    {
        scoreText.text = "Score:\n" + GameManager.Instance.GetGameTime().ToString("0");
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
}
