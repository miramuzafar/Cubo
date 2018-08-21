using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPlacementController : MonoBehaviour {

	// Use this for initialization
	public Transform gridObject;
	public int gridElementsX = 6;
	public int gridElementsZ = 4;
	public int gridElementsSizeX = 4;
	public int gridElementsSizeZ = 2;
	private int totalGridSizeX;
	private int totalGridSizeZ;
	public Transform furniture;

	void Update()
	{
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		Physics.Raycast(ray, out hit, Mathf.Infinity);
		Vector3 furnitureTransform;
		furnitureTransform = furniture.transform.position;
		furnitureTransform.x = gridObject.position.x + Mathf.Floor(hit.point.x * (gridElementsSizeX/totalGridSizeX)) * gridElementsSizeX;
		furnitureTransform.z = gridObject.position.z + Mathf.Floor(hit.point.z * (gridElementsSizeZ/totalGridSizeZ)) * gridElementsSizeZ;
	}
}
