using UnityEngine;
using System.Collections;
using UMA;
using Zenject;

namespace UMA.Zenject {
		
	public class UMAInstaller : MonoInstaller {
		
		public GameObject generator;
		public GameObject umaContext;
		
		public override void InstallBindings() {
			Container.Bind<UMAGeneratorBase>().ToSinglePrefab(generator);
			Container.Bind<UMAContext>().ToSinglePrefab(umaContext);
		}
		
		void InstallUMAContext() {
			//UMAContext context = new UMAContext();
		}
		
		void InstallUMAData() {
			
		}
	}
}