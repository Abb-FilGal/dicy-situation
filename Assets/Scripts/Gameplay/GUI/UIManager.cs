using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Image pointsFill;
    public TextMeshProUGUI roundsText;
    public Image purchaseButton;
    public Sprite purchaseButtonDisabled;
    public Sprite purchaseButtonEnabled;
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
            if (pointsFill != null && GameManager.maxPoints != 0)
            {
                float fillAmount = (float)totalPoints / GameManager.maxPoints;
                pointsFill.fillAmount = Mathf.Min(fillAmount, 0.8f);

                Color lightBlue = new Color(0.5f, 0.5f, 1f);
                Color darkBlue = new Color(0f, 0f, 0.5f);

                pointsFill.color = Color.Lerp(lightBlue, darkBlue, fillAmount);
                if (totalPoints < GameManager.maxPoints)
                {
                    purchaseButton.sprite = purchaseButtonDisabled;
                    purchaseButton.GetComponent<Button>().interactable = false;
                }
                else
                {
                    purchaseButton.sprite = purchaseButtonEnabled;
                    purchaseButton.GetComponent<Button>().interactable = true;
                }
            }
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