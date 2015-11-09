using UnityEngine;
using System.Collections;
using UMA;
using Zenject;

namespace TechnoTycoon.UMA {
		
	public class UMAInstaller : MonoInstaller {
		
		public GameObject UMA;
		//public UMAGeneratorBase generator;
		//public SlotLibrary slotLibrary;
		//public OverlayLibrary overlayLibrary;
		//public RaceLibrary raceLibrary;
		//public RuntimeAnimatorController animController;
		
		public override void InstallBindings() {
			Container.Bind<OverlayLibrary>().ToSinglePrefab(UMA);
			Container.Bind<SlotLibrary>().ToSinglePrefab(UMA);
			Container.Bind<OverlayLibrary>().ToSinglePrefab(UMA);
			Container.Bind<RaceLibrary>().ToSinglePrefab(UMA);
			Container.Bind<RuntimeAnimatorController>().ToSinglePrefab(UMA);
		}
		
	}
}