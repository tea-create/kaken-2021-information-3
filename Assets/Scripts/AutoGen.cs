using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace UnityEngine.XR.ARFoundation.h1j1k1{
    public class AutoGen : MonoBehaviour{
        public Camera FirstPersonCamera;
        ARSessionOrigin m_SessionOrigin;
        [SerializeField]
        GameObject m_RegionPrefab;

        /// <summary>
        /// Get or set the prefab which will be instantiated at each detected face region.
        /// </summary>
        public GameObject regionPrefab
        {
            get { return m_RegionPrefab; }
            set { m_RegionPrefab = value; }
        }
        [SerializeField]
        int m_Counter;
        public int counter
        {
            get { return m_Counter; }
            set
            {
                m_Counter = value;
            }
        }


        // private List<ARRaycastHit> hitResults = new List<ARRaycastHit>();

        void Awake()
        {
            m_SessionOrigin = GetComponent<ARSessionOrigin>();
            // raycast_mgr = GetComponent<ARRaycastManager>();
        }

        void Update()
        {
            if( (Random.Range(1,m_Counter)) <= 1){
                GameObject generated_obj;
                float vx, vy, vz;
                float px, py, pz;
                Vector3 v, p;

                vx = Random.Range(-1F,+ 1F);
                vy =Random.Range(-1F,+ 1F);
                vz = Random.Range(-1F,+ 1F);
                v = new Vector3(
                    vx,
                    vy,
                    vz
                );
                
                px = Random.Range(0F,+7F);
                py = Random.Range(-1F,+2F);
                pz = Random.Range(-3.5F,+3.5F);
                p = new Vector3(
                    px,
                    py,
                    pz
                );

                // v = v * new Vector2( 1, 0 );
                generated_obj = Instantiate (m_RegionPrefab, p, Quaternion.identity);
                generated_obj.AddComponent<Rigidbody>();
                generated_obj.transform.localRotation = Quaternion.Euler(vz,vx,vy);
                var rb = generated_obj.GetComponent<Rigidbody>();
                rb.AddForce(FirstPersonCamera.transform.TransformDirection(vx, vy, vz), ForceMode.Impulse);
                
                // rb.isKinematic = true;


                // if (!regionGos.TryGetValue(regionType, out go))
                // {
                //     go = Instantiate(m_RegionPrefab, m_SessionOrigin.trackablesParent);
                //     // regionGos.Add(regionType, go);
                //         go.AddComponent<Rigidbody>();
                //     var rb = go.GetComponent<Rigidbody>();
                //     rb.AddForce(v, ForceMode.Impulse);
                //     // rb.AddForce(v, ForceMode.Impulse);
                // }
                Destroy(generated_obj, 7F/v.magnitude);
            }


            // if (Input.touchCount > 0) {
            //     Touch touch = Input.GetTouch(0);
            //     if (touch.phase != TouchPhase.Ended) {
            //         return;
            //     }
            //     if (raycast_mgr.Raycast (touch.position, hitResults, TrackableType.All)) {
            //         GameObject generated_obj = Instantiate (obj_prefab, hitResults[0].pose.position, hitResults[0].pose.rotation);
            //         generated_obj.AddComponent<Rigidbody>();
            //         var rb = generated_obj.GetComponent<Rigidbody>();
            //         rb.AddForce(FirstPersonCamera.transform.TransformDirection(Random.value%3f, Random.value%3f, Random.value%3f), ForceMode.Impulse);
            //         // rb.isKinematic = true;
            //     }
            // }
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