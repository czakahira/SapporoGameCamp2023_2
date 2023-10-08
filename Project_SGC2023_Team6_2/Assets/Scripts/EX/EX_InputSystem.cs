using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.InputSystem;

namespace UnityEx
{
	public static class EX_InputSystem
	{
		static public string GetActionMapsDataToString(this InputActionAsset _asset)
		{
			if (_asset.actionMaps.Count > 0) {
				StringBuilder builder = new StringBuilder();
				foreach (var map in _asset.actionMaps) {
					builder.Append($"{map.name} : {map.id}\n");
				}
				return builder.ToString();
			}
			return "";
		}

		static public string GetActionsDataToString(this InputActionMap _map)
		{
			if(_map.actions.Count > 0){
				StringBuilder builder = new StringBuilder();
				foreach (var action in _map.actions) {
					builder.Append($"{action.name} : {action.GetBindingIndex()} \n");
				}
				return builder.ToString();
			}
			return "";
		}

		static public string GetControlsDatasToString(this InputAction _action)
		{	
			if (_action.controls.Count > 0) {
				StringBuilder builder = new StringBuilder();
				foreach(var ctrl in _action.controls){
					builder.Append($"{ctrl.name} : {ctrl.path} \n");
				}
				return builder.ToString();
			}
			return "";
		}

		static public string GetBindingDatsToString(this InputAction _action)
		{
			if (_action?.bindings.Count > 0) {
				StringBuilder builder = new StringBuilder();
				foreach (var bind in _action.bindings) {
					builder.Append($"{bind.name} : {bind.path} \n");
				}
				return builder.ToString();
			}
			return "";
		}
	}
}