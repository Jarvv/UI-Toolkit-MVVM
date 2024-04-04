using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using Unity.Properties;
using UnityEngine;

namespace Views.UI.ViewModels
{
	public class ARUIViewModel : ViewModel
	{
		[SerializeField, DontCreateProperty]
		private FurnitureSO _furniture;

		[SerializeField, DontCreateProperty]
		private string _tapPlaceMessage;

		[CreateProperty]
		public FurnitureSO Furniture
		{
			get => _furniture;
			set => SetProperty(ref _furniture, value, nameof(Furniture));
		}

		[CreateProperty]
		public string TapPlaceMessage
		{
			get => _tapPlaceMessage;
			set => SetProperty(ref _tapPlaceMessage, value, nameof(TapPlaceMessage));
		}
	}
}
