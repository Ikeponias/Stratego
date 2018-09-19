using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;

public class PieceShadow : MonoBehaviour {

    public GameObject ParentPiece;
    public GameObject CanMoveArea;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnMouseDown()
    {
        GameObject g = Instantiate(
            CanMoveArea,
            new Vector2(ParentPiece.transform.position.x, ParentPiece.transform.position.y),
            Quaternion.identity
        );

        g.transform.parent = ParentPiece.transform;
    }

    void OnMouseDrag()
    {
        GameObject closestCanMoveArea = GameObject.FindGameObjectsWithTag("CanMoveArea")
            .OrderBy(go => Vector3.Distance(go.transform.position, this.transform.position))
            .FirstOrDefault();

        GameObject[] notClosestCanMoveAreas = 
            (GameObject [])GameObject.FindGameObjectsWithTag("CanMoveArea")
            .Except(new GameObject[]{ closestCanMoveArea }).ToArray();


        foreach(GameObject g in notClosestCanMoveAreas) {
            g.GetComponent<MeshRenderer>().material.color = new Color(0, 0, 0);
        }

        MeshRenderer closestMeshRenderer = closestCanMoveArea.GetComponent<MeshRenderer>();
        closestMeshRenderer.material.color = new Color(0, 0, 255);
    }

    void OnMouseUp()
    {
        Piece piece = ParentPiece.GetComponent<Piece>();
        piece.Move(this);

        GameObject[] canMoveAreas = GameObject.FindGameObjectsWithTag("CanMoveArea");
        GameObject canMoveAreaParent = GameObject.FindGameObjectsWithTag("CanMoveAreaParent").FirstOrDefault();

        foreach (GameObject canMoveArea in canMoveAreas)
        {
            Destroy(canMoveArea);
        }

        Destroy(canMoveAreaParent);
    }
}
