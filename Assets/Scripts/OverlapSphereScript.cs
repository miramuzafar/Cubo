using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlapSphereScript : MonoBehaviour {

	// Use this for initialization
	public List<Collider> objToMove = new List<Collider>();
	public List<Collider> objToOverlap = new List<Collider>();
	public LayerMask mask;
	public LayerMask mask2;
	//public float newSize;//times size of the overlapbox
	public bool overlapped;
	//float size;
	float xsize;
	float ysize;
	float zsize;
	float newSize;
	Vector3 rescale;
	GameObject compObject;
	Vector3 objSize;
	//public Renderer rend;

	void Start()
	{
		overlapped = false;
	}
	void OnDrawGizmos() 
	{
		objSize = this.gameObject.transform.GetComponent<BoxCollider>().size;
		Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(this.gameObject.transform.position, objSize);
    }
	public void FixedUpdate()
	{
		int oldLayer = this.gameObject.layer;//create a temp layer
		this.gameObject.layer = 2;//ignore raycasting on the same layer type
		Collider[] overlappedColliders;
		overlappedColliders = Physics.OverlapBox(this.gameObject.transform.position, this.gameObject.transform.GetComponent<Collider>().bounds.extents, this.gameObject.transform.rotation, mask2);//detect any overlapping existing components on the component
		this.gameObject.layer = oldLayer;//revert the layer of the component to its original layer type	
		objToOverlap = new List<Collider>();
		foreach(Collider coll in overlappedColliders)
		{
			objToOverlap.Add(coll);//add the overlapped components colliders inside the list
		}
		if(objToOverlap.Count==0)
		{
			overlapped = false;//if there is no overlapping occurs, the component can be attached to the empty slot
		}
		else if(objToOverlap.Count != 0)
		{
			overlapped = true;//if overlapping between existing components and the new component occurs, the component cannot be attached to the empty slot
		}	
		if(Spawn.isAlreadyClicked == false)
		{
			Collider[] colliders;
			colliders = Physics.OverlapBox(this.gameObject.transform.position, this.gameObject.transform.GetComponent<Collider>().bounds.extents, this.gameObject.transform.rotation, mask);//detect any overlapping slots on the component
			objToMove = new List<Collider>();
			foreach(Collider col in colliders)
			{
				if(this.transform.parent != null)
				{
					objToMove.Add(col);//add the overlapped slots inside the list
					if(col.gameObject.CompareTag("Starterslots"))
					{
						col.gameObject.tag = "NewStarter";//change the original slots tag to new tag
						//col.isTrigger = true;
						//col.enabled = false;
					}
				}
				if(this.transform.parent == null)
				{
					//col.enabled = true;
					if(col.gameObject.CompareTag("NewStarter"))
					{
						//col.enabled = false;
						//col.isTrigger = false;
						col.gameObject.tag = "Starterslots";//change the original slots tag to new tag
						objToMove.Remove(col);//add the overlapped slots inside the list
					}
				}
				/*if(Spawn.clicked == true)
				{
					objToMove.Add(col);
					if(col.enabled == false)
					{
						col.gameObject.tag = "Starterslots";
						col.enabled = true;
					}
				}*/
			}
		}
	}
}
