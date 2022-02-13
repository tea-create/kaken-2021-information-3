using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace UnityEngine.XR.ARFoundation.h1j1k1
{
    #if UNITY_ANDROID && !UNITY_EDITOR // 一部を特化するためにiPhone対応切りました
    using UnityEngine.XR.ARCore;
    #endif

    
    [RequireComponent(typeof(ARFaceManager))]
    [RequireComponent(typeof(ARSessionOrigin))]
    public class ARCoreFaceRegionManager : MonoBehaviour
    {
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

        ARFaceManager m_FaceManager;

        ARSessionOrigin m_SessionOrigin;

    #if UNITY_ANDROID && !UNITY_EDITOR
        NativeArray<ARCoreFaceRegionData> m_FaceRegions;

        Dictionary<TrackableId, Dictionary<ARCoreFaceRegion, GameObject>> m_InstantiatedPrefabs;
    #endif
    [SerializeField]
        int m_Counter;
        /// <summary>
        /// The rotation the content should appear to have.
        /// </summary>
        public int counter
        {
            get { return m_Counter; }
            set { m_Counter = value; }
        }

        // Start is called before the first frame update
        void Start()
        {
            m_FaceManager = GetComponent<ARFaceManager>();
            m_SessionOrigin = GetComponent<ARSessionOrigin>();
    #if UNITY_ANDROID && !UNITY_EDITOR
            m_InstantiatedPrefabs = new Dictionary<TrackableId, Dictionary<ARCoreFaceRegion, GameObject>>();
    #endif
        }

        // Update is called once per frame
        void Update()
        {
    #if UNITY_ANDROID && !UNITY_EDITOR
            var subsystem = (ARCoreFaceSubsystem)m_FaceManager.subsystem;
            if (subsystem == null)
                return;

            foreach (var face in m_FaceManager.trackables)
            {
                Dictionary<ARCoreFaceRegion, GameObject> regionGos;
                if (!m_InstantiatedPrefabs.TryGetValue(face.trackableId, out regionGos))
                {
                    regionGos = new Dictionary<ARCoreFaceRegion, GameObject>();
                    m_InstantiatedPrefabs.Add(face.trackableId, regionGos);
                }

                subsystem.GetRegionPoses(face.trackableId, Allocator.Persistent, ref m_FaceRegions);

                var regionType = m_FaceRegions[0].region;
                if(regionType != 0) continue;
                if( (Random.Range(1,m_Counter)) <= 1){
                    GameObject go;
                    Vector3 v = new Vector3(
                        m_FaceRegions[0].pose.rotation.x +Random.Range(-0.03F,+0.03F), //+ (Random.value - 0.5f)/2,
                        m_FaceRegions[0].pose.rotation.y +Random.Range(-0.02F,+0.03F), //+ (Random.value - 0.5f)/2,
                        m_FaceRegions[0].pose.rotation.z +Random.Range(-0.03F,+0.03F) //+ (Random.value - 0.5f)/2
                    );
                    
                    // v = v * new Vector2( 1, 0 );
                    if (!regionGos.TryGetValue(regionType, out go))
                    {
                        go = Instantiate(m_RegionPrefab, m_SessionOrigin.trackablesParent);
                        // regionGos.Add(regionType, go);
                            go.AddComponent<Rigidbody>();
                        var rb = go.GetComponent<Rigidbody>();
                        rb.AddForce(v, ForceMode.Impulse);
                        // rb.AddForce(v, ForceMode.Impulse);
                    }

                    go.transform.localPosition = m_FaceRegions[0].pose.position;
                    go.transform.localRotation = m_FaceRegions[0].pose.rotation;
                    go.transform.Translate(+Random.Range(-0.02F,+0.02F),-0.07f+Random.Range(-0.02F,+0.02F),+Random.Range(-0.02F,+0.02F));
                    // go.transform.Translate(+Random.Range(-30,30),-30+Random.Range(-30,30),+Random.Range(-30,30));
                    Destroy(go, 10);


                    // GameObject go;
                    // if (!regionGos.TryGetValue(regionType, out go)){
                    //     go = Instantiate(m_RegionPrefab, m_SessionOrigin.trackablesParent);
                    //     regionGos.Add(regionType, go);

                    //     go.AddComponent<Rigidbody>();
                    //     var rb = go.GetComponent<Rigidbody>();
                    //     rb.AddForce(0,0,1f, ForceMode.Impulse);
                    //     regionGos.Add(regionType, go);
                    // }

                    // go.transform.localPosition = m_FaceRegions[i].pose.position;
                    // go.transform.localRotation = m_FaceRegions[i].pose.rotation;
                }


                // for (int i = 0; i < m_FaceRegions.Length; ++i)
                // {
                //     var regionType = m_FaceRegions[i].region;
                //     if(regionType != 0) continue;
                //     if( ((int)Random.Range(0,m_Counter)) - 1 < 0){
                //         GameObject go;
                //         Vector3 v = new Vector3(
                //             m_FaceRegions[i].pose.rotation.x, //+ (Random.value - 0.5f)/2,
                //             m_FaceRegions[i].pose.rotation.y, //+ (Random.value - 0.5f)/2,
                //             m_FaceRegions[i].pose.rotation.z //+ (Random.value - 0.5f)/2
                //         );
                //         v = v * new Vector2( 0, 5 );
                //         if (!regionGos.TryGetValue(regionType, out go))
                //         {
                //             go = Instantiate(m_RegionPrefab, m_SessionOrigin.trackablesParent);
                //             // regionGos.Add(regionType, go);
                //                 go.AddComponent<Rigidbody>();
                //             var rb = go.GetComponent<Rigidbody>();
                //             rb.AddForce(v, ForceMode.Impulse);
                //             // rb.AddForce(v, ForceMode.Impulse);
                //         }

                //         go.transform.localPosition = m_FaceRegions[i].pose.position;
                //         go.transform.localRotation = m_FaceRegions[i].pose.rotation;

                //     }

                //     // GameObject go;
                //     // if (!regionGos.TryGetValue(regionType, out go)){
                //     //     go = Instantiate(m_RegionPrefab, m_SessionOrigin.trackablesParent);
                //     //     regionGos.Add(regionType, go);

                //     //     go.AddComponent<Rigidbody>();
                //     //     var rb = go.GetComponent<Rigidbody>();
                //     //     rb.AddForce(0,0,1f, ForceMode.Impulse);
                //     //     regionGos.Add(regionType, go);
                //     // }

                //     // go.transform.localPosition = m_FaceRegions[i].pose.position;
                //     // go.transform.localRotation = m_FaceRegions[i].pose.rotation;
                // }
            }
    #endif
        }

    }
}