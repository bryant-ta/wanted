using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI levelText;
    public Image wantedImage;

    public Image startScreenImage;

    private void Start()
    {
        GameManager.Instance.Timer.TickEvent += UpdateTimerText;
    }

    void UpdateTimerText(float remainingTime)
    {
        timerText.text = Mathf.CeilToInt(GameManager.Instance.Timer.RemainingTimeSeconds).ToString();
    }

    public void UpdateLevelText(int level)
    {
        levelText.text = "$" + level.ToString();
    }

    public void UpdateLevelWanted(Sprite image)
    {
        wantedImage.sprite = image;
    }

    public void HideStartScreen()
    {
        startScreenImage.gameObject.SetActive(false);
    }
}