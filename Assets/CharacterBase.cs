using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class CharacterBase
{
    public float minJumpHeight;
    public float maxJumpHeight;
    public float movementSpeed;
    public float fallSpeed;
    public float jumpSpeed;

    public Action Attack = () => {
        
    };
    // sNormal is basically b, and all other (s)'s are b [direction]
    public Action sNormal = () => {
        
    };
    public Action sUp = () => {
        
    };
    public Action sDown = () => {
        
    };
    public Action sLeft = () => {
        
    };
    public Action sRight = () => {
        
    };
    public Action hUp = () => {
        
    };
    public Action hDown = () => {
        
    };
    public Action hLeft = () => {
        
    };
    public Action hRight = () => {
        
    };

}
