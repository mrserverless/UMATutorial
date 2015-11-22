using UnityEngine;
using System.Collections;
using UMA;
using Zenject;

namespace UMA.DI
{
	public class UMADiData : UMAData {
		
		[PostInject]
		void init (UMAGeneratorBase generator, [InjectOptional] UMARecipe recipe) {
			this.umaGenerator = generator;
			this.umaRecipe = recipe;
		}
		
	}	
}
