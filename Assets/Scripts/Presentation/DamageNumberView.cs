using TMPro;
using UnityEngine;

// System: Presentation
// Role: Provides a display endpoint for a damage number without calculating damage.
// Depends on: TextMeshProUGUI.
public class DamageNumberView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI damageText;

    public void ShowDamage(int amount)
    {
        if (damageText != null)
        {
            damageText.text = amount.ToString();
        }
    }
}
