using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
 
public class Generator : MonoBehaviour{
    public Camera FirstPersonCamera;
	[SerializeField] private GameObject obj_prefab;
	private ARRaycastManager raycast_mgr;
	private List<ARRaycastHit> hitResults = new List<ARRaycastHit>();

	void Awake()
	{
        raycast_mgr = GetComponent<ARRaycastManager>();
	}

	void Update()
	{
        if (Input.touchCount > 0) {
            Touch touch = Input.GetTouch(0);
            if (touch.phase != TouchPhase.Ended) {
                return;
            }
            if (raycast_mgr.Raycast (touch.position, hitResults, TrackableType.All)) {
                GameObject generated_obj = Instantiate (obj_prefab, hitResults[0].pose.position, hitResults[0].pose.rotation);
                generated_obj.AddComponent<Rigidbody>();
                var rb = generated_obj.GetComponent<Rigidbody>();
                rb.AddForce(FirstPersonCamera.transform.TransformDirection(Random.value%3f, Random.value%3f, Random.value%3f), ForceMode.Impulse);
                // rb.isKinematic = true;
            }
        }
    }
}

/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class Generator : MonoBehaviour{
    // public Camera FirstPersonCamera;
	[SerializeField] private GameObject obj_prefab;
	private ARRaycastManager raycast_mgr;
	private List<ARRaycastHit> hitResults = new List<ARRaycastHit>();

	void Awake() // before the first frame
	{
        raycast_mgr = GetComponent<ARRaycastManager>();
	}

	void Update() // each frame
	{
        if (Input.touchCount > 0) {
            Touch touch = Input.GetTouch(0);
            //if (touch.phase == TouchPhase.Ended) {
                if (raycast_mgr.Raycast (touch.position, hitResults, TrackableType.All)) {
                    //GameObject generated_obj = Instantiate(obj_prefab, Camera.main.transform);
                    GameObject generated_obj = Instantiate(obj_prefab, hitResults[0].pose.position, Quaternion.identity);
                    // Material material = new Material(Shader.Find("DiffUse")){
                    //     color = new Color(Random.value,Random.value,Random.value)
                    // };
                    // generated_obj.GetComponent<Renderer>().material = material;
                    // generated_obj.GetComponent<Camera>().ScreenPointToSessionSpaceRay(touch.position);
                    // var rb = generated_obj.GetComponent<Rigidbody>();
                    // rb.AddForce(FirstPersonCamera.transform.TransformDirection(0, 1f, 2f), ForceMode.Impulse);
                }
            }
        //}
    }
}
*/