using UnityEngine;
using System.Collections;
 
public class SetMaterialColorToChildren : MonoBehaviour
{
    Color startColor;
    Color currentColor;
 
    // Use this for initialization
    void Start ()
    {
		currentColor = Color.yellow;
        startColor = gameObject.GetComponent<Renderer>().material.color;             
    }
 
    // Update is called once per frame
	public void OnMouseEnter()
 	{
		gameObject.GetComponent<Renderer>().material.SetColor("_Color", currentColor);
		for (int childIndex = 0; childIndex < gameObject.transform.childCount; childIndex++)
        {
            Transform child = gameObject.transform.GetChild(childIndex);           
            child.gameObject.GetComponent<Renderer>().material.SetColor("_Color", currentColor);
        }
 	}
 	public void OnMouseExit()
 	{
		gameObject.GetComponent<Renderer>().material.SetColor("_Color", startColor);
		for (int childIndex = 0; childIndex < gameObject.transform.childCount; childIndex++)
        {
            Transform child = gameObject.transform.GetChild(childIndex);           
            child.gameObject.GetComponent<Renderer>().material.SetColor("_Color", startColor);
        }
 	}
}