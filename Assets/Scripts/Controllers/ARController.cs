using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using Views;

namespace Controllers
{
	public class ARController : Controller
	{
		[Header("AR")]
		[SerializeField] private ARSession _arSession;
		[SerializeField] private ARView _arView;

		private void OnEnable()
		{
			UIEvents.ARActive += OnARActive;
			UIEvents.SelectFurniture += OnItemSelected;
			UIEvents.PlacementConfirm += OnPlacementConfirm;
		}

		private void OnDisable()
		{
			UIEvents.ARActive -= OnARActive;
			UIEvents.SelectFurniture -= OnItemSelected;
			UIEvents.PlacementConfirm -= OnPlacementConfirm;
		}

		private void OnARActive(bool active)
		{
			_arSession.enabled = active;
			_arView.gameObject.SetActive(active);

			if (!active) _arSession.Reset();
		}

		private void OnItemSelected(string id)
		{
			FurnitureControllerEvents.GetFurnitureItem(id, (FurnitureSO furnitureItem) =>
			{
				_arView.SetTapPlacePrefabs(furnitureItem.ModelPreview, furnitureItem.ModelPrefab);
			});
		}

		private void OnPlacementConfirm()
		{
			_arView.ConfirmFurniturePlacement();
		}
	}
}
