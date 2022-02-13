using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class MarkerMaker : MonoBehaviour{
    [SerializeField] private GameObject[] marker_prefab; // marker
    [SerializeField] private ARTrackedImageManager tracked_image_mgr;
    private readonly Dictionary<string, GameObject> marker_dict = new Dictionary<string, GameObject>();


    void Start(){
        tracked_image_mgr.trackedImagesChanged += OnTrackedImagesChanged;

        //辞書を作る 画像の名前とARオブジェクトのPrefabを紐づける
        for (var i = 0; i < marker_prefab.Length; i++)
        {
            var arPrefab = Instantiate(marker_prefab[i]);
            marker_dict.Add(tracked_image_mgr.referenceLibrary[i].name, arPrefab);
            arPrefab.SetActive(false);
        }
    }
    private void OnDisable(){
        tracked_image_mgr.trackedImagesChanged -= OnTrackedImagesChanged;
    }
    private void ActivateARObject(ARTrackedImage trackedImage){
        //認識した画像マーカーの名前を使って辞書から任意のオブジェクトを引っ張り出す
        var arObject = marker_dict[trackedImage.referenceImage.name];
        var imageMarkerTransform = trackedImage.transform;

        //位置合わせ
        var markerFrontRotation = imageMarkerTransform.rotation * Quaternion.Euler(90f, 0f, 0f);
        arObject.transform.SetPositionAndRotation(imageMarkerTransform.transform.position, markerFrontRotation);
        arObject.transform.SetParent(imageMarkerTransform);

        //トラッキングの状態に応じてARオブジェクトの表示を切り替え
        //arObject.SetActive(trackedImage.trackingState == TrackingState.Tracking);
    }
    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (var trackedImage in eventArgs.added)
        {
            ActivateARObject(trackedImage);
        }

        foreach (var trackedImage in eventArgs.updated)
        {
            ActivateARObject(trackedImage);
        }
    }

    void Update(){
        
    }
}
