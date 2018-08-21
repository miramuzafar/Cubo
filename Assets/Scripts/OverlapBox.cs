using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlapBox : MonoBehaviour {

	// Use this for initialization
	public List<Collider> objToMove = new List<Collider>();
    public LayerMask mask;
	public Color32 selectColor;

    void FixedUpdate()
    {
		int oldLayer = this.gameObject.layer;//create a temp layer
		this.gameObject.layer = 2;//ignore raycasting on the same layer type
		Collider[] colliders;
		colliders = Physics.OverlapBox(this.gameObject.transform.position, this.gameObject.transform.GetComponent<Collider>().bounds.size, this.gameObject.transform.rotation, mask);//detect any overlapping slots on the component
		this.gameObject.layer = oldLayer;//revert the layer of the component to its original layer type	
		objToMove = new List<Collider>();
		foreach(Collider col in colliders)
		{
			if(this.transform.parent != null)
			{
				objToMove.Add(col);//add the overlapped slots inside the list
            	col.gameObject.GetComponent<MeshRenderer>().material.color = selectColor;
			}
		}
    }
}
