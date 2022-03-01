using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "IAT/WordCategory")]
public class WordCategory : ScriptableObject
{
    public string CategoryName;

    [SerializeField]
    private List<string> _words;

    private int _currentIndex = 0;
    private string _currentWord = "";

    public void ShuffleWords()
    {
        // Rearrange images in list.
        var rnd = new System.Random();
        _words = _words.OrderBy(item => rnd.Next()).ToList();

        _currentIndex = 0;

        // If the last word selected happens to be first in the list after shuffling, skip to the next one.
        if (_words[0] == _currentWord)
        {
            _currentIndex = 1;
        }
    }

    public string GetWord()
    {
        if (_words.Count == 0)
            Debug.LogError("Error: No words found in WordCategory: " + CategoryName);

        // Reshuffle if all words have been selected.
        if (_currentIndex >= _words.Count)
            ShuffleWords();

        _currentWord = _words[_currentIndex];
        _currentIndex++;

        return _currentWord;
    }
}