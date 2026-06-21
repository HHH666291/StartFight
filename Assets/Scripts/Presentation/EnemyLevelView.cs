using TMPro;
using UnityEngine;

// 系统：表现（Presentation）
// 职责：显示敌人的等级。
// 依赖：CharacterStats、TMP_Text。
public class EnemyLevelView : MonoBehaviour
{
    [SerializeField] private CharacterStats characterStats;
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private string prefix = "";

    private void Awake()
    {
        if (characterStats == null)
        {
            characterStats =
                GetComponentInParent<CharacterStats>();
        }

        if (levelText == null)
        {
            levelText = GetComponent<TMP_Text>();
        }
    }

    private void Start()
    {
        Refresh();
    }

    public void Refresh()
    {
        if (characterStats == null ||
            levelText == null)
        {
            return;
        }

        levelText.text =
            $"{prefix}{characterStats.Level}";
    }
}