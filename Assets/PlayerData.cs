using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static ControllerType;

public class PlayerData
{
    public string name;
    public string character;
    public ControllerType controllerType;

    public bool selected;

    public int health;
    public float special;

  // Create a class constructor with a parameter
  public PlayerData(string name = "", string character = "", ControllerType? controllerTypex = null, int health = 0, int special = 0)
  {
    controllerTypex = controllerTypex ?? new ControllerType("", "", "", "", "", "", "", "", "");
    this.name = name;
    this.character = character;
    this.controllerType = controllerTypex;
    this.selected = false;
    this.health = health;
    this.special = special;
  }
}