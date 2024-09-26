using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private int totalPoints = 100;
    public const int maxPoints = 100;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddPoints(int points)
    {
        totalPoints += points;
        if (totalPoints > maxPoints)
        {
            totalPoints = maxPoints;
        }
        //Debug.Log($"Points added: {points}, Total points: {totalPoints}");
    }

    public int GetTotalPoints()
    {
        return totalPoints;
    }
    public bool SpendPoints(int points)
    {
        if (totalPoints >= points)
        {
            totalPoints -= points;
            return true;
        }
        return false;
    }

    public void PurchaseTower()
    {
        // This may need to be redone
        SpendPoints(100);
    }
}