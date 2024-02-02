using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TooltipManager : MonoBehaviour
{
    public GameObject tooltipPanel;
    public GameObject candle;

    public void ShowTooltip(bool colliding){

        Vector2 screenPosition = RectTransformUtility.WorldToScreenPoint(Camera.main, candle.transform.position);
        tooltipPanel.transform.position = screenPosition - new Vector2(0,50);
        tooltipPanel.SetActive(colliding);
    }
}
