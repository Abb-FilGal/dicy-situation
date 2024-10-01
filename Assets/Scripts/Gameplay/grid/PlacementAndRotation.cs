using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementAndRotation : MonoBehaviour
{
    [SerializeField]
    public LayerMask placementLayermask;
    public GameObject[] prefabs;
    public string currentName;
    public GameObject prefab;

    public bool isPreviewActive = false;
    public bool hasPlaced = false;  // Ensures the object is placed
    public GameObject previewObject;

    void Start()
    {
        prefabs = Resources.LoadAll<GameObject>("Turrets");  // Ensure this path is correct
        Debug.Log("There are " + prefabs.Length + " prefabs loaded.");

        if (prefabs.Length == 0)
        {
            Debug.LogError("No prefabs found in the 'Turrets' folder.");
        }
        prefab = prefabs[0];
    }


    // Public function to start the tower placement preview, called from GameManager
    public void StartTowerPlacement()
    {
        int index = Random.Range(0, prefabs.Length);
        Debug.Log("Index is: " + index);
        prefab = prefabs[index];
        // isPreviewActive = true;  // Activate preview mode
        hasPlaced = false;  // Reset hasPlaced for a new placement
    }

    void Update()
    {
        if (!hasPlaced)
        {
            HandlePreview();  // Handle the preview and placement while the tower is not placed
            // Debug.Log("Trying to handle preview");
        }
    }

    public void HandlePreview()
    {
        Camera skyboxCamera = GameObject.FindWithTag("SkyboxCamera").GetComponent<Camera>();
        Ray ray = skyboxCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Perform the raycast and ensure a valid hit
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, placementLayermask))
        {
            // Debug.Log(hit.transform.position);

            // Instantiate the preview object if it doesn't exist
            if (!isPreviewActive)
            {   
                Debug.Log("trying to instantiate previewObject");
                previewObject = Instantiate(prefab);  // Create the preview object

                SetObjectOpacity(previewObject, 0.7f);

                // Disable all MonoBehaviour scripts on the preview object
                MonoBehaviour[] allScripts = previewObject.GetComponentsInChildren<MonoBehaviour>();
                foreach (MonoBehaviour script in allScripts)
                {
                    script.enabled = false;
                }

                isPreviewActive = true;
                currentName = hit.transform.name;

                previewObject.transform.rotation = hit.transform.rotation;
            }

            // Check if the current object name has changed and if the previewObject is not null
            if (previewObject != null && hit.transform != null && currentName != hit.transform.name)
            {
                // Apply the current rotation
                previewObject.transform.rotation = hit.transform.rotation;
                currentName = hit.transform.name;
            }

            // Move preview object to the hit position
            if (previewObject != null)
            {
                previewObject.transform.position = hit.transform.position;
                previewObject.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f); // Set scale
            }

            // Handle rotation on key press
            if (Input.GetKeyDown(KeyCode.R))
            {
                RotatePreviewObject(previewObject.transform.rotation, 0f, 90f, 0f);
            }

            // Handle object placement on key press (e.g., pressing Q)
            if (Input.GetMouseButtonDown(0))
            {
                PlaceObject();
            }
        }
    }

    // Method to handle placing the object
    void PlaceObject()
    {
        if (previewObject != null)
        {
            // Set opacity back to 100%
            SetObjectOpacity(previewObject, 1.0f);

            foreach( Transform child in previewObject.transform){child.gameObject.SetActive(false);}

            // Re-enable all MonoBehaviour scripts
            MonoBehaviour[] allScripts = previewObject.GetComponentsInChildren<MonoBehaviour>();
            foreach (MonoBehaviour script in allScripts)
            {
                script.enabled = true;
            }

            hasPlaced = true;  // Mark object as placed
            isPreviewActive = false;  // Disable preview mode
        }
    }

    // Method to rotate the preview object
    void RotatePreviewObject(Quaternion originRotation, float x, float y, float z)
    {
        Quaternion additionalRotation = Quaternion.Euler(x, y, z);
        previewObject.transform.rotation = originRotation * additionalRotation;
    }

    // Method to change the opacity of the object
    void SetObjectOpacity(GameObject obj, float opacity)
    {
        Renderer[] renderers = obj.GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            foreach (Material mat in renderer.materials)
            {
                Color color = mat.color;
                color.a = opacity;
                mat.color = color;

                if (mat.HasProperty("_Mode"))
                {
                    mat.SetFloat("_Mode", 2);
                    mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                    mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                    mat.SetInt("_ZWrite", 0);
                    mat.DisableKeyword("_ALPHATEST_ON");
                    mat.EnableKeyword("_ALPHABLEND_ON");
                    mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                    mat.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
                }
            }
        }
    }
}
