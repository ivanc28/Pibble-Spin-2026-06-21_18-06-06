using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool IsPaused { get; private set; }
    private float gameTime;

    public static GameManager Instance { get; private set; }
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
        if (!IsPaused)
        {
            gameTime += Time.deltaTime;
        }
    }

    public void SetGamePaused(bool paused)
    {
        IsPaused = paused;
        Time.timeScale = paused ? 0 : 1;
        if (paused)
        {
            Player.Instance.input.DeactivateInput();
        }
        else
        {
            Player.Instance.input.ActivateInput();
        }
    }

    public float GetGameTime()
    {
        return gameTime;
    }
}
