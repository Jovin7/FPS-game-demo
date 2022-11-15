using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
    [Header("HUD")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI ammoText;
    public Image healthFill;

    [Header("Pause Menu")]
    public GameObject pauseMenu;

    [Header("End Game Screen")]
    public GameObject endScreen;
    public TextMeshProUGUI endHeaderText;
    public TextMeshProUGUI endScoreText;

    public static GameUI instance;

    private void Awake()
    {
        instance = this;
    }

    public void UpdateHealthBar(int curHp,int maxHp)
    {
        healthFill.fillAmount = (float)curHp / (float)maxHp;
    }
    public void UpdateScoreText(int score )
    {
        scoreText.text = "Score: " + score;
    }
    public void UpdateAmmoText(int curAmmo,int maxAmmo)
    {
        ammoText.text = "Ammo: " + curAmmo + "/"+ maxAmmo;
    }
    public void TogglePauseMenu (bool paused)
    {
        pauseMenu.SetActive(paused);
    }
    public void SetEndGameScreen(bool win,int score)
    {
        endScreen.SetActive(true);
        endHeaderText.text = win == true ? "You Win" : "You Lose";
        endHeaderText.color = win == true ?Color.green: Color.red;
        endScoreText.text = "<b>Score: </b>\n" + score;
    }

    public void OnResumeButton()
    {
        GameManager.instance.TogglePauseGame();
    }
    public void OnRestartButton()
    {
        SceneManager.LoadScene("Game");
    }
    public void OnMenuButton()
    {
        SceneManager.LoadScene("Menu");
    }
}
