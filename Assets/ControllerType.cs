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

  public void set(string key, string value)
    {
      switch (key.ToLower())
      {
          case "up":
              this.up = value;
              break;
          case "down":
              this.down = value;
              break;
          case "left":
              this.left = value;
              break;
          case "right":
              this.right = value;
              break;
          case "jump":
              this.jump = value;
              break;
          case "dash":
              this.dash = value;
              break;
          case "attack":
              this.attack = value;
              break;
          default:
              this.up = value;
              break;
      }
    }

  public override string ToString()
  {
    return "(" + this.up + ", " + this.down + ", " + this.left + ", " + this.right + ", " + this.jump + ", " + this.dash + ", " + this.attack  + ")";
  }
}