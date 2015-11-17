using UnityEngine;
using System;
using System.Collections;
using UMA;
using UMA.Inject;
using Zenject;

namespace UMA.Zenject 
{		
	public class UMAInstaller : MonoInstaller 
	{
		public GameObject umaGenerator;
		public GameObject umaContext;
		public GameObject umaAvatar;
		public GameObject umaData;
		public GameObject umaAnimator;
		
		public override void InstallBindings() 
		{
			InstallPrefabs();
			InstallAvartar();
			InstallUMADna();
		}
		
		void InstallPrefabs() 
		{
			Container.Bind<UMAGeneratorBase>().ToSinglePrefab<UMAGeneratorBase>(umaGenerator);
			Container.Bind<UMAContext>().ToSinglePrefab<UMAContext>(umaContext);
			Container.Bind<UMAInjectableData>().ToTransientPrefab<UMAInjectableData>(umaData);
		}
		
		void InstallAvartar() 
		{
			Container.BindGameObjectFactory<UMAInjectableAvatar.Factory>(umaAvatar);
		}
		
		void InstallUMADna() 
		{

			Container.Bind<UMADnaHumanoid>().ToSingle();
			Container.Bind<UMADnaTutorial>().ToSingle();
		}

	}
	
}