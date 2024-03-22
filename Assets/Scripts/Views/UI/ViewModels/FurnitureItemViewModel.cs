using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using Unity.Properties;
using UnityEngine;

namespace Views.UI.ViewModels
{
	public class FurnitureItemViewModel : ViewModel
	{
		public FurnitureItemViewModel(FurnitureSO furniture) : base()
		{
			_furniture = furniture;
		}
		
		private FurnitureSO _furniture;
	
		[CreateProperty]
		public FurnitureSO Furniture
		{
			get => _furniture;
			set => SetProperty(ref _furniture, value);
		}
	}
}

