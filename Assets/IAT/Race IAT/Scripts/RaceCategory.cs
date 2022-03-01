using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "IAT/RaceCategory")]
public class RaceCategory : ScriptableObject
{
    public string CategoryName;

    [SerializeField]
    private List<Texture2D> _images;

    [NonSerialized]
    private int _currentIndex = 0;

    [NonSerialized]
    private int _currentImageInstanceID = -1;

    public void ShuffleImages()
    {
        // Rearrange images in list.
        var rnd = new System.Random();
        _images = _images.OrderBy(item => rnd.Next()).ToList();

        _currentIndex = 0;

        // If the last image selected happens to be first in the list after shuffling, skip to the next one.
        if (_images[0].GetInstanceID() == _currentImageInstanceID)
        {
            _currentIndex = 1;
        }
    }

    public Texture2D GetPortrait()
    {
        if (_images.Count == 0)
            Debug.LogError("Error: No Images in RaceCategory: " + CategoryName);

        // Reshuffle if all images have been shown.
        if (_currentIndex >= _images.Count)
            ShuffleImages();

        Texture2D tex = _images[_currentIndex];
        _currentImageInstanceID = tex.GetInstanceID();
        _currentIndex++;

        return tex;
    }
}