using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUIController : MonoBehaviour
{
    public string currentScreen;
    public Vector2 center;
    public string playType;
    public string map;
    public TMPro.TextMeshProUGUI statusText; //(From "GameObject" to "TMPro.TextMeshProUGUI statusText"
    public TMPro.TextMeshProUGUI StatusTextControl; //(From "GameObject" to "TMPro.TextMeshProUGUI statusText" 
    public string selectedPlayer;
    public string selectedCharacter;
    public string selectedCharacterControl;
    public string settings;
    public TMPro.TextMeshProUGUI Player1Left;
    public TMPro.TextMeshProUGUI Player1Right;
    public TMPro.TextMeshProUGUI Player1Up;
    public TMPro.TextMeshProUGUI Player1Down;

    public TMPro.TextMeshProUGUI Player2Left;
    public TMPro.TextMeshProUGUI Player2Right;
    public TMPro.TextMeshProUGUI Player2Up;
    public TMPro.TextMeshProUGUI Player2Down;


    public bool Mute;
    public bool FullScreen;
    
    // Start is called before the first frame update
    void Start(){
        currentScreen = "TitleScreen";
        center = new Vector2(0,0);

        playType = "";
        map = "";

        statusText = GameObject.Find("StatusText").GetComponent<TMPro.TextMeshProUGUI>();
        StatusTextControl = GameObject.Find("StatusTextControl").GetComponent<TMPro.TextMeshProUGUI>();
        //Controls Player 1
        Player1Left = GameObject.Find("StatusTextControlLeft").GetComponent<TMPro.TextMeshProUGUI>();
        Player1Right = GameObject.Find("StatusTextControlRight").GetComponent<TMPro.TextMeshProUGUI>();
        Player1Up = GameObject.Find("StatusTextControlUp").GetComponent<TMPro.TextMeshProUGUI>();
        Player1Down = GameObject.Find("StatusTextControlDown").GetComponent<TMPro.TextMeshProUGUI>();
        //Controls Player 2
        Player2Left = GameObject.Find("StatusTextControlLeft").GetComponent<TMPro.TextMeshProUGUI>();
        Player2Right = GameObject.Find("StatusTextControlRight").GetComponent<TMPro.TextMeshProUGUI>();
        Player2Up = GameObject.Find("StatusTextControlUp").GetComponent<TMPro.TextMeshProUGUI>();
        Player2Down = GameObject.Find("StatusTextControlDown").GetComponent<TMPro.TextMeshProUGUI>();

        selectedPlayer = "";
        selectedCharacter = "";
        selectedCharacterControl = "";

        Mute = true;
        FullScreen = true;
        
    }

    public void handleButtonPress(string e) {
        Debug.Log(e);
        Type thisType = this.GetType();
        MethodInfo theMethod = thisType.GetMethod(e);
        theMethod.Invoke(this, null);
    }

    public void changeScreen(string screen) {
        GameObject oldScreen = GameObject.Find(currentScreen);
        GameObject newScreen = GameObject.Find(screen);
        oldScreen.GetComponent<RectTransform>().anchoredPosition = new Vector2(-1000, 0);
        newScreen.GetComponent<RectTransform>().anchoredPosition = center;

    }

    public void StartButton() {
        changeScreen("PlayTypeScreen");
    }

    public void LANButton() {
        playType = "LAN";
        changeScreen("MapSelectScreen");
    }
    public void LocalButton() {
        playType = "Local";
        changeScreen("MapSelectScreen");
    }
    public void Map1() {
        map = "Map1";
        changeScreen("PlayerSelectScreen");
    }
    public void SettingsButton() {
        changeScreen("OptionsScreen");
    }
    public void ControlButton(){
        changeScreen("PlayerSettings");
    }
    public void MuteButton(){
        Mute = !Mute;
    }
    public void FullScreenButton(){
        Screen.fullScreen = !Screen.fullScreen;
    }
//--------------------------------------------------------------------------------------------------------------------------------------------------------------------
    public void RedStickMan() {
        if (selectedPlayer == "") {
            statusText.text = "Please select a player first!";
        } else {
            selectedCharacter = "redstickman";
            statusText.text = selectedPlayer + " has selected " + selectedCharacter;
        }
    }

    public void BlueStickMan() {
        if (selectedPlayer == "") {
            statusText.text = "Please select a player first!";
        } else {
            selectedCharacter = "bluestickman";
            statusText.text = selectedPlayer + " has selected " + selectedCharacter;
        }
    }
    public void Player1() {
        selectedPlayer = "player1";
        statusText.text = "Now select a character!";
    }
    public void Player2() {
        selectedPlayer = "player2";
        statusText.text = "Now select a character!";
    }
    // This is Control Code Now
    public void PlayerOneSettingButton()
    {
        StatusTextControl.text = "You have selected Player 1 settings!";
        selectedCharacterControl = "player1";

    }
    public void PlayerTwoSettingButton() {
        StatusTextControl.text = "You have selected Player 2 settings!";
        selectedCharacterControl = "player2";
    }
    public void LeftSettingButton()
    {
        if(selectedCharacterControl == "")
        {
            StatusTextControl.text = "You have to select a Player First";
        }
        else{
            StatusTextControl.text = "Press a Button";
            if(selectedCharacterControl == "player1"){
                if (Input.inputString.Length > 0){
            // Get the last key pressed
                    Player1Left.text = Input.inputString;
                    Debug.Log("It should print" + Player1Left.text); 
                }
            }
            
        }
    }
    public void RightSettingButton()
    {
        if (selectedCharacterControl == "")
        {
            StatusTextControl.text = "You have to select a Player First";
        }
        else{
            StatusTextControl.text = "You have to select a the Right button";
        }
    }
    public void UpSettingButton()
    {
        if (selectedCharacterControl == "")
        {
            StatusTextControl.text = "You have to select a Player First";
        }
        else{
            StatusTextControl.text = "You have to select a the Up button";
        }
    }
    public void DownSettingButton()
    {
        if (selectedCharacterControl == "")
        {
            StatusTextControl.text = "You have to select a Player First";
        }
        else{
            StatusTextControl.text = "You have to select a the Down button";
        }
    }



    //   public void StartGameButton() {
    //       if (selectedCharacter != "" && selectedPlayer != "") {
    // set global controller things
    // move to game scene
    // }
    //  }
}
