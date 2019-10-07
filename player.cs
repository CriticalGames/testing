using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HellTap.DataKit;
using UnityEngine.UI;


public class player : MonoBehaviour

{

    public float playerCurrency = 0f;
    public int playerAchOneLevel;
    public float worldTemperature = 14.9f;
    public Text Text_PlayerCurrency;
    public Text Text_AchOneLevel;
    public Text Text_WorldTemp;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Text_AchOneLevel.text = playerAchOneLevel.ToString();
        Text_PlayerCurrency.text = "$" + playerCurrency;
        Text_WorldTemp.text = worldTemperature.ToString();
    }
        private void OnEnable()
    {
        DataKit.OnSaveLocalDataStarted += OnSaveLocalDataStarted;
        DataKit.OnLoadLocalDataSuccessful += OnLoadLocalDataSuccessful;
    }

    private void OnDisEnable()
    {
        DataKit.OnSaveLocalDataStarted -= OnSaveLocalDataStarted;
        DataKit.OnLoadLocalDataSuccessful -= OnLoadLocalDataSuccessful;
    }

    public void ManipulateCurrency()
    {
        playerCurrency = playerCurrency + 10f;

    }

    public void ManipulateTemperature()
    {
        playerCurrency = playerCurrency - 5f;
        worldTemperature = worldTemperature + .02f;
    }

    public void ManipulateAchOneLevel()
    {
        playerAchOneLevel = playerAchOneLevel + 25;
    }
    
    
    private void OnLoadLocalDataSuccessful()
    {

        playerCurrency = DataKit.GetLocalKey<float>("Player", "Currency");
        playerAchOneLevel = DataKit.GetLocalKey<int>("Player", "AchOneLevel");
        worldTemperature = DataKit.GetLocalKey<float>("Player", "WorldTemperature");

    }

    private void OnSaveLocalDataStarted()
    {

        DataKit.SetLocalKey("Player", "Currency", playerCurrency);
        DataKit.SetLocalKey("Player", "AchOneLevel", playerAchOneLevel);
        DataKit.SetLocalKey("Player", "WorldTemperature", worldTemperature);


    }
}
