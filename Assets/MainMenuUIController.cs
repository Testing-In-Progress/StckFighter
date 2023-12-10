using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System;
using UnityEngine;

public class MainMenuUIController : MonoBehaviour
{
    public string currentScreen;
    public Vector2 center;
    public string playType;
    public string map;
    public TMPro.TextMeshProUGUI statusText; //(From "GameObject" to "TMPro.TextMeshProUGUI statusText"
    public string selectedPlayer;
    public string selectedCharacter;
    public string settings;

    public bool Mute;
    public bool FullScreen;
   
    // Start is called before the first frame update
    void Start(){
        currentScreen = "TitleScreen";
        center = new Vector2(0,0);

        playType = "";
        map = "";

        statusText = GameObject.Find("StatusText").GetComponent<TMPro.TextMeshProUGUI>();

        selectedPlayer = "";
        selectedCharacter = "";
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
    public void MuteButton(){
        Mute = !Mute;
    }
    public void FullScreenButton(){
        FullScreen = !FullScreen;
    }





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
 //   public void StartGameButton() {
 //       if (selectedCharacter != "" && selectedPlayer != "") {
            // set global controller things
            // move to game scene
       // }
  //  }
}
