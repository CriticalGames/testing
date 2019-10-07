using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HellTap.DataKit;
using UnityEngine.UI;

// CLIMATE CORRUPTION SAVE AND LOAD 
// Property of ZOOM INC.
//
//  Instantly save or load the following data:
//                                             - currency - amount of money accumulated by the player ($4009)
//                                             - achievement one - representing the level of a particular evolved achievement. (1,2 or 3)
//                                             - temperature - the game setting for earth temperature.

public class GameSaveLoad : MonoBehaviour
{
    [Header ("References")]
    public Button saveButton = null;
    public Button loadButton = null;
    public GameObject Savetext;

    private void OnEnable()
    {
        OnNotReady();
        DataKit.OnReady += OnReady;
        DataKit.OnNotReady += OnNotReady;
    }

    private void OnDisEnable()
    {
        DataKit.OnReady-= OnReady;
        DataKit.OnNotReady -= OnNotReady;
    }

   void  OnReady()
    {
        saveButton.interactable = true;
        loadButton.interactable = true;
    }

    void OnNotReady()
    {
        saveButton.interactable = false;
        loadButton.interactable = false;
    }

    public void OnUserPressedSaveButton()
    {
        if (DataKit.IsReady())
        {
            DataKit.AutoSave();

        }
    }

   public void OnUserPressedLoadButton()
    {
        if (DataKit.IsReady())
        {
            DataKit.AutoLoad();
        }
    }
}
