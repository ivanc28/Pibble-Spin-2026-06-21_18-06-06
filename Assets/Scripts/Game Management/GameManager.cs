using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool IsPaused { get; private set; }
    private float gameTime;
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
    }
}
