using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

using static PlayerData;

public class CharacterBase
{
    public float walkSpeed;
    public float sprintMultiplier;
    public float yAccel;
    public float maxHeight;
    // light
    public float lightAttackUpValue;
    public float lightAttackDownValue;
    public float lightAttackForwardValue;
    public float lightAttackBackValue;
    // heavy
    public float heavyAttackUpValue;
    public float heavyAttackDownValue;
    public float heavyAttackForwardValue;
    public float heavyAttackBackValue;
    // air light
    public float lightAttackForwardAirValue;
    public float lightAttackDownAirValue;
    // air heavy
    public float heavyAttackForwardAirValue;
    public float heavyAttackDownAirValue;

    public Action Attack = () => {
        
    };
    // l means Light Attack
    public Action<Animator, GameObject, int> lForward = (Animator anim, GameObject charaObj, int dir) => {
        // now its instansiated. every time we work with these functions we have to define
        // the type of data we pass through 
    };
    public Action<Animator, GameObject, int> lBackward = (Animator anim, GameObject charaObj, int dir) => {
        
    };
    public Action<Animator, GameObject, int> lUp = (Animator anim, GameObject charaObj, int dir) => {
        
    };
    public Action<Animator, GameObject, int> lDown = (Animator anim, GameObject charaObj, int dir) => {
        
    };
    // h means Heavy Attack
    public Action<Animator, GameObject, int> hForward = (Animator anim, GameObject charaObj, int dir) => {
        
    };
    public Action<Animator, GameObject, int> hBackward = (Animator anim, GameObject charaObj, int dir) => {
        
    };
    public Action<Animator, GameObject, int> hUp = (Animator anim, GameObject charaObj, int dir) => {
        
    };
    public Action<Animator, GameObject, int> hDown = (Animator anim, GameObject charaObj, int dir) => {
        
    };
    // s means Special Attack
    public Action sForward = () => {
        
    };
    public Action sUp = () => {
        
    };
    public Action sDown = () => {
        
    };
    // a means Aerial Input Attack
    public Action<Animator, GameObject, int> laForward = (Animator anim, GameObject charaObj, int dir) => {
        
    };
    public Action<Animator, GameObject, int> laDown = (Animator anim, GameObject charaObj, int dir) => {
        
    };
    public Action<Animator, GameObject, int> haForward = (Animator anim, GameObject charaObj, int dir) => {
        
    };
    public Action<Animator, GameObject, int> haDown = (Animator anim, GameObject charaObj, int dir) => {
        
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
    public Action<Animator, GameObject> vJumpUp = (Animator anim, GameObject charaObj) => {
        
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
    public Action<Animator, GameObject, PlayerData, int> hitGround = (Animator anim, GameObject charaObj, PlayerData playerData, int attackAmount) => {
        
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
