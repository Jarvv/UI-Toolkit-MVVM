using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Properties;
using UnityEngine;
using UnityEngine.UIElements;

namespace ScriptableObjects
{
	[CreateAssetMenu(menuName = "User")]
	public class UserSO : ScriptableObject
	{
		public string Name;
		public string Description;
		public Texture2D Image;
	}
}
