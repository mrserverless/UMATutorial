using UnityEngine;
using System.Collections;
using Zenject;
using UnityStandardAssets.Characters.ThirdPerson;


namespace UMA.DI
{
	public class CharacterInstaller : MonoInstaller 
	{
		public GameObject umaAvatar;
		public GameObject playerController;
		
		public override void InstallBindings() 
		{
			Container.Bind<ITickable>().ToSingle<UMAMaker>();
			Container.Bind<ThirdPersonCharacter>().ToSinglePrefab<ThirdPersonCharacter>(playerController);
			Container.BindGameObjectFactory<UMADiAvatar.Factory>(umaAvatar);
		}
	}	
}
