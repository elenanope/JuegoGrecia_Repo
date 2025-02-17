using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameplayUIHandler : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] TMP_Text pointsText;
    [SerializeField] Image lifeBarFill;

    // Update is called once per frame
    void Update()
    {
        PointsLifeUpdater();
    }

    void PointsLifeUpdater()
    {
        pointsText.text = "X: " + GameManager.Instance.points.ToString();
        lifeBarFill.fillAmount = GameManager.Instance.totalLife;
    }
}
