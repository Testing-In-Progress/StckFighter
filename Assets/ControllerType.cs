using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerType
{
    public string up;
    public string down;
    public string left;
    public string right;
    public string jump;
    public string dash;
    public string attack;

  // Create a class constructor with a parameter
  public ControllerType(string up, string down, string left, string right, string jump, string dash, string attack)
  {
    this.up = up;
    this.down = down;
    this.left = left;
    this.right = right;
    this.jump = jump;
    this.dash = dash;
    this.attack = attack;
  }
}