using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class CharacterBase
{
    public Action Attack = () => {
        
    };
    // l means Light Attack
    public Action lForward = () => {
        
    };
    public Action lBackward = () => {
        
    };
    public Action lUp = () => {
        
    };
    public Action lDown = () => {
        
    };
    // h means Heavy Attack
    public Action hForward = () => {
        
    };
    public Action hBackward = () => {
        
    };
    public Action hUp = () => {
        
    };
    public Action hDown = () => {
        
    };
    // s means Special Attack
    public Action sForward = () => {
        
    };
    public Action sUp = () => {
        
    };
    public Action sDown = () => {
        
    };
    // a means Aerial Input Attack
    public Action laForward = () => {
        
    };
    public Action laDown = () => {
        
    };
    public Action haForward = () => {
        
    };
    public Action haDown = () => {
        
    };
    public Action saForward = () => {
        
    };
    public Action saUp = () => {
        
    };
    public Action saDown = () => {
        
    };
    // Movement Actions
    public Action Idle = () => {
        
    };
    public Action forwardWalk = () => {
        
    };
    public Action backwardWalk = () => {
        
    };
    public Action Sprint = () => {
        
    };
    public Action Crouch = () => {
        
    };
    // Jump Animations
    public Action vJumpUp = () => {
        
    };
    public Action vJumpHover = () => {
        
    };
    public Action vJumpFall = () => {
        
    };
    public Action fJumpUp = () => {
        
    };
    public Action fJumpHover = () => {
        
    };
    public Action fJumpFall = () => {
        
    };
    public Action bJumpUp = () => {
        
    };
    public Action bJumpHover = () => {
        
    };
    public Action bJumpFall = () => {
        
    };
    // Dash Animations
    public Action BackDash = () => {
        
    };
    public Action aBackDash = () => {
        
    };
    // Shield/Block Animation
    public Action holdShield = () => {
        
    };
    public Action holdShieldCrouch = () => {
        
    };
    public Action hitShield = () => {
        
    };
    public Action hitShieldCrouch = () => {
        
    };
    public Action hitShieldRelease = () => {
        
    };
    public Action hitShieldCrouchRelease = () => {
        
    };
    // Hitstun Animation
    public Action hitGround = () => {
        
    };
    public Action hitGroundRelease = () => {
        
    };
    public Action hitAir = () => {
        
    };
    public Action hitAirFall = () => {
        
    };
    public Action hitAirLand = () => {
        
    };

    // Intro/Exit Sequence
    public Action introEnter = () => {
        
    };
    // There will be 4 possible animations for the dialogue,  
    // 2 for if they begin talking first,
    // 2 for when they talk second, 
    // The dialogue will be different depending on the opponent
    public Action introDialogueOne = () => {
        
    };
    public Action introDialogueTwo = () => {
        
    };
    public Action introDialogueThree = () => {
        
    };
    public Action introDialogueFour = () => {
        
    };
    public Action introReady = () => {
        
    };
    public Action exitAnimation = () => {
        
    };
}
