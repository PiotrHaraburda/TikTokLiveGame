using System;
using System.Collections;
using System.Collections.Generic;
using TikTokLiveUnity.Example;
using TMPro;
using UnityEngine;

public class RestartHandler : MonoBehaviour
{
    public GameObject restartPane;
    public TextMeshProUGUI teamText;
    public TextMeshProUGUI timerText;
    public GameObject giftPane;
    public GameObject giftSpawnPane1;
    public GameObject giftSpawnPane2;
    public GameObject giftSpawnPane3;
    public GameObject giftSpawnPane4;
    public GameObject Team1Target1;
    public GameObject Team1Target2;
    public GameObject Team1Target3;
    public GameObject Team2Target1;
    public GameObject Team2Target2;
    public GameObject Team2Target3;
    public TextMeshProUGUI Team1Score;
    public TextMeshProUGUI Team2Score;
    
    
    private bool restartEnded=true;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        if (Team1Spawner.target3Destroyed && Team1Spawner.target2Destroyed && Team1Spawner.target1Destroyed&&restartEnded)
        {
            restartEnded = false;
            restartPane.SetActive(true);
            giftPane.SetActive(false);
            giftSpawnPane1.SetActive(false);
            giftSpawnPane2.SetActive(false);
            giftSpawnPane3.SetActive(false);
            giftSpawnPane4.SetActive(false);
            teamText.SetText("DZIEWCZYNY");
            
            String team1ScoreText = Team1Score.text;
            int team1ScoreTextInt = Int32.Parse(team1ScoreText);
            Team1Score.SetText((team1ScoreTextInt+1).ToString());
            
            StartCoroutine(timer());
        }
        else if (Team2Spawner.target3Destroyed && Team2Spawner.target2Destroyed && Team2Spawner.target1Destroyed&&restartEnded)
        {
            restartEnded = false;
            restartPane.SetActive(true);
            giftPane.SetActive(false);
            giftSpawnPane1.SetActive(false);
            giftSpawnPane2.SetActive(false);
            giftSpawnPane3.SetActive(false);
            giftSpawnPane4.SetActive(false);
            teamText.SetText("CHŁOPAKI");
            
            String team2ScoreText = Team2Score.text;
            int team2ScoreTextInt = Int32.Parse(team2ScoreText);
            Team2Score.SetText((team2ScoreTextInt+1).ToString());
            
            StartCoroutine(timer());
        }
    }

    IEnumerator timer()
    {
        timerText.SetText("5");
        yield return new WaitForSeconds(1);
        timerText.SetText("4");
        yield return new WaitForSeconds(1);
        timerText.SetText("3");
        yield return new WaitForSeconds(1);
        timerText.SetText("2");
        yield return new WaitForSeconds(1);
        timerText.SetText("1");
        yield return new WaitForSeconds(1);
        timerText.SetText("0");
        yield return new WaitForSeconds(1);
        restartPane.SetActive(false);
        Team1Spawner.target3Destroyed = false;
        Team1Spawner.target2Destroyed = false;
        Team1Spawner.target1Destroyed = false;
        Team2Spawner.target3Destroyed = false;
        Team2Spawner.target2Destroyed = false;
        Team2Spawner.target1Destroyed = false;
        Team1Spawner.currentTarget1Health = 3000f;
        Team1Spawner.currentTarget2Health = 3000f;
        Team1Spawner.currentTarget3Health = 3000f;
        Team2Spawner.currentTarget1Health = 3000f;
        Team2Spawner.currentTarget2Health = 3000f;
        Team2Spawner.currentTarget3Health = 3000f;
        giftPane.SetActive(true);
        giftSpawnPane1.SetActive(true);
        giftSpawnPane2.SetActive(true);
        giftSpawnPane3.SetActive(true);
        giftSpawnPane4.SetActive(true);
        PlayerMoveHandler.getHealthbar(Team1Target1).fillAmount = 1;
        PlayerMoveHandler.getHealthbar(Team1Target2).fillAmount = 1;
        PlayerMoveHandler.getHealthbar(Team1Target3).fillAmount = 1;
        PlayerMoveHandler2.getHealthbar(Team2Target1).fillAmount = 1;
        PlayerMoveHandler2.getHealthbar(Team2Target2).fillAmount = 1;
        PlayerMoveHandler2.getHealthbar(Team2Target3).fillAmount = 1;
        Team1Target1.SetActive(true);
        Team1Target2.SetActive(true);
        Team1Target3.SetActive(true);
        Team2Target1.SetActive(true);
        Team2Target2.SetActive(true);
        Team2Target3.SetActive(true);
        restartEnded = true;
    }
}
