using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// 系统：游戏流程（Game）
// 职责：保存全局游戏状态，并提供暂停、继续、结束和重新开始命令。
// 依赖：GameState、Time、SceneManager。
// 扩展：全局流程状态和场景级命令放在这里；角色、战斗、UI 具体逻辑不要放入。
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameState CurrentState { get; private set; } = GameState.Playing;
    public bool IsPlaying => CurrentState == GameState.Playing;

    public event Action<GameState> OnStateChanged;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void PauseGame()
    {
        CurrentState = GameState.Paused;
        Time.timeScale = 0f;
        OnStateChanged?.Invoke(CurrentState);

    }

    public void ResumeGame()
    {
        CurrentState = GameState.Playing;
        Time.timeScale = 1f;
        OnStateChanged?.Invoke(CurrentState);

    }

    public void GameOver()
    {
        CurrentState = GameState.GameOver;
        Time.timeScale = 0f;
        OnStateChanged?.Invoke(CurrentState);

    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        CurrentState = GameState.Playing;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    private void SetState(GameState newState)
    {
        if (CurrentState == newState) return;
        CurrentState = newState;
        OnStateChanged?.Invoke(CurrentState);
    }
}
