﻿using UnityEngine;
using System.Collections;

/// <summary>
/// This script should be attached to the card game object to display card`s rotation correctly.
/// </summary>

[ExecuteInEditMode]
public class CardRotation : MonoBehaviour {

    // Parent game object for all the card face graphics
    public RectTransform CardFront;

	// Parent game object for all the card back graphics
    public RectTransform CardBack;

    // An empty game object that is placed a bit above the face of the card, in the center of the card
    public Transform targetFacePoint;

    // 3d collider attached to the card (2d colliders like BoxCollider2D won`t work in this case)
    public Collider col;

    // if this is true, our players currently see the card Back
    private bool showingBack = false;

	private void Update () 
    {
        // Raycast from Camera to a target point on the face of the card
        // If it passes through the card`s collider, we should show the back of the card
        RaycastHit[] hits;
        hits = Physics.RaycastAll(origin: Camera.main.transform.position, 
								  direction: (targetFacePoint.position - Camera.main.transform.position).normalized, 
            					  maxDistance: (-Camera.main.transform.position + targetFacePoint.position).magnitude);
        bool passedThroughColliderOnCard = false;
        foreach (RaycastHit h in hits)
        {
            if (h.collider == col)
                passedThroughColliderOnCard = true;
        }
        //Debug.Log("TotalHits: " + hits.Length); 
        if (passedThroughColliderOnCard != showingBack)
        {
            showingBack = passedThroughColliderOnCard;
            if (showingBack)
            {
                // Show the back side
                CardFront.gameObject.SetActive(false);
                CardBack.gameObject.SetActive(true);
            }
            else
            {
                // Show the front side
                CardFront.gameObject.SetActive(true);
                CardBack.gameObject.SetActive(false);
            }

        }

	}
}
