using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementAndRotation : MonoBehaviour
{
    [SerializeField]
    public LayerMask placementLayermask;
    public GameObject[] prefabs;
    public int index;
    public string currentName;

    [SerializeField]
    private bool isPreviewActive = false;
    private GameObject previewObject;

    // Start is called before the first frame update
    void Start()
    {
        prefabs = Resources.LoadAll<GameObject>("Turrets");
        Debug.Log("There are: " + prefabs.Length + " prefabs loaded");
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(prefabs[1].name);
        HandlePreview(prefabs[index]);
    }

    void HandleRotation(string name, Quaternion originRotation)
    {
        // Assuming 'hit' is available within the context
        if (name.Length > 0)
        {
            char firstCharacter = name[9]; // Get the first character

            switch (firstCharacter)
            {
                case '1':
                    // Logic for case when first character is '1'
                    Debug.Log("First character is 1");
                    if(Input.GetKeyDown(KeyCode.R)){
                        RotatePreviewObject(originRotation, 90f, 0f, 0f);
                    }
                    break;

                case '2':
                    // Logic for case when first character is '2'
                    Debug.Log("First character is 2");
                    if(Input.GetKeyDown(KeyCode.R)){
                        RotatePreviewObject(originRotation, 90f, 0f, 0f);
                    }
                    break;

                case '3':
                    // Logic for case when first character is '3'
                    Debug.Log("First character is 3");
                    if(Input.GetKeyDown(KeyCode.R)){
                        RotatePreviewObject(originRotation, 90f, 0f, 0f);
                    }
                    break;

                case '4':
                    // Logic for case when first character is '4'
                    Debug.Log("First character is 4");
                    if(Input.GetKeyDown(KeyCode.R)){
                        RotatePreviewObject(originRotation, 0f, 0f, 90f);
                        Debug.Log("Rotating");
                    }
                    break;

                case '5':
                    // Logic for case when first character is '5'
                    Debug.Log("First character is 5");
                    if(Input.GetKeyDown(KeyCode.R)){
                        RotatePreviewObject(originRotation, 90f, 0f, 0f);
                    }
                    break;

                case '6':
                    // Logic for case when first character is '6'
                    Debug.Log("First character is 6");
                    if(Input.GetKeyDown(KeyCode.R)){
                        RotatePreviewObject(originRotation, 0f, 0f, 0f);
                    }
                    break;

                default:
                    // Logic for case when first character is not '1' through '6'
                    Debug.Log("First character is not in the range of 1-6 and is: " + name);
                    break;
            }
        }
    }

    // Rotate the preview object by a specified angle
    void RotatePreviewObject(Quaternion originRotation, float x, float y, float z)
    {
        Debug.Log("Test rotate function going");
        // Get the current rotation of the origin object
        Quaternion currentRotation = originRotation;

        // Create a new rotation that adds the specified angle on the x-axis
        Quaternion additionalRotation = Quaternion.Euler(x, y, z); // Angle on the x-axis
        Quaternion newRotation = currentRotation * additionalRotation; // Multiply the rotations

        // Set the rotation of the preview object
        if (previewObject != null)
        {
            previewObject.transform.rotation = newRotation;
        }
    }


    void HandlePreview(GameObject prefab)
    {
        Camera skyboxCamera = GameObject.FindWithTag("SkyboxCamera").GetComponent<Camera>();
        Ray ray = skyboxCamera.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, placementLayermask))
        {
            Debug.Log(hit.transform.position);
            if (!isPreviewActive)
            {
                // Create the preview object
                previewObject = Instantiate(prefab);  // Use the turretPrefab here

                SetObjectOpacity(previewObject, 0.7f);
                
                // previewObject.GetComponent<Collider>().enabled = false;  // Disable collider for preview

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

            // Move preview object to the hit point on the ground
            previewObject.transform.position = hit.transform.position;

            previewObject.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f); // Change double to float
        }

        if(currentName != hit.transform.name){
            // Apply the current rotation
            previewObject.transform.rotation = hit.transform.rotation;
            currentName = hit.transform.name;        
        }

        HandleRotation(hit.transform.name, previewObject.transform.rotation);
    }

    void SetObjectOpacity(GameObject obj, float opacity)
    {
        // Get all renderers (MeshRenderer, SkinnedMeshRenderer, etc.) from the object and its children
        Renderer[] renderers = obj.GetComponentsInChildren<Renderer>();

        foreach (Renderer renderer in renderers)
        {
            // Get the material's current color
            foreach (Material mat in renderer.materials)
            {
                Color color = mat.color;
                // Set the alpha (opacity) to the desired value (0.7 for 70% opacity)
                color.a = opacity;
                mat.color = color;

                // Ensure the shader supports transparency
                if (mat.HasProperty("_Mode"))
                {
                    mat.SetFloat("_Mode", 2);  // Set shader to Transparent mode
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
