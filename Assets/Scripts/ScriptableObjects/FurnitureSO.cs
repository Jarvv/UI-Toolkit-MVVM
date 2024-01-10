using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects
{
	[CreateAssetMenu(menuName = "FurnitureSO")]
	public class FurnitureSO : ScriptableObject
	{
		[HideInInspector]
		public string id = Guid.NewGuid().ToString();	
		public string FurnitureName;
		public string Description;
		public Sprite Thumbnail;
		public GameObject ModelPrefab;
	}
}
