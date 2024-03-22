using System;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Util;

public class UIViewControllerEditorWindow : EditorWindow
{
	[SerializeField]
	private VisualTreeAsset m_VisualTreeAsset = default;
	
	private EventRegistry _eventRegistry;
	
	// UI element references
	private Button _confirmButton;
	private TextField _scriptNameTextField;
	private TextField _viewTextField;
	private TextField _controllerTextField;
	private TextField _uxmlTextField;
	private TextField _ussTextField;
	private TextField _viewStateField;
	
	private Toggle _uxmlToggle;
	private Toggle _ussToggle;
	

	[MenuItem("MVC/Create New UI VC")]
	public static void ShowExample()
	{
		UIViewControllerEditorWindow wnd = GetWindow<UIViewControllerEditorWindow>();
		wnd.titleContent = new GUIContent("UIViewControllerEditorWindow");
	}
	
	private void OnDestroy()
	{
		_eventRegistry.Dispose();
	}

	public void CreateGUI()
	{
		VisualElement root = rootVisualElement;
		root.Add(m_VisualTreeAsset.Instantiate());
		
		_eventRegistry  = new EventRegistry(); 
		
		GetVisualElements();
		RegisterCallbacks();
	}
	
	private void GetVisualElements()
	{
		_confirmButton = rootVisualElement.Q<Button>("confirm_button");
		_scriptNameTextField = rootVisualElement.Q<TextField>("script_name_text_field");
		_viewTextField = rootVisualElement.Q<TextField>("view_text_field");
		_controllerTextField = rootVisualElement.Q<TextField>("controller_text_field");
		_uxmlTextField = rootVisualElement.Q<TextField>("uxml_text_field");
		_uxmlToggle = rootVisualElement.Q<Toggle>("uxml_toggle");
		_ussTextField = rootVisualElement.Q<TextField>("uss_text_field");
		_ussToggle = rootVisualElement.Q<Toggle>("uss_toggle");
	}
	
	private void RegisterCallbacks()
	{
		_eventRegistry.RegisterCallback<ClickEvent>(_confirmButton, evt => GenerateScripts());
		_eventRegistry.RegisterCallback<InputEvent>(_scriptNameTextField, evt => UpdateTextFieldValues());
		_eventRegistry.RegisterCallback<ChangeEvent<bool>>(_uxmlToggle, evt => ToggleField(_uxmlTextField));
		_eventRegistry.RegisterCallback<ChangeEvent<bool>>(_ussToggle, evt => ToggleField(_ussTextField));
	}
	
	private void UpdateTextFieldValues()
	{
		_viewTextField.value = _scriptNameTextField.value + "UIView";
		_controllerTextField.value = _scriptNameTextField.value + "UIController";
		_uxmlTextField.value = _scriptNameTextField.value;
		_ussTextField.value = _scriptNameTextField.value;
	}
	
	private void ToggleField(TextField field)
	{
		if(field.ClassListContains("read-only"))
		{
			field.isReadOnly = false;
			field.RemoveFromClassList("read-only");
		}
		else
		{
			field.isReadOnly = true;
			field.AddToClassList("read-only");
		}
	}
	
	private void GenerateScripts()
	{
		string baseName = _scriptNameTextField.value;
		
		if(baseName.Length == 0) return;
		
		CreateScript(
			baseName + "UIView",
			"Assets/Scripts/Views/UI/", 
			Path.Combine(Application.dataPath, "ScriptTemplates/81-Scripts__C# UIView Script-NewUIView.cs.txt"));
			
		CreateScript(
			baseName + "UIController",
			"Assets/Scripts/Controllers/UI/", 
			Path.Combine(Application.dataPath, "ScriptTemplates/81-Scripts__C# UIController Script-NewUIController.cs.txt"));

		CreateUSS(
			baseName,
			"Assets/UI/Pages/"+baseName+"/");
			
		CreateUXML(
			baseName,
			"Assets/UI/Pages/"+baseName+"/");
			
		AssetDatabase.Refresh();
		
		this.Close();
	}
	
	private void CreateScript(string scriptName, string creationPath, string templatePath)
	{
		string script = Path.Combine(creationPath, scriptName + ".cs");
		
		// Check if the script already exists
		if (AssetDatabase.LoadAssetAtPath(script, typeof(MonoScript)) == null)
		{
			// Load the custom script template from the ScriptTemplates folder
			string template = File.ReadAllText(templatePath);

			// Replace placeholders in the template with the actual script name
			template = template.Replace("#SCRIPTNAME#", scriptName);

			// Write the modified template to the specified path
			File.WriteAllText(script, template);

			// Refresh the AssetDatabase to make sure the new script is recognized
			AssetDatabase.Refresh();
			Debug.Log("Script created: " + script);
		}
		else
		{
			Debug.LogWarning("Script already exists: " + script);
		}
	}
	
	private void CreateUXML(string baseName, string creationPath)
	{
		 if (!Directory.Exists(creationPath))
		{
			CreateFolder(creationPath);
		}
		
		string uxml = Path.Combine(creationPath, baseName + ".uxml");
		string uss = Path.Combine(creationPath, baseName + ".uss");
		
		string uxmlContent = _ussToggle.value ? 
			$"<ui:UXML xmlns:ui=\"UnityEngine.UIElements\" xmlns:uie=\"UnityEditor.UIElements\" xsi=\"http://www.w3.org/2001/XMLSchema-instance\" engine=\"UnityEngine.UIElements\" editor=\"UnityEditor.UIElements\"  editor-extension-mode=\"False\"><Style src=\"project://database/"+ uss +"\" /></ui:UXML>" 
			: $"<ui:UXML xmlns:ui=\"UnityEngine.UIElements\" xmlns:uie=\"UnityEditor.UIElements\" xsi=\"http://www.w3.org/2001/XMLSchema-instance\" engine=\"UnityEngine.UIElements\" editor=\"UnityEditor.UIElements\"  editor-extension-mode=\"False\"></ui:UXML>";
		
		// Check if the script already exists
		if (AssetDatabase.LoadAssetAtPath(uxml, typeof(VisualTreeAsset)) == null)
		{
			File.WriteAllText(uxml, uxmlContent);
		}
		else
		{
			Debug.LogWarning("UXML already exists: " + uxml);
		}
	}
	
	private void CreateUSS(string baseName, string creationPath)
	{
		if (!Directory.Exists(creationPath))
		{
			CreateFolder(creationPath);
		}
		
		string uss = Path.Combine(creationPath, baseName + ".uss");
		
		string ussContent = $"";
	
		if (AssetDatabase.LoadAssetAtPath(uss, typeof(StyleSheet)) == null)
		{
			File.WriteAllText(uss, ussContent);
		}
		else
		{
			Debug.LogWarning("USS already exists: " + uss);
		}
	}
	
	private void CreateFolder(string folderPath)
	{
		try
		{
			Directory.CreateDirectory(folderPath);
			Debug.Log("Folder created: " + folderPath);
		}
		catch (Exception e)
		{
			Debug.LogError("Error creating folder: " + e.Message);
		}
	}
}
