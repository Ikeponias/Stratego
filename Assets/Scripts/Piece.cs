using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;

public class Piece : MonoBehaviour {
    public GameObject CanMoveArea;

	// Use this for initialization
	void Start () {
        // iTween.MoveTo(gameObject, iTween.Hash("x", 1f));
        // Destroy(gameObject, 3f);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Move(PieceShadow pieceShadow)
    {
        CanMoveArea closestCanMoveArea = GameObject.FindGameObjectsWithTag("CanMoveArea")
            .OrderBy(go => Vector3.Distance(go.transform.position, pieceShadow.transform.position))
            .FirstOrDefault().GetComponent<CanMoveArea>();

        int posX = closestCanMoveArea.PosX;
        int posY = closestCanMoveArea.PosY;

        iTween.MoveBy(this.gameObject, iTween.Hash("x", posX, "y", posY));

        iTween.MoveTo(pieceShadow.gameObject, iTween.Hash("x", this.transform.position.x+posX, "y", this.transform.position.y+posY));

    }
}
