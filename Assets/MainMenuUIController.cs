using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Dynamic;
using static System.Linq.Enumerable;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

using static GlobalController;
using static PlayerData;
using Unity.VisualScripting;
using System.Diagnostics.CodeAnalysis;
using Unity.Burst.Intrinsics;

public class MainMenuUIController : MonoBehaviour
{
    public GlobalController game;
    public string currentScreen;
    public Vector2 center;
    public string playType;
    public string map;
    public GameObject[] characters;
    public TMPro.TextMeshProUGUI statusText; //(From "GameObject" to "TMPro.TextMeshProUGUI statusText"
    public TMPro.TextMeshProUGUI StatusTextControl; //(From "GameObject" to "TMPro.TextMeshProUGUI statusText" 
    public string selectedPlayer;
    public string oppositePlayer;

    public string selectedCharacterControl;
    public int numPlayers;
    public List<PlayerData> players;
    public string settings;
    public bool selecting;
    public GameObject startGameButton;
    public TMPro.TextMeshProUGUI leftControlUI;
    public TMPro.TextMeshProUGUI rightControlUI;
    public TMPro.TextMeshProUGUI upControlUI;
    public TMPro.TextMeshProUGUI downControlUI;
    public TMPro.TextMeshProUGUI jumpControlUI;
    public TMPro.TextMeshProUGUI dashControlUI;
    public TMPro.TextMeshProUGUI attackControlUI;
    private bool captureKeyInput = false;
    private string keyInputType = "";
    private bool isRedStickManSelected = false;
    private bool isBlueStickManSelected = false;

    private Stack<string> screenHistory = new Stack<string>();
    public List<string> playerControllerStore1;
    public List<string> playerControllerStore2;

    public ControllerType wasdreset;

    public ControllerType arrowreset;
    public GameObject characterSelectObject;
    public List<GameObject> characterSelectObjectArray;
    public bool Mute;
    public bool FullScreen;

    // Start is called before the first frame update
    void Start() {
        game = GameObject.Find("GLOBALOBJECT").GetComponent<GlobalController>();

        currentScreen = "TitleScreen";
        center = new Vector2(0, 0);

        playType = "";
        map = "";

        selecting = false;
        startGameButton = GameObject.Find("StartGame");

        //statusText = GameObject.Find("StatusText").GetComponent<TMPro.TextMeshProUGUI>();
        StatusTextControl = GameObject.Find("StatusTextControl").GetComponent<TMPro.TextMeshProUGUI>();
        // control ui's
        leftControlUI = GameObject.Find("StatusTextControlLeft").GetComponent<TMPro.TextMeshProUGUI>();
        rightControlUI = GameObject.Find("StatusTextControlRight").GetComponent<TMPro.TextMeshProUGUI>();
        upControlUI = GameObject.Find("StatusTextControlUp").GetComponent<TMPro.TextMeshProUGUI>();
        downControlUI = GameObject.Find("StatusTextControlDown").GetComponent<TMPro.TextMeshProUGUI>();

        jumpControlUI = GameObject.Find("StatusTextControlJump").GetComponent<TMPro.TextMeshProUGUI>();
        dashControlUI = GameObject.Find("StatusTextControlDash").GetComponent<TMPro.TextMeshProUGUI>();
        attackControlUI = GameObject.Find("StatusTextControlAttack").GetComponent<TMPro.TextMeshProUGUI>();

        selectedPlayer = "";
        selectedCharacterControl = "";
        wasdreset = new ControllerType("W", "S", "A", "D", "Space", "LeftShift", "Mouse0");
        arrowreset = new ControllerType("UpArrow", "DownArrow", "LeftArrow", "RightArrow", "RightControl", "RightShift", "Mouse1");
        Debug.Log(wasdreset.right.ToString());
        Debug.Log(arrowreset.up.ToString());

        characterSelectObject = Resources.Load<GameObject>("UI/characterSelect");
        characterSelectObjectArray = new List<GameObject>();

        // player stuff
        numPlayers = 2;
        players = new List<PlayerData>();
        playerControllerStore1 = new List<string>(new string[7]);
        playerControllerStore2 = new List<string>(new string[7]);

        // grab all player models
        characters = Resources.LoadAll<GameObject>("Characters");

        // initializing players
        foreach (var index in Range(1, numPlayers)) {
            PlayerData playerData = new PlayerData();
            playerData.name = "player" + index.ToString();
            playerData.character = "";
            if (index == 1) {
                playerData.controllerType = game.wasd;
            } else if (index == 2) {
                playerData.controllerType = game.arrow;
            }
            players.Add(playerData);
            // update ui
            addCharcterSelect();
        }
        Debug.Log(players);
        Mute = false;
        FullScreen = false;
    }

    GameObject getCharacter(string characterName) {
        foreach (GameObject character in characters) {
            if (character.name == characterName) {
                return character;
            }
        }
        return new GameObject();
    }

    public int getPNum(string playerName) {
        int pNum = Int32.Parse(playerName.Split("player")[1]) - 1;
        return pNum;
    }

    public void printPlayerData() {
        int index = 1;
        foreach (PlayerData player in players) {
            Debug.Log("player" + index.ToString() + "\n" + 
                      "     " + player.name + "\n" +
                      "     " + player.character + "\n" +
                      "     " + player.controllerType + "\n");
            index++;
        }
    }

    public void updateControlsUI() {
        leftControlUI.text = players[getPNum(selectedCharacterControl)].controllerType.left;
        rightControlUI.text = players[getPNum(selectedCharacterControl)].controllerType.right;
        upControlUI.text = players[getPNum(selectedCharacterControl)].controllerType.up;
        downControlUI.text = players[getPNum(selectedCharacterControl)].controllerType.down;

        jumpControlUI.text = players[getPNum(selectedCharacterControl)].controllerType.jump;
        dashControlUI.text = players[getPNum(selectedCharacterControl)].controllerType.dash;
        attackControlUI.text = players[getPNum(selectedCharacterControl)].controllerType.attack;

        if(getPNum(selectedCharacterControl) == 1)
        {   
            playerControllerStore1[0] = leftControlUI.text;
            playerControllerStore1[1] = rightControlUI.text;
            playerControllerStore1[2] = upControlUI.text;
            playerControllerStore1[3] = downControlUI.text;
            playerControllerStore1[4] = jumpControlUI.text;
            playerControllerStore1[5] = dashControlUI.text;
            playerControllerStore1[6] = attackControlUI.text;
        } else {
            playerControllerStore2[0] = leftControlUI.text;
            playerControllerStore2[1] = rightControlUI.text;
            playerControllerStore2[2] = upControlUI.text;
            playerControllerStore2[3] = downControlUI.text;
            playerControllerStore2[4] = jumpControlUI.text;
            playerControllerStore2[5] = dashControlUI.text;
            playerControllerStore2[6] = attackControlUI.text;

        }
    }

    public void updateStartButton() {
        bool startable = true;
        Debug.Log(players);
        foreach (PlayerData player in players) {
            if (player.character == "" || selecting) {
                startable = false;
            }
        }
        startGameButton.SetActive(startable);
    }

    public void addCharcterSelect() {
        GameObject newSelectObject = Instantiate(characterSelectObject);
        Debug.Log("newSelectObject");
        characterSelectObjectArray.Add(newSelectObject);

        List<string> charaNames = new List<string>();
        foreach (GameObject character in characters) {
            charaNames.Add(character.name);
            Debug.Log(charaNames);
            Debug.Log(character.name);
        }

        int i = 0;
        foreach (GameObject selUI in characterSelectObjectArray) {
            int currentIndex = i;

            GameObject pss = GameObject.Find("PlayerSelectScreen");
            Debug.Log("Posing");
            float mapWidth = pss.transform.Find("bg").GetComponent<RectTransform>().rect.width;
            Debug.Log(mapWidth);
            float startingPoint = pss.transform.position.x - (mapWidth/2);
            Debug.Log(startingPoint);
            float segmentSize = mapWidth/numPlayers;

            Debug.Log(segmentSize);
            float finalPos = startingPoint + segmentSize*i + segmentSize/2;
            Debug.Log(finalPos);
            selUI.transform.position = new Vector2(finalPos, pss.transform.position.y);
            selUI.transform.parent = pss.transform;

            selUI.name = players[i].name;
            Debug.Log("UpArrow " + (currentIndex + 1).ToString());

            selUI.transform.Find("uparrow").GetComponent<Button>().onClick.RemoveAllListeners();
            selUI.transform.Find("downarrow").GetComponent<Button>().onClick.RemoveAllListeners();

            selUI.transform.Find("uparrow").GetComponent<Button>().onClick.AddListener(delegate {handleButtonPress("UpArrow " + (currentIndex+1).ToString()); Debug.Log("THE UP ARROW HAS BEEN PRESSED, AND I AM GONG TO RUN THE HANDEBUTTONPREEESS UCINTOM"); });
            selUI.transform.Find("downarrow").GetComponent<Button>().onClick.AddListener(delegate {handleButtonPress("DownArrow " + (currentIndex+1).ToString()); });
            selUI.transform.Find("numberholder").transform.Find("number").GetComponent<TextMeshProUGUI>().text = (currentIndex+1).ToString();
            
            if (!selUI.transform.Find("selectedChara")) {
                GameObject newCharacter = Instantiate(getCharacter(charaNames[0]));

                players[currentIndex].character = charaNames[0];

                newCharacter.transform.position = selUI.transform.position;
                newCharacter.transform.parent = selUI.transform;
                newCharacter.GetComponent<RectTransform>().localScale = new Vector2(newCharacter.GetComponent<RectTransform>().localScale.x*80, newCharacter.GetComponent<RectTransform>().localScale.y*80);
                newCharacter.name = "selectedChara";
                Destroy(newCharacter.GetComponent<PlayerController>());
                Destroy(newCharacter.GetComponent<Rigidbody2D>());
            }


            i++;
        }
    }

    public void handleButtonPress(string e) {
        Debug.Log(e);
        Type thisType = this.GetType();
        MethodInfo theMethod;

        if (e.Any(x => Char.IsWhiteSpace(x))) {
            theMethod = thisType.GetMethod(e.Split(' ')[0]);
            string data = e.Split(' ')[1];
            theMethod.Invoke(this, new object[] { data });
        } else {
            theMethod = thisType.GetMethod(e);
            theMethod.Invoke(this, null);
        }
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

        updateStartButton();
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
        FullScreen = Screen.fullScreen;
    }

    public void PlayerSelectRightUp(){
        
    }
    public void UpArrow(string number) {
        GameObject ourCharacterSelectObjectArray = characterSelectObjectArray[Int32.Parse(number)-1];
        Debug.Log("moveing character selectino up for player" + number);
        Debug.Log(ourCharacterSelectObjectArray.name);

        Debug.Log(characters);
        Debug.Log(players[getPNum(characterSelectObjectArray[Int32.Parse(number)-1].name)].character);
        var index = Array.FindIndex(characters, character => character.name == players[getPNum(characterSelectObjectArray[Int32.Parse(number)-1].name)].character);
        Debug.Log(index);
        GameObject temp;
        GameObject finalChara;

        if((index-1) >= 0 && (index-1) < characters.Length) {
            temp = characters[index-1];
            finalChara = Instantiate(characters[index-1]);
        } else {
            temp = characters[characters.Length-1];
            finalChara = Instantiate(characters[characters.Length-1]);
        }
        // update screen 
        GameObject oldChara = ourCharacterSelectObjectArray.transform.Find("selectedChara").gameObject;
        
        finalChara.transform.position = ourCharacterSelectObjectArray.transform.position;
        finalChara.transform.parent = ourCharacterSelectObjectArray.transform;
        finalChara.GetComponent<RectTransform>().localScale = new Vector2(finalChara.GetComponent<RectTransform>().localScale.x*80, finalChara.GetComponent<RectTransform>().localScale.y*80);
        finalChara.name = "selectedChara";
        Destroy(finalChara.GetComponent<PlayerController>());
        Destroy(finalChara.GetComponent<Rigidbody2D>());
        
        Destroy(oldChara);

        players[getPNum(characterSelectObjectArray[Int32.Parse(number)-1].name)].character = temp.name;
    }
    public void DownArrow(string number) {
        GameObject ourCharacterSelectObjectArray = characterSelectObjectArray[Int32.Parse(number)-1];
        Debug.Log("moveing character selectino down for player" + number);
        Debug.Log(ourCharacterSelectObjectArray.name);

        Debug.Log(characters);
        Debug.Log(players[getPNum(characterSelectObjectArray[Int32.Parse(number)-1].name)].character);
        var index = Array.FindIndex(characters, character => character.name == players[getPNum(characterSelectObjectArray[Int32.Parse(number)-1].name)].character);
        Debug.Log(index);
        GameObject temp;
        GameObject finalChara;

        if((index+1) >= 0 && (index+1) < characters.Length) {
            temp = characters[index+1];
            finalChara = Instantiate(characters[index+1]);
        } else {
            temp = characters[0];
            finalChara = Instantiate(characters[0]);
        }
        // update screen 
        GameObject oldChara = ourCharacterSelectObjectArray.transform.Find("selectedChara").gameObject;
        
        finalChara.transform.position = ourCharacterSelectObjectArray.transform.position;
        finalChara.transform.parent = ourCharacterSelectObjectArray.transform;
        finalChara.GetComponent<RectTransform>().localScale = new Vector2(finalChara.GetComponent<RectTransform>().localScale.x*80, finalChara.GetComponent<RectTransform>().localScale.y*80);
        finalChara.name = "selectedChara";
        Destroy(finalChara.GetComponent<PlayerController>());
        Destroy(finalChara.GetComponent<Rigidbody2D>());
        
        Destroy(oldChara);

        players[getPNum(characterSelectObjectArray[Int32.Parse(number)-1].name)].character = temp.name;
    }
    public void Player1() {
        
        selectedPlayer = "player1";
        oppositePlayer = "player2";
        statusText.text = "Now select a character!";
        selecting = true;
        updateStartButton();
    }
    public void Player2() {
        selectedPlayer = "player2";
        oppositePlayer = "player1";
        statusText.text = "Now select a character!";
        selecting = true;
        updateStartButton();
    }

    public void StartGame() {
        SceneManager.LoadScene("GameArena");
        
        // save to global
        game.players = players;
        game.map = map;
        game.playType = playType;
        game.mute = Mute;
        game.fullScreen = FullScreen;
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
    public string GetCurrentSettings() {
        players[getPNum(selectedCharacterControl)].controllerType = game.wasd;
            var controller = players[getPNum(selectedCharacterControl)].controllerType;
            string controllerSettings = $"Player {getPNum(selectedCharacterControl) + 1} Controller Settings:\n" +
                                        $"Left: {controller.left}, Right: {controller.right}, " +
                                        $"Up: {controller.up}, Down: {controller.down}, " +
                                        $"Jump: {controller.jump}, Dash: {controller.dash}, " +
                                        $"Attack: {controller.attack}";
        return controllerSettings;
    }


    public void PlayerSettingResetButton() {
        StatusTextControl.text = "You have clicked Reset.";
        if(selectedCharacterControl == "") {
            StatusTextControl.text = "You have to select a Player First";
        } else {
                
                if (getPNum(selectedCharacterControl) == 0) {
                
                    players[getPNum(selectedCharacterControl)].controllerType.up = wasdreset.up.ToString();
                    players[getPNum(selectedCharacterControl)].controllerType.down = wasdreset.down.ToString();
                    players[getPNum(selectedCharacterControl)].controllerType.left = wasdreset.left.ToString();
                    players[getPNum(selectedCharacterControl)].controllerType.right = wasdreset.right.ToString();
                    players[getPNum(selectedCharacterControl)].controllerType.jump = wasdreset.jump.ToString();
                    players[getPNum(selectedCharacterControl)].controllerType.dash = wasdreset.dash.ToString();
                    players[getPNum(selectedCharacterControl)].controllerType.attack = wasdreset.attack.ToString();
                } else {
                    // Assigning properties from game.arrow to the selected player's controllerType
                    players[getPNum(selectedCharacterControl)].controllerType.up = arrowreset.up.ToString();
                    players[getPNum(selectedCharacterControl)].controllerType.down = arrowreset.down.ToString();
                    players[getPNum(selectedCharacterControl)].controllerType.left = arrowreset.left.ToString();
                    players[getPNum(selectedCharacterControl)].controllerType.right = arrowreset.right.ToString();
                    players[getPNum(selectedCharacterControl)].controllerType.jump = arrowreset.jump.ToString();
                    players[getPNum(selectedCharacterControl)].controllerType.dash = arrowreset.dash.ToString();
                    players[getPNum(selectedCharacterControl)].controllerType.attack = arrowreset.attack.ToString();
            }
                updateControlsUI();
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
    public void JumpSettingButton()
    {
        if (selectedCharacterControl == "")
        {
            StatusTextControl.text = "You have to select a Player First";
        }
        else{
            StatusTextControl.text = "Press a Button";
            captureKeyInput = true;
            keyInputType = "Jump";
        }
    }
    public void DashSettingButton()
    {
        if (selectedCharacterControl == "")
        {
            StatusTextControl.text = "You have to select a Player First";
        }
        else{
            StatusTextControl.text = "Press a Button";
            captureKeyInput = true;
            keyInputType = "Dash";
        }
    }
    public void AttackSettingButton()
    {
        if (selectedCharacterControl == "")
        {
            StatusTextControl.text = "You have to select a Player First";
        }
        else{
            StatusTextControl.text = "Press a Button";
            captureKeyInput = true;
            keyInputType = "Attack";
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

            selecting = false;
        }
    }
    public void replaceKey(string newKeyCode, string keyInputType)
    {
        // Get a reference to the selected player's controllerType
        var controllerType = players[getPNum(selectedCharacterControl)].controllerType;

        // Check each key binding and clear it if it matches the new key code
        if (controllerType.left == newKeyCode) controllerType.left = "None";
        if (controllerType.right == newKeyCode) controllerType.right = "None";
        if (controllerType.up == newKeyCode) controllerType.up = "None";
        if (controllerType.down == newKeyCode) controllerType.down = "None";
        if (controllerType.jump == newKeyCode) controllerType.jump = "None";
        if (controllerType.dash == newKeyCode) controllerType.dash = "None";
        if (controllerType.attack == newKeyCode) controllerType.attack = "None";

        // Now set the new key code to the desired control
        switch (keyInputType)
        {
            case "Left":
                controllerType.left = newKeyCode;
                break;
            case "Right":
                controllerType.right = newKeyCode;
                break;
            case "Up":
                controllerType.up = newKeyCode;
                break;
            case "Down":
                controllerType.down = newKeyCode;
                break;
            case "Jump":
                controllerType.jump = newKeyCode;
                break;
            case "Dash":
                controllerType.dash = newKeyCode;
                break;
            case "Attack":
                controllerType.attack = newKeyCode;
                break;
            default:
                Debug.LogError("Invalid keyInputType provided: " + keyInputType);
                break;
        }
        updateControlsUI();
    }
    bool IsAllowedKey(KeyCode keyCode)
    {
        // Define any keys that should cancel the operation
        if (keyCode == KeyCode.Escape)
        {
            return false; // Escape or other cancel keys are not allowed for binding
        }

        // Your existing logic to exclude Mouse0 and others
        List<KeyCode> excludedKeys = new List<KeyCode> { KeyCode.Mouse0 };
        return !excludedKeys.Contains(keyCode);
    }


    void Update()
    {
        // only runs on controller page
        if (captureKeyInput && Input.anyKeyDown)
        {
            foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(kcode) &&  IsAllowedKey(kcode))
                {
                    if (keyInputType == "Left")
                    {
                        replaceKey(kcode.ToString(), keyInputType);
                        players[getPNum(selectedCharacterControl)].controllerType.left = kcode.ToString();
                        Debug.Log("Left key set to: " + kcode + " for " + selectedCharacterControl);
                    }

                    if(keyInputType == "Right"){
                        replaceKey(kcode.ToString(), keyInputType);
                        players[getPNum(selectedCharacterControl)].controllerType.right = kcode.ToString();
                        Debug.Log("Right key set to: " + kcode + " for " + selectedCharacterControl);
                    }
                    if (keyInputType == "Up")
                    {
                        replaceKey(kcode.ToString(), keyInputType);
                        players[getPNum(selectedCharacterControl)].controllerType.up = kcode.ToString();
                        Debug.Log("Up key set to: " + kcode + " for " + selectedCharacterControl);
                    }
                    if (keyInputType == "Down"){
                        replaceKey(kcode.ToString(), keyInputType);
                        players[getPNum(selectedCharacterControl)].controllerType.down = kcode.ToString();
                        Debug.Log("Down key set to: " + kcode + " for " + selectedCharacterControl);
                    }
                    if (keyInputType == "Jump"){
                        replaceKey(kcode.ToString(), keyInputType);
                        players[getPNum(selectedCharacterControl)].controllerType.jump = kcode.ToString();
                        Debug.Log("Jump key set to: " + kcode + " for " + selectedCharacterControl);
                    }
                    if (keyInputType == "Dash"){
                        replaceKey(kcode.ToString(), keyInputType);
                        players[getPNum(selectedCharacterControl)].controllerType.dash = kcode.ToString();
                        Debug.Log("Dash key set to: " + kcode + " for " + selectedCharacterControl);
                    }
                    if (keyInputType == "Attack"){
                        replaceKey(kcode.ToString(), keyInputType);
                        players[getPNum(selectedCharacterControl)].controllerType.attack = kcode.ToString();
                        Debug.Log("Attack key set to: " + kcode + " for " + selectedCharacterControl);
                    }
                    captureKeyInput = false; // Stop capturing key input
                    updateControlsUI();
                    break;
                }
            }
        }
        if (Input.anyKeyDown) {
            foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(kcode))
                {
                    if (kcode.ToString() == "O") {
                        printPlayerData();
                    }
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
