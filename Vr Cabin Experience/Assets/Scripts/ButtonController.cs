using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRStandardAssets.Utils;
namespace ZenvaVR{

	[RequireComponent(typeof(VRInteractiveItem))]
	public class ButtonController : MonoBehaviour {

		//selected material 
		public Material selectedMaterial;

		// direction 
		public Vector3 direction;

		// rotation 
		public float rotation;
		 
		// delegate 
		public delegate void PressedDel(Vector3 dir,float rotation);

		// event defination
		public event PressedDel OnPress;
		// vr itneractive item component 
		VRInteractiveItem VRItem;

		// default material 
		Material defaultMaterial;

		// renderer component 
		Renderer render;

		void Awake(){
			// grab the component 
			VRItem = GetComponent<VRInteractiveItem> ();
			render = GetComponent<Renderer> ();


			// grab default material 
			defaultMaterial = render.sharedMaterial;
		}

		void OnEnable(){
			VRItem.OnClick += Press;
			VRItem.OnOver += Hover;
			VRItem.OnOut += Unhover;
		}

		void OnDisable(){
			VRItem.OnClick -= Press;
			VRItem.OnOver -= Hover;
			VRItem.OnOut -= Unhover;
		}
		void Press(){
			// trigger event 
			OnPress(direction, rotation);

		}
		void Hover(){
			// set highlight material 
			render.sharedMaterial = selectedMaterial;
		}

		void Unhover(){
			// set the button back to its default material 
			render.sharedMaterial = defaultMaterial;
		}



	}

}


