using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

		public FurnitureSO GetFurnitureItemById(string id)
		{
			return _furnitureItems.First(item => item.id == id);
		}
	}
}
