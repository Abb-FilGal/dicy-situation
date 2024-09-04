using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlacementSystem : MonoBehaviour
{
    [SerializeField]
    private InputManager inputManager;

    [SerializeField]
    private GameObject mouseIndicator;
    public Collider mouseCollider;

    private void Start()
    {
        mouseCollider = mouseIndicator.GetComponent<Collider>();
        print("MouseCollider: " + mouseCollider);
    }

    private void Update()
    {
        UpdateMouseIndicator();
        if (Input.GetMouseButtonDown(0))
        {
            Collider[] colliders = Physics.OverlapBox(mouseCollider.bounds.center, mouseCollider.bounds.extents, mouseCollider.transform.rotation, inputManager.placementLayermask);
            if (colliders.Length > 0)
            {
            Collider collidedCollider = colliders[0];
            print("MouseCollider: " + "Hit");
            instantiateObject(collidedCollider.transform.position, collidedCollider.transform.rotation); // Pass the position of the object the mouse has collided with
            }
        }
            }

    private void UpdateMouseIndicator()
    {
        Vector3 mousePos = inputManager.GetSelectedMapPosition();
        mouseIndicator.transform.position = mousePos;
    }

    private void instantiateObject(Vector3 position,Quaternion rotation) // Add type 'Vector3' to the 'position' parameter
    {
        GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
        go.transform.position = position;
        go.transform.localScale = new Vector3(0.8f, 1.6f, 0.8f); // Change double to float
        go.transform.rotation = rotation;
        //go.transform.parent = parent;
    }
}