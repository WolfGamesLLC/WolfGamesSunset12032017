﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class TestModalPanel : MonoBehaviour {

    public Sprite icon;
    public Transform spawnPoint;
    public GameObject thingToSpawn;

    private ModalPanel modalPanel;
    private DisplayManager displayManager;

    private void Awake()
    {
        modalPanel = ModalPanel.Instance();
        displayManager = DisplayManager.Instance();
    }

    private void ButtonOneFunction()
    {
        displayManager.DisplayMessage("Button One Pressed");
    }

    private void ButtonTwoFunction()
    {
        displayManager.DisplayMessage("Button Two Pressed");
    }

    private void ButtonThreeFunction()
    {
        displayManager.DisplayMessage("Button Three Pressed");
    }

    public void TestYNCWindow()
    {
        ModalPanelData details = new ModalPanelData("Test the YNC dialog box");
        details.ButtonDetails.Add(new EventButtonData("YES", ButtonOneFunction));
        details.ButtonDetails.Add(new EventButtonData("NO", ButtonTwoFunction));
        details.ButtonDetails.Add(new EventButtonData("CANCEL", ButtonThreeFunction));

        modalPanel.ShowPanel(details);
    }

    public void TestErrorWindow()
    {
        modalPanel.SetSelection("OK", ButtonOneFunction);

        modalPanel.ShowPanel("You have encountered an error", icon);
    }

    public void TestLambda()
    {
        modalPanel.SetSelection("YES", () => { InstantiateObject(thingToSpawn); });
        modalPanel.SetSelection("NO", () => {  });
        modalPanel.ShowPanel("Do you wish to instantiate a cube with a Lambda function?");
    }

    private void InstantiateObject(GameObject thingToInstantiate)
    {
        displayManager.DisplayMessage("Display Text");
        Instantiate(thingToInstantiate, spawnPoint.position, spawnPoint.rotation);
    }
}
