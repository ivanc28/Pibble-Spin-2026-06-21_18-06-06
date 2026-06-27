using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Slider healthBar;
    public Slider spinChargeBar;
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
    }

    private void UpdateHealthBar()
    {
        healthBar.value = Player.Instance.GetHealth() / Player.Instance.GetMaxHealth();
    }
    private void UpdateSpinBar()
    {
        spinChargeBar.value = Player.Instance.GetSpinCharge() / Player.Instance.GetMaxSpinCharge();
    }
}
