using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections.Generic;

namespace SixThreeZero{
    namespace UI{
        
/// <summary>
/// Custom Color Picker component.
/// </summary>
public class UI_ColorPicker:MonoBehaviour{

    
    private static UI_ColorPicker _instance;

/// <summary>
/// Gets a reference to to UI_ColorPicker gameObject
/// </summary>
/// <returns>Returns either a UI_ColorPicker reference to the current UI_colorPicker component, or null if no component exists</returns>
    public UI_ColorPicker Instance { get { return _instance;}}

    /// <summary>
    /// Custom UnityEvent<Color32> that is used by the UI_ColorPicker component
    /// </summary>
    [System.Serializable]
    public class ColorPickerEvent:UnityEvent<Color32>{};


    /// <summary>
    /// Reference to the Image used to display the color preview.
    /// </summary>
    public Image SelectedColorPreview;

    /// <summary>
    /// reference to the RectTransform that contains the color chart.
    /// </summary>
    public RectTransform colorChartRect;    

    /// <summary>
    /// reference to the RectTransform of the panel that contains the color chart.
    /// </summary>
    public RectTransform colorChartContainer;

    /// <summary>
    /// reference to the RectTransform of the panel that contains the color history buttons.
    /// </summary>
    public RectTransform colorHistoryContainer;

    /// <summary>
    /// reference to the cancel selection button.
    /// </summary>
    public Button cancelButton;

    /// <summary>
    /// reference to the select button.
    /// </summary>
    public Button selectButton;

    /// <summary>
    /// reference to the Slider used to control the R value of the selected color.
    /// </summary>
    [Header("Inputs")]
    public Slider slider_R;
    /// <summary>
    /// reference to the Slider used to control the G value of the selected color.
    /// </summary>
    public Slider slider_G;
    /// <summary>
    /// reference to the Slider used to control the B value of the selected color.
    /// </summary>
    public Slider slider_B;
    /// <summary>
    /// reference to the Slider used to control the A value of the selected color.
    /// </summary>
    public Slider slider_A;

    /// <summary>
    /// reference to the inputField used to enter numeric value for the R value of the selected color.
    /// </summary>
    public InputField input_R;
    /// <summary>
    /// reference to the inputField used to enter numeric value for the G value of the selected color.
    /// </summary>
    public InputField input_G;
    /// <summary>
    /// reference to the inputField used to enter numeric value for the B value of the selected color.
    /// </summary>
    public InputField input_B;
    /// <summary>
    /// reference to the inputField used to enter numeric value for the A value of the selected color.
    /// </summary>
    public InputField input_A;

    /// <summary>
    /// Reference to the RectTransform the is used as the Color Palette Container.
    /// </summary>
    [Header("Color Pallete")]
    public RectTransform ColorPaletteContainer;

    /// <summary>
    /// Reference to the Button used to add colors to the Palette.
    /// </summary>
    public Button btnAddColorToPallete;

    /// <summary>
    /// Reference to the Image used as a template for the buttons in the color palette.
    /// </summary>
    public Image ColorPalleteEntryPrefab;

    /// <summary>
    /// List of color palette colors.
    /// </summary>
    public List<Color32> PalleteColors;

    /// <summary>
    /// List of Images that are used as the Color Selection History Buttons.
    /// </summary>
    [Header("Color Picker")]
    public List<Image> colorHistoryButtons;

    /// <summary>
    /// List of color used for the color selection history.
    /// </summary>
    public List<Color32> colorHistory;

    /// <summary>
    /// Event that is fired when the SelectedColor is altered.
    /// </summary>
    public ColorPickerEvent OnColorChanged;

    /// <summary>
    /// Event that is fired when the user clicks the "Select" button.
    /// </summary>
    public ColorPickerEvent ColorSelected;

    /// <summary>
    /// Event that is fired when the user hits the "Cancel" button.
    /// </summary>
    public UnityEvent SelectionCanceled;


    private bool _showColorChart = true;
    private bool _showColorHistory = true;

    private Color32 _originalColor;
    public Color32 OriginalColor{ get{ return _originalColor;}}
    private Color32 _selectedColor;
    private byte _r;
    private byte _g;
    private byte _b;
    private byte _a;

    private UnityAction<Color32> selectedColorCallback;
    private UnityAction cancelSelectionCallback;
    
    /// <summary>
    /// Public Get/Set accessor for the currently Selected Color.
    /// </summary>
    /// <returns>Returns the currently selected color</returns>
    public Color SelectedColor{ get{ return _selectedColor;} }

/// <summary>
    /// Public Get/Set accessor for the currently Selected Color.
    /// </summary>
    /// <returns>Returns the currently selected color</returns>
    public Color32 SelectedColor32{ get{ return _selectedColor;} }

    private Image colorChartImage;

    /// <summary>
    /// Handles initialization of the UI Color Picker.
    /// </summary>
    /// <returns>Returns the currently selected color</returns>
    public void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;

        colorChartImage = colorChartRect.GetComponent<Image>();



        slider_R.onValueChanged.AddListener(SetRedValue);
        slider_G.onValueChanged.AddListener(SetGreenValue);
        slider_B.onValueChanged.AddListener(SetBlueValue);
        slider_A.onValueChanged.AddListener(SetAlphaValue);
        
        input_R.onEndEdit.AddListener(SetRedValue);
        input_G.onEndEdit.AddListener(SetGreenValue);
        input_B.onEndEdit.AddListener(SetBlueValue);
        input_A.onEndEdit.AddListener(SetAlphaValue);

        input_R.onValueChanged.AddListener(SetRedValue);
        input_G.onValueChanged.AddListener(SetGreenValue);
        input_B.onValueChanged.AddListener(SetBlueValue);
        input_A.onValueChanged.AddListener(SetAlphaValue);


        _r = (byte) slider_R.value;
        _g = (byte) slider_G.value;
        _b = (byte) slider_B.value;
        _a = (byte) slider_A.value;

        input_R.text = _r.ToString();
        input_G.text = _g.ToString();
        input_B.text = _b.ToString();
        input_A.text = _a.ToString();

        btnAddColorToPallete.onClick.AddListener(handleBtnAddColorToPallete);
        updateColorHistoryButtons();
        cancelButton.onClick.AddListener(handleBtnCancelClick);
        selectButton.onClick.AddListener(handleBtnSelectClick);
        for (int i = 0; i < colorHistoryButtons.Count;i++)
        {
            int currentI = i;
            Button nButton = colorHistoryButtons[i].GetComponent<Button>();
            nButton.onClick.AddListener(()=> SetColorFromHistory(currentI));
        }
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Public method to display the UI with no Color Chart or history. This is for access by other UI elements suc as buttons
    /// to cause the UI to display as opposed to the Runtime API. The downside to displaying the UI
    /// this way is you are unable to pass the Selection Color In.
    /// </summary>
    public void Display()
    {
        _instance.OnColorChanged.RemoveAllListeners();
        _instance._showColorChart = false;
        _instance._showColorHistory = false;
        _instance.togglePannels();
        _instance.updateColorHistoryButtons();
        _instance.gameObject.SetActive(true);
    }

    /// <summary>
    /// Public method to display the UI with a Color Chart but no history. This is for access by other UI elements suc as buttons
    /// to cause the UI to display as opposed to the Runtime API. The downside to displaying the UI
    /// this way is you are unable to pass the Selection Color In.
    /// </summary>
    public void DisplayWithColorChart()
    {
    _instance.OnColorChanged.RemoveAllListeners();
        _instance._showColorChart = true;
        _instance._showColorHistory = false;
        _instance.togglePannels();
        _instance.updateColorHistoryButtons();
        _instance.gameObject.SetActive(true);

    }

    /// <summary>
    /// Public method to display the UI with no Color Chart but a history. This is for access by other UI elements suc as buttons
    /// to cause the UI to display as opposed to the Runtime API. The downside to displaying the UI
    /// this way is you are unable to pass the Selection Color In.
    /// </summary>
    public void DisplayWithHistory()
    {
        _instance.OnColorChanged.RemoveAllListeners();
        _instance._showColorChart = false;
        _instance._showColorHistory = true;
        _instance.togglePannels();
        _instance.updateColorHistoryButtons();
        _instance.gameObject.SetActive(true);
    }

    /// <summary>
    /// Public method to display the UI with both a Color Chart and a history. This is for access by other UI elements suc as buttons
    /// to cause the UI to display as opposed to the Runtime API. The downside to displaying the UI
    /// this way is you are unable to pass the Selection Color In.
    /// </summary>
    public void DisplayWithColorChartAndHistory()
    {
        _instance.OnColorChanged.RemoveAllListeners();
        _instance._showColorChart = true;
        _instance._showColorHistory = true;
        _instance.togglePannels();
        _instance.updateColorHistoryButtons();
        _instance.gameObject.SetActive(true);
    }


/// <summary>
/// Display the color picker component in its default state which shows both the color chart and color history panels.
/// </summary>
/// <param name="originalColor">The color that the color picker starts out with selected.</param>
/// <param name="selectedCallback">The function that gets called when a color is picked.</param>
/// <param name="cancelCallback">(Optional)The function that gets called when the selection process is canceled.</param>
    public static void Display(Color32 originalColor, UnityAction<Color32> selectedCallback, UnityAction cancelCallback = null)
    {
        
        _instance.OnColorChanged.RemoveAllListeners();
        _instance._originalColor = originalColor;
        _instance._showColorChart = true;
        _instance._showColorHistory = true;
        _instance.selectedColorCallback = selectedCallback;
        _instance.cancelSelectionCallback = cancelCallback;
        _instance.togglePannels();
        _instance.updateColorHistoryButtons();
        _instance.gameObject.SetActive(true);
        
        

    }

    /// <summary>
    /// Display the color picker component in its default state which shows both the color chart and color history panels.
    /// </summary>
    /// <param name="originalColor">The color that the color picker starts out with selected.</param>
    /// <param name="selectedCallback">The function that gets called when a color is picked.</param>
    /// <param name="colorChangedCallback">The function that gets called as the Selected Color is changed. Used for live preview of the color during selection process.</param>
    /// <param name="cancelCallback">The function that gets called when the selection process is canceled.</param>
    /// 
    public static void Display(Color32 originalColor, UnityAction<Color32> selectedCallback, UnityAction<Color32> colorChangedCallback, UnityAction cancelCallback = null)
    {
        _instance.OnColorChanged.RemoveAllListeners();
        _instance.OnColorChanged.AddListener(colorChangedCallback);
        _instance._originalColor = originalColor;
        _instance._showColorChart = true;
        _instance._showColorHistory = true;
        _instance.selectedColorCallback = selectedCallback;
        
        _instance.cancelSelectionCallback = cancelCallback;
        _instance.togglePannels();
        _instance.updateColorHistoryButtons();
        
        _instance.gameObject.SetActive(true);
        
        

    }

/// <summary>
/// Displays the color picker component. Allows you to specifiy if the color chart and color history panels are visible or not.
/// </summary>
/// <param name="originalColor">The color that the color picker starts out with selected.</param>
/// <param name="showColorChart">If the color chart panel is visible</param>
/// <param name="showColorHistory">If the color history window is visible</param>
/// <param name="selectedCallback">The function that gets called when a color is picked.</param>
/// <param name="cancelCallback">The function that gets called when the selection process is canceled.</param>
    public static void Display(Color32 originalColor, bool showColorChart, bool showColorHistory, UnityAction<Color32> selectedCallback, UnityAction<Color32> colorChangedCallback, UnityAction cancelCallback = null)
    {
        _instance.OnColorChanged.RemoveAllListeners();
        _instance._originalColor = originalColor;
        _instance._showColorChart = showColorChart;
        _instance._showColorHistory = showColorHistory;
        _instance.selectedColorCallback = selectedCallback;
        _instance.OnColorChanged.AddListener(colorChangedCallback);
        _instance.cancelSelectionCallback = cancelCallback;
        _instance.togglePannels();
        _instance.updateColorHistoryButtons();
        _instance.gameObject.SetActive(true);
    }

/// <summary>
/// Displays the color picker component. Allows you to specifiy if the color chart and color history panels are visible or not.
/// </summary>
/// <param name="originalColor">The color that the color picker starts out with selected.</param>
/// <param name="showColorChart">If the color chart panel is visible</param>
/// <param name="showColorHistory">If the color history window is visible</param>
/// <param name="selectedCallback">The function that gets called when a color is picked.</param>
/// <param name="cancelCallback">The function that gets called when the selection process is canceled.</param>
    public static void Display(Color32 originalColor, bool showColorChart, bool showColorHistory, UnityAction<Color32> selectedCallback, UnityAction cancelCallback = null)
    {
        _instance.OnColorChanged.RemoveAllListeners();
        _instance._originalColor = originalColor;
        _instance._showColorChart = showColorChart;
        _instance._showColorHistory = showColorHistory;
        _instance.selectedColorCallback = selectedCallback;
        _instance.cancelSelectionCallback = cancelCallback;
        _instance.togglePannels();
        _instance.updateColorHistoryButtons();
        _instance.gameObject.SetActive(true);
    }

    /// <summary>
    /// toggle that Color Chart & Color History panels on and off based on the ShowColorChart and ShowColorHistory properties.
    /// </summary>
    private void togglePannels()
    {
        colorChartContainer.gameObject.SetActive(_showColorChart);
        colorHistoryContainer.gameObject.SetActive(_showColorHistory);
    }

    /// <summary>
    /// Updates the SelectedColor with the values from _r, _g, _b, _a
    /// </summary>
    private void UpdateSelectedColorFromRGBA()
    {
        _selectedColor = new Color32( _r, _g, _b, _a);
        SelectedColorPreview.color = _selectedColor;

        if (OnColorChanged != null)
            OnColorChanged.Invoke(_selectedColor);
    }

    /// <summary>
    /// Handler for when the user drags the mouse around the Color Chart. Updates the Selected color with the color under the mouse.
    /// </summary>
    /// <param name="eventData">Event Data passed from the event dispatcher</param>
    public void handleColorChartDrag(BaseEventData eventData)
    {
        PointerEventData pEventData = eventData as PointerEventData;
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(colorChartRect, pEventData.position, null,  out localPoint);
        _selectedColor = colorChartImage.sprite.texture.GetPixel((int) localPoint.x, (int) localPoint.y);
        SelectedColorPreview.color = _selectedColor;
        slider_R.value = _selectedColor.r;
        slider_G.value = _selectedColor.g;
        slider_B.value = _selectedColor.b;
        slider_A.value = _selectedColor.a;

        if (OnColorChanged != null)
            OnColorChanged.Invoke(_selectedColor);
    }

    /// <summary>
    /// Allows you to set the Red value of the SelectedColor using a float, which is then converted to a byte.
    /// This is useful for things like the Slider component where the event only passes a float value.
    /// </summary>
    /// <param name="value">Value between 0 and 255. This value is converted to a byte</param>
    public void SetRedValue(float value)
    {
        _r = (byte)value;
        slider_R.value = value;
        input_R.text = value.ToString();
        UpdateSelectedColorFromRGBA();
    }

    /// <summary>
    /// Allows you to set the Red value of the SelectedColor using a int, which is then converted to a byte.
    /// This is useful for linking in to events where the event passes an int value.
    /// </summary>
    /// <param name="value">Value between 0 and 255. This value is converted to a byte</param>
    public void SetRedValue(int value)
    {
        _r = (byte)value;
        slider_R.value = value;
        input_R.text = value.ToString();
        UpdateSelectedColorFromRGBA();
    }

    /// <summary>
    /// Allows you to set the Red value of the SelectedColor using a string, which is then converted to a byte.
    /// This is useful for linking in to events where the event passes an string value such as the inputField UI component.
    /// </summary>
    /// <param name="value"></param>
    public void SetRedValue(string value)
    {
        byte val;

        if (byte.TryParse(value, out val))
        {
            _r = val;
            slider_R.value = val;
            input_R.text = value;
            UpdateSelectedColorFromRGBA();
        }
    }

    public void SetGreenValue(float value)
    {
        _g = (byte)value;
        slider_G.value = value;
        input_G.text = value.ToString();
        UpdateSelectedColorFromRGBA();
    }

    public void SetGreenValue(int value)
    {
        _g = (byte)value;
        slider_G.value = value;
        input_G.text = value.ToString();
        UpdateSelectedColorFromRGBA();
    }

    public void SetGreenValue(string value)
    {
        byte val;

        if (byte.TryParse(value, out val))
        {
            _g = val;
            slider_G.value = val;
            input_G.text = value;
            UpdateSelectedColorFromRGBA();
        }
    }

    public void SetBlueValue(float value)
    {
        _b = (byte)value;
        slider_B.value = value;
        input_B.text = value.ToString();
        UpdateSelectedColorFromRGBA();
    }

    public void SetBlueValue(int value)
    {
        _b = (byte)value;
        slider_B.value = value;
        input_B.text = value.ToString();
        UpdateSelectedColorFromRGBA();
    }

    public void SetBlueValue(string value)
    {
        byte val;

        if (byte.TryParse(value, out val))
        {
            _b = val;
            slider_B.value = val;
            input_B.text = value;
            UpdateSelectedColorFromRGBA();
        }
    }

    public void SetAlphaValue(float value)
    {
        _a = (byte)value;
        slider_A.value = value;
        input_A.text = value.ToString();
        UpdateSelectedColorFromRGBA();
    }

    public void SetAlphaValue(int value)
    {
        _a = (byte)value;
        slider_A.value = value;
        input_A.text = value.ToString();
        UpdateSelectedColorFromRGBA();
    }

    public void SetAlphaValue(string value)
    {
        byte val;

        if (byte.TryParse(value, out val))
        {
            _a = val;
            slider_A.value = val;
            input_A.text = value;
            UpdateSelectedColorFromRGBA();
        }
    }

    public void SetColorFromHistory(int index)
    {
        if (index < 0 || index >= colorHistory.Count)
            return;

        SetSelectedColor(colorHistory[index]);

    }

    public void SetColorFromPalette(int index)
    {
        if (index < 0 || index >= PalleteColors.Count)
            return;

        SetSelectedColor(PalleteColors[index]);

    }
    public void SetSelectedColor(Color32 color)
    {
        _r = color.r;
        _g = color.g;
        _b = color.b;
        _a = color.a;

        slider_R.value = (int)color.r;
        slider_G.value = (int)color.g;
        slider_B.value = (int)color.b;
        slider_A.value = (int)color.a;
        input_R.text = _r.ToString();
        input_G.text = _g.ToString();
        input_B.text = _b.ToString();
        input_A.text = _a.ToString();


        UpdateSelectedColorFromRGBA();
    }

    /// <summary>
    /// Handler for when the user clicks the button to add the currently selected color to the palette.
    /// </summary>
    public void handleBtnAddColorToPallete()
    {
        if (doesPalleteContainColor(_selectedColor))
            return;

        Image newColorPrefab = GameObject.Instantiate(ColorPalleteEntryPrefab, ColorPaletteContainer.transform, false) as Image;
        newColorPrefab.color = _selectedColor;
        int newIndex = PalleteColors.Count;
        newColorPrefab.GetComponent<Button>().onClick.AddListener( ()=> SetColorFromPalette(newIndex));
        newColorPrefab.gameObject.SetActive(true);

        PalleteColors.Add(_selectedColor);
        btnAddColorToPallete.transform.SetAsLastSibling();
    }

    /// <summary>
    /// Handler for when the user clicks the cancel button. Dispatches the cancelSelectionCallback function.
    /// </summary>
    public void handleBtnCancelClick()
    {
        if (cancelSelectionCallback != null)
        {
            cancelSelectionCallback.Invoke();
        }

        gameObject.SetActive(false);
    }

    /// <summary>
    /// Handler for when the user clicks the "Select" button. Dispatches the selectedColorCallback function.
    /// </summary>
    public void handleBtnSelectClick()
    {
        if (selectedColorCallback != null)
        {
            selectedColorCallback.Invoke(_selectedColor);
        }
        AddColorToHistory(_selectedColor);
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Checks if the color palette contains the color passed in to the function.
    /// </summary>
    /// <param name="color">The color to check for.</param>
    /// <returns>true if the color is already in the palette, otherwise returns false</returns>
    public bool doesPalleteContainColor(Color32 color)
    {
        if (PalleteColors == null)
            return false;

        for (int i = 0; i < PalleteColors.Count; i++)
        {
            if (AreColorsEqual(color, PalleteColors[i]))
                return true;

        }

        return false;
    }

    /// <summary>
    /// Determines if two colors are equal.
    /// </summary>
    /// <param name="color1">The first color to compare</param>
    /// <param name="color2">The second color to compare</param>
    /// <returns>true if the colors are a match, otherwise it returns false</returns>
    private bool AreColorsEqual(Color32 color1, Color32 color2)
    {

        if ((color1.r == color2.r) && (color1.g == color2.g) && (color1.g == color2.g) && (color1.b == color2.b) && color1.a == color2.a)
            return true;

            return false;

    }

    /// <summary>
    /// Adds a color to the color histroy if the color is anot already present in the history.
    /// </summary>
    /// <param name="color">The color to check for</param>
    public void AddColorToHistory(Color32 color)
    {
        if (doesHistoryContainColor(color))
            return;

        colorHistory.Insert(0,color);


    }

    /// <summary>
    /// Checks if the Color History contains the color passed in to the function.
    /// </summary>
    /// <param name="color">The color to check for.</param>
    /// <returns>True if the color is already present in the history, otherwise it returns false</returns>
    public bool doesHistoryContainColor(Color32 color)
    {
        for (int i = 0; i < colorHistory.Count; i++)
        {
            if (AreColorsEqual(color, colorHistory[i]))
                return true;

        }

        return false;
    }

    /// <summary>
    /// Updates the color History Buttons display colors to match the Color History.
    /// </summary>
    public void updateColorHistoryButtons()
    {
        for (int i = 0; i < colorHistoryButtons.Count; i++)
        {
            if (i >= colorHistory.Count)
            return;


            colorHistoryButtons[i].color = colorHistory[i];
        }
    }
}

    }
}