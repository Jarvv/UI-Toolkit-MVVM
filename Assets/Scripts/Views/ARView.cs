using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace Views
{
	public class ARView : View
	{
		private ARRaycastManager _raycastManager;

		private GameObject _furniturePrefab;
		private GameObject _previewPrefab;

		private GameObject _previewInstance;
		private GameObject _furnitureInstance;

		private GameObject _planeObject;
		private bool _objectHasSpawned = false;
		private bool _previewHasSpawned = false;
		private bool _tapPlaceReady = false;

		private Vector3 centre = new Vector3(Screen.width / 2, Screen.height / 2, 0f);
		private static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();

		private List<GameObject> _furnitureInstances = new List<GameObject>();

		private void Awake()
		{
			_raycastManager = GetComponent<ARRaycastManager>();
		}

		private void OnEnable()
		{
			_planeObject = GameObject.CreatePrimitive(PrimitiveType.Plane);
			_planeObject.AddComponent(typeof(BoxCollider));
			_planeObject.GetComponent<MeshRenderer>().enabled = false;
			_planeObject.GetComponent<BoxCollider>().isTrigger = true;
			_planeObject.layer = LayerMask.NameToLayer("ARPlane");
			_planeObject.SetActive(false);

			AREvents.TouchMove += OnTouchMove;
			AREvents.TouchRotate += OnTouchRotate;
			AREvents.ARFurnitureTapped += OnARFurnitureTapped;
		}

		private void OnDisable()
		{
			_furniturePrefab = null;
			_previewPrefab = null;
			_objectHasSpawned = false;
			_previewHasSpawned = false;
			_tapPlaceReady = false;

			DestroyImmediate(_planeObject);
			if (_previewInstance) DestroyImmediate(_previewInstance);
			_furnitureInstance = null;

			_furnitureInstances.ForEach(item => DestroyImmediate(item));

			AREvents.TouchMove -= OnTouchMove;
			AREvents.TouchRotate -= OnTouchRotate;
			AREvents.ARFurnitureTapped -= OnARFurnitureTapped;
		}

		private void Update()
		{
			if (_objectHasSpawned || !_tapPlaceReady) return;

			Camera camera = Camera.main;
			Ray ray = camera.ScreenPointToRay(centre);

			ProcessARPlaneHit(ray);
			ProcessPhysicsRaycast(ray, camera);
		}

		private void ProcessARPlaneHit(Ray ray)
		{
			if (_raycastManager.Raycast(ray, s_Hits, TrackableType.PlaneWithinPolygon))
			{
				Pose hitPose = s_Hits[0].pose;
				_planeObject.transform.position = hitPose.position;
				_planeObject.SetActive(true);
			}
		}

		private void ProcessPhysicsRaycast(Ray ray, Camera camera)
		{
			if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, LayerMask.GetMask("ARPlane")))
			{
				UpdatePreviewPosition(hit.point);
				MakePrefabFaceCamera(_previewInstance);

				float dist = Vector3.Distance(_previewInstance.transform.position, camera.transform.position);
				UpdatePreviewVisibility(dist);

				if (!GetTouch() || !_previewInstance.activeSelf) return;

				PlaceFurniture();
			}
		}

		private void UpdatePreviewPosition(Vector3 hitPoint)
		{
			if (!_previewHasSpawned)
			{
				_previewHasSpawned = true;
				_previewInstance = Instantiate(_previewPrefab);
				AREvents.TrackAndPlace(TrackAndPlaceEvents.PREVIEW_PLACED);
			}

			_previewInstance.transform.position = hitPoint;
		}

		private void UpdatePreviewVisibility(float distance)
		{
			bool isActive = true;
			TrackAndPlaceEvents eventType = TrackAndPlaceEvents.JUST_RIGHT;

			if (distance < 1.5f)
			{
				isActive = false;
				eventType = TrackAndPlaceEvents.TOO_CLOSE;
			}
			else if (distance > 5f)
			{
				isActive = false;
				eventType = TrackAndPlaceEvents.TOO_FAR;
			}

			_previewInstance.SetActive(isActive);
			AREvents.TrackAndPlace(eventType);
		}

		private void PlaceFurniture()
		{
			_objectHasSpawned = true;
			_furnitureInstance = Instantiate(_furniturePrefab, _previewInstance.transform.position, _previewInstance.transform.rotation, transform);
			_furnitureInstances.Add(_furnitureInstance);
			Handheld.Vibrate();
			Destroy(_previewInstance);
			AREvents.TrackAndPlace(TrackAndPlaceEvents.PREFAB_PLACED);
		}

		public void SetTapPlacePrefabs(GameObject preview, GameObject prefab)
		{
			_previewPrefab = preview;
			_furniturePrefab = prefab;
			StartCoroutine(WaitToReady());
		}

		private IEnumerator WaitToReady()
		{
			yield return null;
			_tapPlaceReady = true;
		}

		public void ConfirmFurniturePlacement()
		{
			_furniturePrefab = null;
			_previewPrefab = null;
			_objectHasSpawned = false;
			_previewHasSpawned = false;
			_tapPlaceReady = false;

			_furnitureInstance = null;
		}

		private void MakePrefabFaceCamera(GameObject prefab)
		{
			prefab.transform.rotation = Quaternion.LookRotation(prefab.transform.position - Camera.main.transform.position);
			prefab.transform.transform.localEulerAngles = new Vector3(0, prefab.transform.localEulerAngles.y, 0);
		}

		private void OnTouchRotate(float rotateValue)
		{
			if (!_furnitureInstance) return;

			Vector3 rotationDeg = Vector3.zero;
			rotationDeg.y = -rotateValue;
			_furnitureInstance.transform.rotation *= Quaternion.Euler(rotationDeg);
		}

		private void OnTouchMove(Vector3 fingerPos, TouchPhase touchPhase)
		{
			if (!_furnitureInstance) return;

			if (touchPhase == TouchPhase.Stationary)
			{
				Ray ray = Camera.main.ScreenPointToRay(fingerPos);
				if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, LayerMask.GetMask("ARPlane")))
				{
					_furnitureInstance.transform.position = hit.point;
				}
			}
		}

		private void OnARFurnitureTapped(GameObject furniture)
		{
			if (_tapPlaceReady) return;

			_furnitureInstance = furniture;

			AREvents.EditFurniturePosition();
		}

		private bool GetTouch()
		{
			if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
				return true;
			else
				return false;
		}

		// Editor functions
		[ContextMenu("TapPlace")]
		public void TapPlaceFromMenu()
		{
			StartCoroutine(TapPlaceEditor());
		}

		private IEnumerator TapPlaceEditor()
		{
			yield return new WaitForSeconds(1);
			UpdatePreviewPosition(new Vector3(0, -1, 3));
			yield return new WaitForSeconds(1);
			PlaceFurniture();
		}
	}

	public enum TrackAndPlaceEvents
	{
		TOO_CLOSE,
		TOO_FAR,
		JUST_RIGHT,
		PREVIEW_PLACED,
		PREFAB_PLACED
	}
}
