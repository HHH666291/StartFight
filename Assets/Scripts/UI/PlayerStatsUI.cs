using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
public class PlayerStatsUI : MonoBehaviour
{
    [SerializeField] private CharacterStats playerStats;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI attackText;
    [SerializeField] private TextMeshProUGUI expText;
    // Start is called before the first frame update
    private void Start()
    {
        Refresh();

    }

    private void OnEnable()
    {
        playerStats.OnHealthChanged += HandleStatsChanged;
        playerStats.OnLevelUp += HandleStatsChanged;
        playerStats.OnExpChanged += HandleStatsChanged;
    }

    private void OnDisable()
    {
        playerStats.OnHealthChanged -= HandleStatsChanged;
        playerStats.OnLevelUp -= HandleStatsChanged;
        playerStats.OnExpChanged -= HandleStatsChanged;
    }

    private void HandleStatsChanged(CharacterStats stats)
    {
        Refresh();
    }

    private void Refresh()
    {
        levelText.text = "Level: " + playerStats.Level;
        healthText.text = "HP: " + playerStats.CurrentHealth + "/" + playerStats.MaxHealth;
        attackText.text = "Attack: " + playerStats.AttackPower;
        expText.text = "EXP: " + playerStats.CurrentExp + "/" + playerStats.ExpToNextLevel;
    }
}
