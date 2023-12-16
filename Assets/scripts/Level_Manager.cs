using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_Manager : MonoBehaviour
{
    public static Level_Manager instance;

    private void Awake()
    {
        instance = this;
    }
    public bool gameActive;
    public float timer;
    public float waitToShowEndScreen = 1f;
    void Start()
    {
        gameActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameActive == true)
        {
            timer += Time.deltaTime;
            ui_controller.instance.UpdateTimer(timer);
        }
    }
    public void Endlevel()
    {
        gameActive = false;

        StartCoroutine(EndLevelCo());
    }
    IEnumerator EndLevelCo()
    {
        yield return new WaitForSeconds(waitToShowEndScreen);

        float minutes = Mathf.FloorToInt(timer / 60);
        float seconds = Mathf.FloorToInt(timer % 60);

        ui_controller.instance.endTimeText.text = minutes.ToString() + "mins" + seconds.ToString("00" + " secs");
        ui_controller.instance.levelEndScreen.SetActive(true);
    }
}