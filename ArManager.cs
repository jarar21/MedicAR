using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARTrackedImageManager))]
public class ArManager : MonoBehaviour
{
    public ARTrackedImageManager imageManager;

    public GameObject[] PrefabsToSpawn;

    private readonly Dictionary<string, GameObject> _InstantiatedPrefabs= new Dictionary<string, GameObject>();

    private void OnEnable()
    {
        imageManager.trackedImagesChanged += OnTrackedImageChanged;
    }

    private void OnDisable()
    {
        imageManager.trackedImagesChanged -= OnTrackedImageChanged;
    }

    void  OnTrackedImageChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (var trackedImage in eventArgs.added)
        {
            var img = trackedImage.referenceImage.name;

            foreach (var currentPrefab in PrefabsToSpawn)
            {
                if (string.Compare(img,currentPrefab.name,StringComparison.OrdinalIgnoreCase)==0 && !_InstantiatedPrefabs.ContainsKey(img))
                {
                    var NewPrefab = Instantiate(currentPrefab, trackedImage.transform);
                    _InstantiatedPrefabs[img] = NewPrefab;
                }
            }
        }


        foreach (var trackedImage in eventArgs.updated)
        {
            _InstantiatedPrefabs[trackedImage.referenceImage.name].SetActive(trackedImage.trackingState==TrackingState.Tracking);
        }


        foreach (var trackedImage in eventArgs.removed)
        {
            Destroy(_InstantiatedPrefabs[trackedImage.referenceImage.name]);

            _InstantiatedPrefabs.Remove(trackedImage.referenceImage.name);
        }
    }
}
