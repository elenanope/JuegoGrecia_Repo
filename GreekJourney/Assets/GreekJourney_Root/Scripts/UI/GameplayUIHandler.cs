using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameplayUIHandler : MonoBehaviour
{
    [Header("UI References")]
    public int pickUps;
    [SerializeField] int pickUpsTotales;
    [SerializeField] TMP_Text pointsText;
    [SerializeField] Image lifeBarFill;

    // Update is called once per frame
    void Update()
    {
        PointsLifeUpdater();
    }

    void PointsLifeUpdater()
    {
        pointsText.text = pickUps+ " / " + pickUpsTotales;
        lifeBarFill.fillAmount = GameManager.Instance.totalLife;
    }
}
