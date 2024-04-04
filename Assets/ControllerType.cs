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
  public string light;
  public string heavy;
  public string special;

  // Create a class constructor with a parameter
  public ControllerType(string up, string down, string left, string right, string jump, string dash, string light, string heavy, string special)
  {
    this.up = up;
    this.down = down;
    this.left = left;
    this.right = right;
    this.jump = jump;
    this.dash = dash;
    this.light = light;
    this.heavy = heavy;
    this.special = special;
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
          case "light":
              this.light = value;
              break;
          case "heavy":
              this.heavy = value;
              break;
          case "special":
              this.special = value;
              break;
          default:
              this.up = value;
              break;
      }
    }

  public override string ToString()
  {
    return "(" + this.up + ", " + this.down + ", " + this.left + ", " + this.right + ", " + this.jump + ", " + this.dash + ", " + this.light + ", " + this.heavy + ", " + this.special  + ")";
  }
}