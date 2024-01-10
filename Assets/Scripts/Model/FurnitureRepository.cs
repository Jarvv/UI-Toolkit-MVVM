using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;

namespace Model 
{
	public class FurnitureRepository
	{
		private FurnitureSO[] _furnitureItems;
		
		public FurnitureRepository()
		{
			_furnitureItems = LoadFurnitureData();
		}
		  
		private FurnitureSO[] LoadFurnitureData()
		{
			return Resources.LoadAll<FurnitureSO>("Furniture");
		}
		
		public FurnitureSO[] GetAllFurnitureItems()
		{
			return _furnitureItems;
		}
	}
}
