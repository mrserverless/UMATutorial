using UnityEngine;
using System.Collections;
using UMA;
using Zenject;

namespace UMA.Inject
{
	public class UMAInjectableData : UMAData {
		
		[PostInject]
		void init (UMAGeneratorBase generator, [InjectOptional] UMARecipe recipe) {
			this.umaGenerator = generator;
			this.umaRecipe = recipe;
		}
		
	}	
}
