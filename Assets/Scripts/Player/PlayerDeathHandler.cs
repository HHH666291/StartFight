using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 系统：玩家（Player）
// 职责：监听玩家死亡，并通知游戏流程进入 GameOver。
// 依赖：CharacterDeath、GameManager。
public class PlayerDeathHandler : MonoBehaviour
{
    [SerializeField] private CharacterDeath characterDeath;

    private void Awake()
    {
        if (characterDeath == null)
        {
            characterDeath = gameObject.GetComponent<CharacterDeath>();
        }
    }

    private void OnEnable()
    {
        if (characterDeath != null)
        {
            characterDeath.OnDeath += HandlePlayerDeath;
        }
    }
    private void OnDisable()
    {
        if (characterDeath != null)
        {
            characterDeath.OnDeath -= HandlePlayerDeath;
        }
    }


    private void HandlePlayerDeath(CharacterDeath death)
    {
        if (GameManager.Instance != null) { GameManager.Instance.GameOver(); }
    }

}
