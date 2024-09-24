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
            collidedCollider.gameObject.SetActive(false);
            }
        }
    }

    private void UpdateMouseIndicator()
    {
        Vector3 mousePos = inputManager.GetSelectedMapPosition();
        mouseIndicator.transform.position = mousePos;
    }

    private void instantiateObject(Vector3 position, Quaternion rotation) // Add type 'Vector3' to the 'position' parameter
    {
        string folderPath = "Turrets"; // Specify the folder path where the prefab is located
        GameObject[] prefabs; // Declare the prefabs array

        prefabs = Resources.LoadAll<GameObject>(folderPath); // Load all prefabs from the specified folder

        print("Prefabs: " + prefabs.Length);

        if (prefabs.Length > 0) // Check if the prefabs array is not empty
        {
            // int randomIndex = UnityEngine.Random.Range(0, prefabs.Length); // Generate a random index
            // GameObject prefab = prefabs[randomIndex]; // Get the prefab at the random index
            // print("Prefab: " + prefab.name);

            GameObject go = Instantiate(prefabs[1]); // Instantiate the selected prefab
            go.transform.position = position;
            go.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f); // Change double to float
            go.transform.rotation = rotation;

            
        }
        else
        {
            print("No prefabs found in the specified folder: " + folderPath);
        }
    }

    

}