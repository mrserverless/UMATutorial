using UnityEngine;
using System.Collections;
using UMA;
using Zenject;

public class InjectableUMAData : UMAData {
	
	[PostInject]
	void init (UMAGenerator generator, [InjectOptional] UMARecipe recipe) {
		this.umaGenerator = generator;
		this.umaRecipe = recipe;
	}
	
}
