using UnityEngine;
using System.Collections;
using UMA;
using Zenject;

namespace TechnoTycoon.UMA {
		
	public class UMAInstaller : MonoInstaller {
		
		//public GameObject UMA;
		 //GameObject generator;
		//public SlotLibrary slotLibrary;
		//public OverlayLibrary overlayLibrary;
		//public RaceLibrary raceLibrary;
		//public RuntimeAnimatorController animController;
		
		public override void InstallBindings() {
			Container.Bind<UMAGenerator>().ToSinglePrefabResource("UMA/UMAGenerator");
			Container.Bind<SlotLibrary>().ToSinglePrefabResource("UMA/SlotLibrary");
			Container.Bind<OverlayLibrary>().ToSinglePrefabResource("UMA/OverlayLibrary");
			Container.Bind<RaceLibrary>().ToSinglePrefabResource("UMA/RaceLibrary");
			Container.Bind<UMAContext>().ToSinglePrefabResource("UMA/InjectableUMAContext");
		}
		
	}
}