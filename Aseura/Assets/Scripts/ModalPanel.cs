﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Event button data object for accessing event button properties
/// </summary>
public class EventButtonData
{
    #region Members

    private string title;
    private Sprite background;
    private UnityAction action;

    #endregion

    #region Properties

    /// <summary>
    /// Text displayed on the button
    /// </summary>
    public string Title
    {
        get { return title; }
        set { title = value; }
    }

    /// <summary>
    /// Background sprite used for the button
    /// </summary>
    public Sprite Background
    {
        get { return background; }
        set { background = value; }
    }

    /// <summary>
    /// A function reference to the action the button performs
    /// </summary>
    public UnityAction Action
    {
        get { return action; }
        set { action = value; }
    }

    #endregion

    #region Initialization

    /// <summary>
    /// Standard constructor for an EventButtonData object
    /// </summary>
    /// <param name="title">The text displayed on the button</param>
    /// <param name="action">A reference to a function that will be performed when the button is pressed</param>
    /// <param name="background">An optional sprite containing the background image used for the button</param>
    public EventButtonData(string title, UnityAction action, Sprite background = null)
    {
        Title = title;
        Action = action;
        Background = background;
    }

    #endregion
}

/// <summary>
/// Modal Panel data object for accessing modal panel data
/// </summary>
public class ModalPanelData
{
    #region Members

    private Transform position;
    private string title;
    private string text;
    private Sprite icon;
    private Sprite background;
    private List<EventButtonData> buttonDetails;

    #endregion
    #region Properties

    /// <summary>
    /// Position of the ModalPanel on the screen - NOT IMPLEMENTED
    /// </summary>
    public Transform Position
    {
        get { return position; }
        set { position = value; }
    }

    /// <summary>
    /// Title of the Modal Panel - NOT IMPLEMENTED
    /// </summary>
    public string Title
    {
        get { return title; }
        set { title = value; }
    }

    /// <summary>
    /// Textual content of the modal panel
    /// </summary>
    public string Text
    {
        get { return text; }
        set { text = value; }
    }

    /// <summary>
    /// A sprite containing the icon displayed on the panel
    /// </summary>
    public Sprite Icon
    {
        get { return icon; }
        set { icon = value; }
    }

    /// <summary>
    /// A sprite containing the background image to be displayed with the panel
    /// </summary>
    public Sprite Background
    {
        get { return background; }
        set { background = value; }
    }

    /// <summary>
    /// The list of buttons to be displayed on the panel
    /// </summary>
    public List<EventButtonData> ButtonDetails
    {
        get { return buttonDetails; }
        set { buttonDetails = value; }
    }

    #endregion

    #region Initialization

    /// <summary>
    /// The standard constructor for a ModalPanelData object
    /// </summary>
    /// <param name="text">The text to be displayed in the dialog box</param>
    /// <param name="icon">An option sprite that contains an icon to be displayed on the dialog</param>
    public ModalPanelData(string text, Sprite icon = null)
    {
        Text = text;
        Icon = icon;
        buttonDetails = new List<EventButtonData>();
    }

    #endregion
}

public class ModalPanel : MonoBehaviour 
{

    private int ButtonIndex;

    /// <summary>
    /// Unity Text object that display the text of the panel
    /// </summary>
    public Text text;

    /// <summary>
    /// Unity Image object that will display the optional dialog box icon
    /// </summary>
    public Image icon;
 
    public List<Button> buttons;
    public GameObject modalPanelObject;

    /// <summary>
    /// Singleton for access to the modal panel object
    /// </summary>
    private static ModalPanel modalPanel;
    public static ModalPanel Instance()
    {
        if (!modalPanel)
        {
            modalPanel = FindObjectOfType(typeof(ModalPanel)) as ModalPanel;
            if (!modalPanel)
                Debug.LogError("No active ModalPanel script found: there needs to be at least one active ModelPanel script on a game object in your game");
        }

        return modalPanel;
    }

    public void Awake()
    {
        ClosePanel();
    }

    public void Start()
    {
        ClosePanel();
    }

    public void SetSelection(string buttonText, UnityAction buttonEvent)
    {
        if (ButtonIndex >= buttons.Count)
        {
            Debug.LogError("You asked to create button #" + ButtonIndex.ToString() + " only " + buttons.Count.ToString() + " are allowed");
        }
        
        buttons[ButtonIndex].name = buttonText;
        buttons[ButtonIndex].onClick.RemoveAllListeners();
        buttons[ButtonIndex].onClick.AddListener(buttonEvent);
        buttons[ButtonIndex].onClick.AddListener(ClosePanel);
        buttons[ButtonIndex].gameObject.SetActive(true);

        ButtonIndex++;
    }

    public void ShowPanel(string text, Sprite imageIcon = null)
    {
        modalPanelObject.SetActive(true);

        this.text.gameObject.SetActive(true);
        this.text.text = text;

        if (imageIcon)
        {
            icon.sprite = imageIcon;
            icon.gameObject.SetActive(true);
        }
    }

    public void ShowPanel(ModalPanelData details)
    {
        foreach (EventButtonData buttonDetails in details.ButtonDetails)
        {
            SetSelection(buttonDetails.Title, buttonDetails.Action);
        }

        ShowPanel(details.Text, details.Icon);
    }

    private void ClosePanel()
    {
        modalPanelObject.SetActive(false);

        icon.gameObject.SetActive(false);
        text.gameObject.SetActive(false);

        foreach (Button button in buttons)
        {
            button.gameObject.SetActive(false);
        }

        ButtonIndex = 0;
    }
}
