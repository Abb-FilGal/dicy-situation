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

    private void Update()
    {
        UpdateMouseIndicator();
        if (Input.GetMouseButtonDown(0))
        {
            print("Mouse clicked at coords: " + inputManager.GetSelectedMapPosition());
            instantiateObject();
        }
    }
    private void UpdateMouseIndicator()
        {
            Vector3 mousePos = inputManager.GetSelectedMapPosition();
            mouseIndicator.transform.position = mousePos;
        }

    private void instantiateObject()
    {
        GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
        go.transform.position = inputManager.GetSelectedMapPosition();
    }
}