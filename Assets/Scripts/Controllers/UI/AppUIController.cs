using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Controllers.UI;
using Model;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.UIElements;
using Views;
using Views.UI;
using Views.UI.States;

namespace Controllers.UI
{
	public class AppUIController : UIController
	{
		private AppUIView _appUIView;
		private CatalogueUIView _catalogueUIView;
		private SplashUIView _splashUIView;
		private FurnitureUIView _furnitureUIView;
		private NavBarUIView _navBarUIView;
		private ARUIView _arUIView;

		private UIElementRepository _uIElementRepository;


		protected override void Awake()
		{
			base.Awake();

			_uIElementRepository = new UIElementRepository();

			_appUIView = new AppUIView(UI.rootVisualElement.Q<VisualElement>("app"));
			_catalogueUIView = new CatalogueUIView(UI.rootVisualElement.Q<VisualElement>("Catalogue"));
			_splashUIView = new SplashUIView(UI.rootVisualElement.Q<VisualElement>("Splash"));
			_furnitureUIView = new FurnitureUIView(UI.rootVisualElement.Q<VisualElement>("Furniture"));
			_navBarUIView = new NavBarUIView(UI.rootVisualElement.Q<VisualElement>("NavBar"), _uIElementRepository.GetAllNavItems());
			_arUIView = new ARUIView(UI.rootVisualElement.Q<VisualElement>("AR"));
		}

		private void OnEnable()
		{
			UIEvents.Enter += OnAppEnter;
			UIEvents.CatalogueItemOpened += OnCatalogueItemOpened;
			UIEvents.FurnitureBackButtonTapped += OnFurnitureBackButtonTapped;
			UIEvents.NavButtonTapped += OnNavButtonTapped;

			AREvents.TrackAndPlace += OnTrackAndPlaceEvent;
			AREvents.EditFurniturePosition += OnEditFurniturePosition;
		}

		private void OnDisable()
		{
			UIEvents.Enter -= OnAppEnter;
			UIEvents.CatalogueItemOpened -= OnCatalogueItemOpened;
			UIEvents.FurnitureBackButtonTapped -= OnFurnitureBackButtonTapped;
			UIEvents.NavButtonTapped -= OnNavButtonTapped;

			AREvents.TrackAndPlace -= OnTrackAndPlaceEvent;
			AREvents.EditFurniturePosition -= OnEditFurniturePosition;
		}

		private void Start()
		{
			FurnitureControllerEvents.LoadFurnitureData(PopulateFurnitureInUI);
		}

		private void OnAppEnter()
		{
			UpdateAppViewState(AppViewStates.HOME);
		}

		private void OnCatalogueItemOpened(string id)
		{
			FurnitureControllerEvents.GetFurnitureItem(id, (FurnitureSO furnitureItem) =>
			{
				_furnitureUIView.SetFurniture(furnitureItem);
				UpdateAppViewState(AppViewStates.FURNITURE);
			});
		}

		private void OnFurnitureBackButtonTapped()
		{
			UpdateAppViewState(AppViewStates.CATALOGUE);
		}

		private void OnNavButtonTapped(AppViewStates state)
		{
			UpdateAppViewState(state);

			if (state == AppViewStates.AR) UIEvents.ARActive(true);
			else UIEvents.ARActive(false);
		}

		private void UpdateAppViewState(AppViewStates state)
		{
			_appUIView.UpdateAppViewState(state);
		}

		private void PopulateFurnitureInUI(FurnitureSO[] furnitureSOs)
		{
			_catalogueUIView.PopuplateFurnitureList(furnitureSOs);
			_arUIView.PopuplateFurnitureScrollView(furnitureSOs);
		}

		private void OnTrackAndPlaceEvent(TrackAndPlaceEvents e)
		{
			_arUIView.SetTapPlaceImage(e);
		}

		private void OnEditFurniturePosition()
		{
			_arUIView.UpdateARViewState(ARViewStates.POSITION);
		}
	}
}
