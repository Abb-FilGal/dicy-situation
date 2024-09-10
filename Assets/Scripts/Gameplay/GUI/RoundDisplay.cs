using UnityEngine;
using TMPro;

public class RoundDisplay : MonoBehaviour
{
    public TextMeshProUGUI roundText; // Reference to the TextMeshProUGUI component
    private EnemySpawner enemySpawner;

    void Start()
    {
        // Find the EnemySpawner in the scene
        enemySpawner = FindObjectOfType<EnemySpawner>();
    }

    void Update()
    {
        if (enemySpawner != null)
        {
            // Update the UI text with the current round number
            roundText.text = "Round: " + enemySpawner.CurrentRoundNumber;
        }
    }
}