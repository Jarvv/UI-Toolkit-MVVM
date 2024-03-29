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
			if (!_objectHasSpawned && _tapPlaceReady)
			{
				Ray ray = Camera.main.ScreenPointToRay(centre);

				if (_raycastManager.Raycast(ray, s_Hits, TrackableType.PlaneWithinPolygon))
				{
					Pose hitPose = s_Hits[0].pose;
					_planeObject.transform.position = hitPose.position;
					_planeObject.SetActive(true);
				}

				if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, LayerMask.GetMask("ARPlane")))
				{
					if (!_previewHasSpawned)
					{
						_previewHasSpawned = true;
						_previewInstance = Instantiate(_previewPrefab);
						_previewInstance.transform.position = hit.point;
						AREvents.TrackAndPlace(TrackAndPlaceEvents.PREFAB_PLACED);
					}
					else
					{
						_previewInstance.transform.position = hit.point;
					}

					MakePrefabFaceCamera(_previewInstance);

					float dist = Vector3.Distance(_previewInstance.transform.position, Camera.main.transform.position);

					if (dist < 1.5)
					{
						_previewInstance.SetActive(false);
						AREvents.TrackAndPlace(TrackAndPlaceEvents.TOO_CLOSE);
					}
					else if (dist > 5)
					{
						_previewInstance.SetActive(false);
						AREvents.TrackAndPlace(TrackAndPlaceEvents.TOO_FAR);
					}
					else
					{
						_previewInstance.SetActive(true);
						AREvents.TrackAndPlace(TrackAndPlaceEvents.JUST_RIGHT);
					}
				}

				if (_previewHasSpawned && _previewInstance.activeSelf)
				{
					_objectHasSpawned = true;
					_furnitureInstance = Instantiate(_furniturePrefab, _previewInstance.transform.position, _previewInstance.transform.rotation, transform);
					_furnitureInstances.Add(_furnitureInstance);
					Handheld.Vibrate();
					Destroy(_previewInstance);
					AREvents.TrackAndPlace(TrackAndPlaceEvents.PREFAB_PLACED);
				}
			}
		}

		public void SetTapPlacePrefabs(GameObject preview, GameObject prefab)
		{
			_previewPrefab = preview;
			_furniturePrefab = prefab;
			_tapPlaceReady = true;
		}

		public void ConfirmFurniturePlacement()
		{
			_furniturePrefab = null;
			_previewPrefab = null;
			_objectHasSpawned = false;
			_previewHasSpawned = false;
			_tapPlaceReady = false;
		}


		[ContextMenu("TapPlace")]
		public void TapPlaceFromMenu()
		{
			StartCoroutine(TapPlaceEditor());
		}

		private IEnumerator TapPlaceEditor()
		{
			yield return new WaitForSeconds(1);
			_previewInstance = Instantiate(_previewPrefab, new Vector3(0, -1, 3), new Quaternion());
			AREvents.TrackAndPlace(TrackAndPlaceEvents.PREVIEW_PLACED);
			yield return new WaitForSeconds(1);
			Destroy(_previewInstance);
			_furnitureInstance = Instantiate(_furniturePrefab, _previewInstance.transform.position, _previewInstance.transform.rotation, transform);
			AREvents.TrackAndPlace(TrackAndPlaceEvents.PREFAB_PLACED);
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
