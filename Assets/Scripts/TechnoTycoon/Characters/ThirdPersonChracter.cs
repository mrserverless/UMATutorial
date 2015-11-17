using UnityEngine;
using System.Collections;
using UMA;
using Zenject;

namespace TechnoTycoon.Characters.ThirdPerson
{
	public class ThirdPersonCharacter : UnityStandardAssets.Characters.ThirdPerson.ThirdPersonCharacter, IInitializable {
		
		public void Initialize()
		{
			base.m_Animator = transform.Find("MyUMA").GetComponent<Animator>();
			Debug.Log("animator found");
			base.m_Animator.applyRootMotion = false;
		}
	}	
}
