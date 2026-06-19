using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 系统：表现（Presentation）
// 职责：根据游戏状态显示失败界面，并提供重新开始入口。
// 依赖：GameManager、GameState。

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;

    private void Awake()
    {
        if (gameOverPanel != null) { gameOverPanel.SetActive(false); }


    }
    private void OnEnable()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnStateChanged += HandleGameStateChanged;
        }
    }
    private void OnDisable()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnStateChanged -= HandleGameStateChanged;
        }
    }

    private void HandleGameStateChanged(GameState state)
    {
        if (gameOverPanel != null) { gameOverPanel.SetActive(state == GameState.GameOver); }
    }
    public void RestartGame()
    {
        if (GameManager.Instance != null) { GameManager.Instance.RestartGame(); }
    }
}
