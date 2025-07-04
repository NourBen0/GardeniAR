using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using Unity.XR.CoreUtils;

public class PlantPlaceManagement : MonoBehaviour
{
    [Header("AR Setup")]
    public XROrigin sessionOrigin;
    public ARRaycastManager raycastManager;
    public ARPlaneManager planeManager;

    [Header("Flowers to Place")]
    public GameObject[] flowers;

    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Vector2 touchPosition = Input.GetTouch(0).position;

            if (raycastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon))
            {
                Pose hitPose = hits[0].pose;

                // Instancier une fleur aléatoire à la position du toucher
                GameObject flowerToPlace = Instantiate(flowers[Random.Range(0, flowers.Length)]);
                flowerToPlace.transform.position = hitPose.position;
                flowerToPlace.transform.rotation = hitPose.rotation;

                // Désactiver les plans détectés
                foreach (var plane in planeManager.trackables)
                {
                    plane.gameObject.SetActive(false);
                }

                planeManager.enabled = false;
            }
        }
    }
}
