using UnityEngine;
using System.Collections;
using UMA;
using UnityStandardAssets.Characters.ThirdPerson;
using Zenject;

namespace UMA.Inject
{
	public class UMAInjectableAvatar : UMAAvatarBase {
		
		public override void Start()
		{
			// do nothing until dependencies are injected
		}
		
		[PostInject]
		public void Initialize(
			UMAContext context, UMAGeneratorBase generator, UMAInjectableData data, 
			[InjectOptional]UMARecipeBase recipe, 
			[InjectOptional]UMARecipeBase [] additionalRecipe,
			ThirdPersonCharacter controller)
		{
			base.context = context;
			base.umaGenerator = generator;
			base.umaData = data;
			base.umaRecipe = recipe;
			base.umaAdditionalRecipes = additionalRecipe;
			
			//controller.gameObject.transform.SetParent(this.gameObject.transform);
			this.gameObject.transform.SetParent(controller.gameObject.transform);
			this.gameObject.transform.localPosition = Vector3.zero;
			this.gameObject.transform.localRotation = Quaternion.identity;
			
			Animator animator = this.gameObject.GetComponent<Animator>();
			controller.m_Animator = animator;
			
			base.Initialize();
			
			if (umaAdditionalRecipes == null || umaAdditionalRecipes.Length == 0)
			{
				Load(umaRecipe);
			}
			else
			{
				Load(umaRecipe, umaAdditionalRecipes);
			}
		}
		
		public class Factory : GameObjectFactory<UMAInjectableAvatar>
		{
		}
	}

	
}
