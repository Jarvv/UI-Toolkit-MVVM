using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using UI.Controls;
using UnityEngine;
using UnityEngine.UIElements;
using Views.UI.States;
using Views.UI.ViewModels;

namespace Views.UI
{
	public class ARUIView : UIView
	{
		private ARViewState _arViewState;

		private Button _confirmButton;
		private Label _tapPlaceMessage;
		private VisualElement _tapImage;
		private VisualElement _scanImage;
		private SnappyScrollView _furnitureSelectScrollView;
		private Button _furnitureSelectButton;
		private Label _furnitureName;

		private FurnitureSO _furniture;

		private List<FurnitureSelectUIView> _furnitureSelectItems = new List<FurnitureSelectUIView>();

		public ARUIView(VisualElement parentElement) : base(parentElement)
		{
			SetVisualElements();
			RegisterCallbacks();
		}

		private void SetVisualElements()
		{
			_arViewState = _rootElement.Q<ARViewState>();
			_confirmButton = _rootElement.Q<VisualElement>("ConfirmButton").Q<Button>();
			_tapPlaceMessage = _rootElement.Q<Label>("tap_place_message");
			_tapImage = _rootElement.Q<VisualElement>("tap_image");
			_scanImage = _rootElement.Q<VisualElement>("scan_image");
			_furnitureSelectScrollView = _rootElement.Q<SnappyScrollView>();
			_furnitureSelectButton = _rootElement.Q<Button>("furniture_select_button");
			_furnitureName = _rootElement.Q<Label>("furniture_name");
		}

		private void RegisterCallbacks()
		{
			_eventRegistry.RegisterCallback<ClickEvent>(_confirmButton, ev =>
			{
				UpdateARViewState(ARViewStates.SELECT);
				UIEvents.PlacementConfirm();
			});

			_eventRegistry.RegisterCallback<ClickEvent>(_furnitureSelectButton, ev =>
			{
				UpdateARViewState(ARViewStates.TAP_TO_PLACE);
				UIEvents.SelectFurniture(_furniture.id);
			});

			_furnitureSelectScrollView.ItemActive += OnItemActive;
		}

		public override void Disable()
		{
			base.Disable();

			_furnitureSelectScrollView.ItemActive -= OnItemActive;
			_furnitureSelectItems.ForEach(item => item.ScrollToItem -= _furnitureSelectScrollView.ScrollToItem);
		}


		private void OnItemActive(VisualElement element)
		{
			_furniture = ((FurnitureItemViewModel)element.dataSource).Furniture;
			_furnitureName.text = _furniture.FurnitureName;
		}

		private void SetTapPlaceText(string text)
		{
			_tapPlaceMessage.text = text;
		}

		public void SetTapPlaceImage(TrackAndPlaceEvents e)
		{
			switch (e)
			{
				case TrackAndPlaceEvents.TOO_CLOSE:
					_tapImage.style.display = DisplayStyle.None;
					SetTapPlaceText("Too close - please tilt phone up");
					break;
				case TrackAndPlaceEvents.TOO_FAR:
					_tapImage.style.display = DisplayStyle.None;
					SetTapPlaceText("Too far - please tilt phone down");
					break;
				case TrackAndPlaceEvents.JUST_RIGHT:
					_tapImage.style.display = DisplayStyle.Flex;
					SetTapPlaceText("Tap to place furniture");
					break;
				case TrackAndPlaceEvents.PREFAB_PLACED:
					_scanImage.style.display = DisplayStyle.Flex;
					_tapImage.style.display = DisplayStyle.None;
					SetTapPlaceText("Scan your surroundings to begin");

					UpdateARViewState(ARViewStates.POSITION);
					break;
				case TrackAndPlaceEvents.PREVIEW_PLACED:
					_scanImage.style.display = DisplayStyle.None;
					_tapImage.style.display = DisplayStyle.Flex;
					SetTapPlaceText("Tap to place furniture");
					break;
			}
		}

		public void PopuplateFurnitureScrollView(FurnitureSO[] furniture)
		{
			foreach (FurnitureSO furnitureSO in furniture)
			{
				TemplateContainer furnitureItem = _furnitureSelectScrollView.ItemTemplate.Instantiate();
				FurnitureSelectUIView furnitureSelectItemView = new FurnitureSelectUIView(furnitureItem, furnitureSO);
				_furnitureSelectScrollView.AddItem(furnitureItem);
				_furnitureSelectItems.Add(furnitureSelectItemView);

				furnitureSelectItemView.ScrollToItem += _furnitureSelectScrollView.ScrollToItem;
			}
		}

		public void UpdateARViewState(ARViewStates state)
		{
			_arViewState.SetState(state);
		}
	}
}
