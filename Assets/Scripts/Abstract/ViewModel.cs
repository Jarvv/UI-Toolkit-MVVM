using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class ViewModel : INotifyBindablePropertyChanged
{
	public event EventHandler<BindablePropertyChangedEventArgs> propertyChanged;

	public bool SetProperty<T>(ref T field, T newValue, string propertyName)
	{
		if (!EqualityComparer<T>.Default.Equals(field, newValue))
		{
			field = newValue;
			Notify(propertyName);
			return true;
		}

		return false;
	}

	private void Notify([CallerMemberName] string property = "")
	{
		propertyChanged?.Invoke(this, new BindablePropertyChangedEventArgs(property));
	}
}
