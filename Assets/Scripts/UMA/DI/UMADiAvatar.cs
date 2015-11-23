using UnityEngine;
using System.Collections;
using UMA;
using UnityStandardAssets.Characters.ThirdPerson;
using Zenject;

namespace UMA.DI
{
	public class UMADiAvatar : UMAAvatarBase {

		ThirdPersonCharacter controller;

		public override void Start()
		{
			// do nothing until dependencies are injected
		}
		
		[PostInject]
		public void Initialize(
			UMAContext context, UMAGeneratorBase generator, UMAData data, 
			[InjectOptional]UMARecipeBase recipe, 
			[InjectOptional]UMARecipeBase [] additionalRecipe,
			ThirdPersonCharacter controller)
		{
			base.context = context;
			base.umaGenerator = generator;
			base.umaData = data;
			base.umaRecipe = recipe;
			base.umaAdditionalRecipes = additionalRecipe;
			this.controller = controller;
			
			this.gameObject.transform.SetParent(controller.gameObject.transform);
			this.gameObject.transform.localPosition = Vector3.zero;
			this.gameObject.transform.localRotation = Quaternion.identity;

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

		public void Update()
		{
			
			if (controller.m_Animator == null) { // && transform.Find("MyUMA").GetComponent<Animator>()
					controller.m_Animator = base.umaData.animator;
					controller.m_Animator.applyRootMotion = false;
				}

		}


		public class Factory : GameObjectFactory<UMADiAvatar>
		{
		}
	}

	
}
