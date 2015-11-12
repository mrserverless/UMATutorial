using UnityEngine;
using System;
using System.Collections;
using UMA;
using Zenject;

namespace UMA.Zenject {
		
	public class UMAInstaller : MonoInstaller {
		
		public GameObject umaGenerator;
		public GameObject umaContext;
		
		public override void InstallBindings() {
			Container.Bind<UMAGeneratorBase>().ToSinglePrefab<UMAGeneratorBase>(umaGenerator);
			Container.Bind<UMAContext>().ToSinglePrefab<UMAContext>(umaContext);
		}
		
		void InstallUMAData() {
			
		}
		
	}
}