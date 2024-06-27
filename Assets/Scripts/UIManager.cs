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
    public TextMeshProUGUI highScoreText;
    public Image startScreenGuyImage;
    public Sprite startScreenGuySprite;

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

    public void ToggleStartScreen(bool enabled)
    {
        startScreenImage.gameObject.SetActive(enabled);
    }
    public void ToggleHighScoreText(bool enabled)
    {
        highScoreText.gameObject.SetActive(enabled);
    }
    public void UpdateHighScoreText(int level)
    {
        highScoreText.text = "$" + level.ToString();
    }
    public void ToggleStartScreenGuyImage(bool enabled)
    {
        startScreenGuyImage.gameObject.SetActive(enabled);
    }
    public void UpdateStartScreenGuyImage() {
        startScreenGuyImage.sprite = startScreenGuySprite;
    }
}