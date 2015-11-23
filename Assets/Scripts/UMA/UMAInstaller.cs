using UnityEngine;
using System;
using System.Collections;
using UMA;
using UMA.DI;
using Zenject;

namespace UMA.Di
{		
	public class UMAInstaller : MonoInstaller 
	{
		public GameObject umaGenerator;
		public GameObject umaContext;
		public GameObject umaData;
		
		public override void InstallBindings() 
		{
			InstallPrefabs();
			InstallUMADna();
		}
		
		void InstallPrefabs() 
		{
			Container.Bind<UMAGeneratorBase>().ToSinglePrefab<UMAGeneratorBase>(umaGenerator);
			Container.Bind<UMAContext>().ToSinglePrefab<UMAContext>(umaContext);
			Container.Bind<UMAData>().ToTransientPrefab<UMAData>(umaData);
		}
		
		void InstallUMADna() 
		{
			Container.Bind<UMADnaHumanoid>().ToSingle();
			Container.Bind<UMADnaTutorial>().ToSingle();
		}

	}
	
}