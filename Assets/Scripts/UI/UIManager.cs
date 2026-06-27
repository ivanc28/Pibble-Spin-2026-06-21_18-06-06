using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Slider healthBar;
    public Slider spinChargeBar;
    public TextMeshProUGUI currencyText;
    public TextMeshProUGUI clockText;

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
        
    }

    // Update is called once per frame
    void Update()
    {
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
}
