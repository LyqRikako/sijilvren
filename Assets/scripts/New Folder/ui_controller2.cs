using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class ui_controller2 : MonoBehaviour
{
    public static ui_controller2 instance;
    public void Awake()
    {
        instance = this; 
    }
    public Slider expLvSlider;
    public TMP_Text expLvText;
    public levelup_selection_button2[] levelUpButtons;
    public GameObject levelUpPanel;
    public TMP_Text coinText;
    public playerstatupgradedisplay2 moveSpeedUpgradeDisplay, healthUpgradeDisplay, pickupRangeUpgradeDisplay, maxWeaponsUpgradeDisplay;
    public TMP_Text timeText;
    public GameObject levelEndScreen;
    public TMP_Text endTimeText;
    public string mainMenuName;
    public GameObject pauseScreen;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
           //pauseUnpause();
        }
    }

    public void UpdateExperience(int currentExp,int levelExperience,int currentLevel)
    {
        expLvSlider.maxValue = levelExperience;
        expLvSlider.value = currentExp;
        expLvText.text = "Level: " + currentLevel;
    }

    public void SkipLevelUp()
    {
        levelUpPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void UpdateCoins()
    {
        coinText.text = "Coins:" + coin_controller2.instance.currentCoins;
    }
    public void UpdateTimer(float time)
    {
        float minutes = Mathf.FloorToInt( time / 60f);
        float seconds = Mathf.FloorToInt( time % 60);

        timeText.text = "Time: " + minutes + ":" + seconds.ToString("00"); 
    }
    public void PurchaseMoveSpeed()
    {
        playstatcontroller2.instance.PurchaseMoveSpeed();
        SkipLevelUp();
    }
    public void PurchaseHealth()
    {
        playstatcontroller2.instance.PurchaseHealth();
        SkipLevelUp();
    }
    public void PurchasePickupRange()
    {
        playstatcontroller2.instance.PurchasePickupRange();
        SkipLevelUp();
    }
    public void PurchaseMaxWeapons()
    {
        playstatcontroller2.instance.PurchaseMaxWeapons();
        SkipLevelUp();
    }
    public void GoToMainMenu()
    {
        SceneManager.LoadScene(mainMenuName);
    }
    public void GoToRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void QuitGamet()
    {
       Application.Quit();
    }
    public void PauseUnpause()
    { 
    
    }
}
