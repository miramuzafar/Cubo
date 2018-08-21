using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIController : MonoBehaviour
{

    public InputField valueGridX;
    public InputField valueGridZ;
    public Button applyButton;

    private PlaceObject placeObject;

    void Start()
    {
        placeObject = FindObjectOfType(typeof(PlaceObject)) as PlaceObject;

        applyButton.onClick = new Button.ButtonClickedEvent();
        applyButton.onClick.AddListener(() => Apply());
    }

    public void Apply()
    {
        placeObject.Destroy();

        placeObject.sizeGridX = int.Parse(valueGridX.text);
        placeObject.sizeGridZ = int.Parse(valueGridZ.text);

        placeObject.FillGrid();
    }
}
