using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using Unity.Properties;
using UnityEngine;

namespace Views.UI.ViewModels
{
	public class FurnitureItemViewModel : ViewModel
	{
		private FurnitureSO _furniture;
	
		[CreateProperty]
		public FurnitureSO Furniture
		{
			get => _furniture;
			set => SetProperty(ref _furniture, value);
		}
	}
}

