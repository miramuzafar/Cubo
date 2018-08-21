using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlaceObject : MonoBehaviour
{
    public int sizeGridX;
    public int sizeGridZ;
    public Transform baseGrid;
    DragObjectPlace dragObjectPlace;
    public GameObject place;

    Vector3 objSize;
    private Vector3 sizePlace = Vector3.zero;
    private Vector3 gridPosition = Vector3.zero;
    private Vector3 startGridPosition = Vector3.zero;

    void Start()
    {
        dragObjectPlace = gameObject.GetComponent<DragObjectPlace>();
        baseGrid = GameObject.FindWithTag("LeftRightWall").GetComponent<Transform>();
        FillGrid();
    }
    /* void OnDrawGizmos() {
		objSize = baseGrid.gameObject.transform.GetComponent<MeshRenderer>().bounds.size;
		Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(baseGrid.gameObject.transform.position, objSize);
    }  */
    private void Values()
    {
        /* float dragObjy = dragObjectPlace.objectInDrag.gameObject.GetComponent<MeshRenderer>().bounds.size.y;
        float dragObjyz = dragObjectPlace.objectInDrag.gameObject.GetComponent<MeshRenderer>().bounds.size.z; */
        sizePlace.y = baseGrid.gameObject.GetComponent<MeshRenderer>().bounds.size.y / sizeGridX;
        sizePlace.z = baseGrid.gameObject.GetComponent<MeshRenderer>().bounds.size.z / sizeGridZ;
        /* sizePlace.y = dragObjy / sizeGridX;
        sizePlace.z = dragObjy / sizeGridZ; */
        sizePlace.x = baseGrid.gameObject.GetComponent<MeshRenderer>().bounds.size.x*1.57f;

        gridPosition.y = (baseGrid.gameObject.GetComponent<MeshRenderer>().bounds.size.y / 2) - (sizePlace.y / 2);
        gridPosition.z = (baseGrid.gameObject.GetComponent<MeshRenderer>().bounds.size.z / 2) - (sizePlace.z / 2);
        gridPosition.x = baseGrid.transform.position.x;

        startGridPosition = gridPosition;
    }
    public void FillGrid()
    {
        Values();
        for (int x = 0; x < sizeGridX; x++)
        {
            for (int z = 0; z < sizeGridZ; z++)
            {
                GameObject newPlace = Instantiate(place) as GameObject;
                newPlace.transform.localScale = sizePlace;
                newPlace.transform.position = gridPosition;

                if (z >= (sizeGridZ - 1))
                    gridPosition.z = startGridPosition.z;
                else
                    gridPosition.z -= sizePlace.z;

                newPlace.transform.parent = transform;
                newPlace.SetActive(true);
            }
            gridPosition.y -= sizePlace.y;
        }
    }
    public void Destroy()
    {
        PlaceBehaviour[] places = GetComponentsInChildren<PlaceBehaviour>();

        for (int i = 0; i < places.Length; i++)
        {
            Destroy(places[i].gameObject);
        }
    }
}
