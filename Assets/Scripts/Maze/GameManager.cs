using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public MazeGenerator mg;
    public GameObject startPanel;
    public GameObject losePanel;
    public GameObject winPanel;
    public GameObject creditPanel;
    public GameObject manualPanel;
    public GameObject manualInfo;
    public Power power;
    public float batteryLife;
    public TMP_Text batteryLifeText;
    void Start()
    {
        batteryLife = 100.0f/power.drainSpeedWhenNotUsing;
    }
    public void GameStart(){
        mg.StartNext();
        startPanel.SetActive(false);
    }
    public void QuitGame(){
         Application.Quit();
    }
    public void ShowCredit(){
        creditPanel.SetActive(true);
    }
    public void HideCredit(){
        creditPanel.SetActive(false);
    }
    public void ShowManual(){
        manualPanel.SetActive(true);
        manualInfo.SetActive(true);
    }
    public void HideManual(){
        manualPanel.SetActive(false);
        manualInfo.SetActive(false);
    }
    public void NextLevel(){
        Time.timeScale = 1;
        mg.StartNext();
        winPanel.SetActive(false);
    }
    public void Retry(){
        Time.timeScale = 1;
        mg.StartNext();
        losePanel.SetActive(false);
    }
    public void GameLost(){
        Time.timeScale = 0;
        losePanel.SetActive(true);
    }
    public void GameWon(){
        Time.timeScale = 0;
        winPanel.SetActive(true);
        power.drainSpeedWhenNotUsing += 0.33f;
        batteryLife = 100.0f/power.drainSpeedWhenNotUsing;
        batteryLifeText.text = "Battery Life: " + (Mathf.RoundToInt(batteryLife*10)/10.0f).ToString()+"s";
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
