using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Dynamic;
using static System.Linq.Enumerable;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using static GlobalController;
using static PlayerData;
using Unity.VisualScripting;



public class CharacterSelector : MonoBehaviour
{
    public Image displayImage; // The UI Image component displaying the character
    public Sprite[] characters; // Array of character sprites
    private int currentIndex = 0; // Current character index

    public void NextCharacter()
    {
        int nextIndex = (currentIndex + 1) % characters.Length;
        StartCoroutine(TransitionCharacter(nextIndex));
    }

    public void PreviousCharacter()
    {
        int nextIndex = (currentIndex - 1 + characters.Length) % characters.Length;
        StartCoroutine(TransitionCharacter(nextIndex));
    }

    private IEnumerator TransitionCharacter(int nextIndex)
    {
        // Implement your transition effect here
        // For example, gradually change displayImage.sprite from current to next character

        yield return new WaitForSeconds(1); // Simulate transition time

        currentIndex = nextIndex;
        displayImage.sprite = characters[currentIndex];
    }
}