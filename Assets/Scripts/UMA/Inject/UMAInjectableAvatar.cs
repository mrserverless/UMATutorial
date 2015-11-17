using UnityEngine;
using System.Collections;
using UMA;
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
			[InjectOptional]UMARecipeBase recipe, [InjectOptional]UMARecipeBase [] additionalRecipe)
		{
			base.context = context;
			base.umaGenerator = generator;
			base.umaData = data;
			base.umaRecipe = recipe;
			base.umaAdditionalRecipes = additionalRecipe;
		
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
