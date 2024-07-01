using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateLevelUI : MonoBehaviour
{
    [SerializeField] private int level;
    [SerializeField] private Sprite[] levelUI;
    [SerializeField] private SpriteRenderer levelUIObject;
    // Start is called before the first frame update

    private void OnEnable()
    {
        level = 0;
        levelUIObject.sprite = levelUI[level];
    }

    public void UpdateLevel()
    {
        levelUIObject.sprite = levelUI[level];
        level++;
    }

}
