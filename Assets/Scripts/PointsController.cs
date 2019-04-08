using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointsController : MonoBehaviour
{
    public HealthManager playerHealth;
    public HealthManager enemyHealth;
    public Text playerPoints;
    public Text enemyPoints;
    int playerP=0;
    int enemyP=0;

    void Start()
    {
        playerHealth.CriticalDamageTaken += IncreaceEnemyPoints;
        enemyHealth.CriticalDamageTaken += IncreacePlayerPoints;
    }

    void IncreaceEnemyPoints()
    {
        enemyP++;
        enemyPoints.text = enemyP.ToString();
    }
    void IncreacePlayerPoints()
    {
        playerP++;
        playerPoints.text = playerP.ToString();
    }
}
