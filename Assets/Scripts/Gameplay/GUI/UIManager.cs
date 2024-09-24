using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI pointsText;
    public TextMeshProUGUI roundsText;
    private EnemySpawner enemySpawner;

    void Start()
    {
        enemySpawner = FindObjectOfType<EnemySpawner>();
    }

    void Update()
    {
        UpdatePointsDisplay();
        UpdateRoundDisplay();
    }

    void UpdatePointsDisplay()
    {
        if (GameManager.instance != null)
        {
            int totalPoints = GameManager.instance.GetTotalPoints();
            pointsText.text = "Points: " + totalPoints;
        }
    }

    void UpdateRoundDisplay()
    {
        if (enemySpawner != null)
        {
            roundsText.text = "Round: " + enemySpawner.CurrentRoundNumber;
        }
    }
    public void PurchaseItem(int cost)
    {
        if (GameManager.instance != null)
        {
            if (GameManager.instance.SpendPoints(cost))
            {
                //Debug.Log("Item purchased for " + cost + " points.");
                UpdatePointsDisplay();
            }
            else
            {
                Debug.LogWarning("Not enough points to purchase item.");
            }
        }
    }
}