using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using ScriptableObjects;
using Unity.Properties;
using UnityEngine;
using UnityEngine.UIElements;

namespace Views.UI.ViewModels
{
	public class HomeViewModel : ViewModel
	{
		private UserSO _user;
	
		[CreateProperty]
		public UserSO User
		{
			get => _user;
			set => SetProperty(ref _user, value);
		}
	}
}
