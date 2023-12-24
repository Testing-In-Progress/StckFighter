using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Dynamic;
using static System.Linq.Enumerable;
using System;
using UnityEngine;
using UnityEngine.UI;
using static GlobalController;

public class MainMenuUIController : MonoBehaviour
{
    public GlobalController game;
    public string currentScreen;
    public Vector2 center;
    public string playType;
    public string map;
    public TMPro.TextMeshProUGUI statusText; //(From "GameObject" to "TMPro.TextMeshProUGUI statusText"
    public TMPro.TextMeshProUGUI StatusTextControl; //(From "GameObject" to "TMPro.TextMeshProUGUI statusText" 
    public string selectedPlayer;
    public string selectedCharacterControl;
    public int numPlayers;
    public dynamic players;
    public string settings;
    public TMPro.TextMeshProUGUI leftControlUI;
    public TMPro.TextMeshProUGUI rightControlUI;
    public TMPro.TextMeshProUGUI upControlUI;
    public TMPro.TextMeshProUGUI downControlUI;
    private bool captureKeyInput = false;
    private string keyInputType = "";

    private Stack<string> screenHistory = new Stack<string>();


    public bool Mute;
    public bool FullScreen;
    
    // Start is called before the first frame update
    void Start(){
        game = GameObject.Find("GLOBALOBJECT").GetComponent<GlobalController>();

        currentScreen = "TitleScreen";
        center = new Vector2(0,0);

        playType = "";
        map = "";

        statusText = GameObject.Find("StatusText").GetComponent<TMPro.TextMeshProUGUI>();
        StatusTextControl = GameObject.Find("StatusTextControl").GetComponent<TMPro.TextMeshProUGUI>();
        // control ui's
        leftControlUI = GameObject.Find("StatusTextControlLeft").GetComponent<TMPro.TextMeshProUGUI>();
        rightControlUI = GameObject.Find("StatusTextControlRight").GetComponent<TMPro.TextMeshProUGUI>();
        upControlUI = GameObject.Find("StatusTextControlUp").GetComponent<TMPro.TextMeshProUGUI>();
        downControlUI = GameObject.Find("StatusTextControlDown").GetComponent<TMPro.TextMeshProUGUI>();

        selectedPlayer = "";
        selectedCharacterControl = "";
        // player stuff
        numPlayers = 2;
        players = new ExpandoObject();

        // initializing players
        foreach (var index in Range(1, numPlayers)) {
            dynamic playerData = new ExpandoObject();
            playerData.name = "player" + index.ToString();
            playerData.character = "";
            if (index == 1) {
                playerData.controllerType = game.wasd;
            } else if (index == 2) {
                playerData.controllerType = game.arrow;
            } else {
                playerData.controllerType = "";
            }
            players["player" + index.ToString()] = playerData;
        }

        Mute = true;
        FullScreen = true;
        
    }

    public void printPlayerData() {
        foreach (var index in Range(1, numPlayers)) {
            string playerName = "player" + index.ToString();
            dynamic player = players[playerName];
            Debug.Log("     " + player.name);
            Debug.Log("     " + player.character);
            Debug.Log("     " + player.controllerType);
        }
    }

    public void updateControlsUI() {
        leftControlUI.text = players[selectedCharacterControl].controllerType.left;
        rightControlUI.text = players[selectedCharacterControl].controllerType.right;
        upControlUI.text = players[selectedCharacterControl].controllerType.up;
        downControlUI.text = players[selectedCharacterControl].controllerType.down;
    }

    public void handleButtonPress(string e) {
        Debug.Log(e);
        Type thisType = this.GetType();
        MethodInfo theMethod = thisType.GetMethod(e);
        theMethod.Invoke(this, null);
    }

    public void changeScreen(string screen) {
        if (currentScreen != screen) {
            screenHistory.Push(currentScreen); // Push the current screen onto the stack
        }

        GameObject oldScreen = GameObject.Find(currentScreen);
        GameObject newScreen = GameObject.Find(screen);
        oldScreen.GetComponent<RectTransform>().anchoredPosition = new Vector2(-1000, 0);
        newScreen.GetComponent<RectTransform>().anchoredPosition = center;
        currentScreen = screen; // Update currentScreen
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

    public void RedStickMan() {
        if (selectedPlayer == "") {
            statusText.text = "Please select a player first!";
        } else {
            players[selectedPlayer].character = "redstickman";
            statusText.text = selectedPlayer + " has selected " + players[selectedPlayer].character;
        }
    }

    public void BlueStickMan() {
        if (selectedPlayer == "") {
            statusText.text = "Please select a player first!";
        } else {
            players[selectedPlayer].character = "bluestickman";
            statusText.text = selectedPlayer + " has selected " + players[selectedPlayer].character;
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
        updateControlsUI();
        

    }
    public void PlayerTwoSettingButton() {
        StatusTextControl.text = "You have selected Player 2 settings!";
        selectedCharacterControl = "player2";
        updateControlsUI();
    }
    public void LeftSettingButton()
    {
        if(selectedCharacterControl == "")
        {
            StatusTextControl.text = "You have to select a Player First";
        }
        else {
            StatusTextControl.text = "Press a Button";
            captureKeyInput = true;
            keyInputType = "Left";
        }
    }
    public void RightSettingButton()
    {
        if (selectedCharacterControl == "")
        {
            StatusTextControl.text = "You have to select a Player First";
        }
        else{
            StatusTextControl.text = "Press a Button";
            captureKeyInput = true;
            keyInputType = "Right";
            
        }
    }
    public void UpSettingButton()
    {
        if (selectedCharacterControl == "")
        {
            StatusTextControl.text = "You have to select a Player First";
        }
        else{
            StatusTextControl.text = "Press a Button";
            captureKeyInput = true;
            keyInputType = "Up";
        }
    }
    public void DownSettingButton()
    {
        if (selectedCharacterControl == "")
        {
            StatusTextControl.text = "You have to select a Player First";
        }
        else{
            StatusTextControl.text = "Press a Button";
            captureKeyInput = true;
            keyInputType = "Down";
        }
    }
    public void Back()
    {
        if (screenHistory.Count > 0)
        {
            string previousScreen = screenHistory.Pop(); // Pop the top screen off the stack
            GameObject oldScreen = GameObject.Find(currentScreen);
            GameObject newScreen = GameObject.Find(previousScreen);
            oldScreen.GetComponent<RectTransform>().anchoredPosition = new Vector2(-1000, 0);
            newScreen.GetComponent<RectTransform>().anchoredPosition = center;
            currentScreen = previousScreen; // Update currentScreen
        }
    }


    void Update()
    {
        // only runs on controller page
        if (captureKeyInput && Input.anyKeyDown)
        {
            foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(kcode))
                {
                    if (keyInputType == "Left")
                    {
                        players[selectedCharacterControl].controllerType.left = kcode.ToString();
                        Debug.Log("Left key set to: " + kcode + " for " + selectedCharacterControl);
                    }
                    if(keyInputType == "Right"){
                        players[selectedCharacterControl].controllerType.right = kcode.ToString();
                        Debug.Log("Right key set to: " + kcode + " for " + selectedCharacterControl);
                    }
                    if (keyInputType == "Up")
                    {
                        players[selectedCharacterControl].controllerType.up = kcode.ToString();
                        Debug.Log("Up key set to: " + kcode + " for " + selectedCharacterControl);
                    }
                    if(keyInputType == "Down"){
                        players[selectedCharacterControl].controllerType.down = kcode.ToString();
                        Debug.Log("Down key set to: " + kcode + " for " + selectedCharacterControl);
                    }
                    captureKeyInput = false; // Stop capturing key input
                    updateControlsUI();
                    break;
                }
            }
        }
    }




    //   public void StartGameButton() {
    //       if (selectedCharacter != "" && selectedPlayer != "") {
    // set global controller things
    // move to game scene
    // }
    //  }
}
