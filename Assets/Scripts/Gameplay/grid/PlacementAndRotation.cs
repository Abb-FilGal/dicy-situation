using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementAndRotation : MonoBehaviour
{
    [SerializeField]
    public LayerMask placementLayermask;
    public GameObject[] prefabs;
    public int index;
    public string currentName = "start";
    public RaycastHit curentObject;

    [SerializeField]
    private bool isPreviewActive = false;
    private GameObject previewObject;

    void Start()
    {
        prefabs = Resources.LoadAll<GameObject>("Turrets");
        Debug.Log("There are: " + prefabs.Length + " prefabs loaded");
    }

    void Update()
    {
        // Debug.Log("Test1");
        HandlePreview(prefabs[index]);
        HandlePlacement();  // Call the new HandlePlacement function to handle placement
        // Debug.Log("TESTs");
    }

    void HandleRotation(string name, Quaternion originRotation)
    {
        if (name.Length > 0)
        {
            char firstCharacter = name[9]; // Get the first character

            switch (firstCharacter)
            {
                case '1':
                    Debug.Log("First character is 1");
                    
                    break;

                case '2':
                    Debug.Log("First character is 2");
                    if(Input.GetKeyDown(KeyCode.R)){
                        RotatePreviewObject(originRotation, 90f, 0f, 0f);
                    }
                    break;

                case '3':
                    Debug.Log("First character is 3");
                    if(Input.GetKeyDown(KeyCode.R)){
                        RotatePreviewObject(originRotation, 90f, 0f, 0f);
                    }
                    break;

                case '4':
                    Debug.Log("First character is 4");
                    if(Input.GetKeyDown(KeyCode.R)){
                        RotatePreviewObject(originRotation, 0f, 90f, 0f);
                    }
                    break;

                case '5':
                    Debug.Log("First character is 5");
                    if(Input.GetKeyDown(KeyCode.R)){
                        RotatePreviewObject(originRotation, 0f, 90f, 0f);
                    }
                    break;

                case '6':
                    Debug.Log("First character is 6");
                    if(Input.GetKeyDown(KeyCode.R)){
                        RotatePreviewObject(originRotation, 0f, 90f, 0f);
                    }
                    break;

                default:
                    Debug.Log("First character is not in the range of 1-6 and is: " + name);
                    break;
            }
        }
    }

    void RotatePreviewObject(Quaternion originRotation, float x, float y, float z)
    {
        Debug.Log("Test rotate function going");
        Quaternion currentRotation = originRotation;
        Quaternion additionalRotation = Quaternion.Euler(x, y, z); 
        Quaternion newRotation = currentRotation * additionalRotation;

        if (previewObject != null)
        {
            previewObject.transform.rotation = newRotation;
        }
    }

    void ExportHit(RaycastHit hitmarker){
        curentObject = hitmarker;
        Debug.Log(curentObject); 
    }

void HandlePreview(GameObject prefab)
{
    Camera skyboxCamera = GameObject.FindWithTag("SkyboxCamera").GetComponent<Camera>();
    Ray ray = skyboxCamera.ScreenPointToRay(Input.mousePosition);
    RaycastHit hit;

    // Perform the raycast and ensure a valid hit
    if (Physics.Raycast(ray, out hit, Mathf.Infinity, placementLayermask))
    {
        Debug.Log(hit.transform.position);

        // Instantiate the preview object if it doesn't exist
        if (!isPreviewActive)
        {
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

        if(Input.GetKeyDown(KeyCode.R)){
            RotatePreviewObject(previewObject.transform.rotation, 0f, 90f, 0f);
        }
    }
}


    void HandlePlacement()
    {
        if (Input.GetKeyDown(KeyCode.Q) && isPreviewActive)
        {
            // Lock the object in place
            SetObjectOpacity(previewObject, 1.0f);  // Reset opacity to 100%
            EnableAllScripts(previewObject);  // Enable all MonoBehaviour scripts



            isPreviewActive = false;  // Reset for the next preview
            previewObject = null;  // Clear the current previewObject

            Debug.Log("Object placed and ready for the next object.");
        }
    }

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

    void EnableAllScripts(GameObject obj)
    {
        // Re-enable all previously disabled MonoBehaviour scripts
        MonoBehaviour[] allScripts = obj.GetComponentsInChildren<MonoBehaviour>();
        foreach (MonoBehaviour script in allScripts)
        {
            script.enabled = true;
        }
    }
        void DisableAllScripts(GameObject obj)
    {
        // Re-enable all previously disabled MonoBehaviour scripts
        MonoBehaviour[] allScripts = obj.GetComponentsInChildren<MonoBehaviour>();
        foreach (MonoBehaviour script in allScripts)
        {
            script.enabled = false;
        }
    }
}
