using UnityEngine;
using System.Collections.Generic;
using UMA;
using UMA.Inject;
using Zenject;

public class UMAMaker : MonoBehaviour {
	
	private UMAGeneratorBase generator;
	private UMAContext context;
	public RuntimeAnimatorController animController;

	private SlotLibraryBase slotLibrary;
	private OverlayLibraryBase overlayLibrary;
	private RaceLibraryBase raceLibrary;

	private UMAAvatarBase umaDynamicAvatar; // needed to display uma character
	private UMAData umaData;
	private UMADnaHumanoid umaDna; 
	private UMADnaTutorial umaTutorialDna; // optional make your own dna
	private UMAInjectableAvatar.Factory avatarGOFactory;
	
	[Range (0.0f,1.0f)]
	public float bodyMass = 0.5f;
	
	private int numberOfSlots = 20; // slots to be added to UMA
	
	[PostInject]
	public void Init(UMAGeneratorBase generator, UMAContext context, 
		UMADnaHumanoid dna, UMADnaTutorial tutorial,
		UMAInjectableAvatar.Factory avatarGOFactory) {
		this.generator = generator;
		this.context = context;
		this.slotLibrary = context.slotLibrary;
		this.overlayLibrary = context.overlayLibrary;
		this.raceLibrary = context.raceLibrary;
		this.avatarGOFactory = avatarGOFactory;
			
		umaDna = dna;
		umaTutorialDna = tutorial;
	}
	
	void Start() {
		GenerateUMA();	
	}
	
	void Update() {
		
		if (bodyMass != umaDna.upperMuscle) {
			SetBodyMass(bodyMass);
			umaData.isShapeDirty = true;
			umaData.Dirty();
		}
			
	}
	
	void GenerateUMA () {
		
		GameObject go = new GameObject("MyUMA");
		umaDynamicAvatar = go.AddComponent<UMADynamicAvatar>();
		umaDynamicAvatar.context = context;
		umaDynamicAvatar.umaGenerator = generator;

		umaData = go.AddComponent<UMAInjectableData> ();
		umaDynamicAvatar.umaData = umaData;

		umaDynamicAvatar.Initialize();
		//umaDynamicAvatar = avatarGOFactory.Create();
		//GameObject go = umaDynamicAvatar.gameObject;
		
		UMAData.UMARecipe recipe = new UMAData.UMARecipe(); 
		recipe.slotDataList = new SlotData[numberOfSlots];
		recipe.AddDna(umaDna);
		recipe.AddDna(umaTutorialDna);
		umaData.umaRecipe = recipe;
		
		CreateMale();
		
		umaDynamicAvatar.animationController = animController;
		
		// Generate UMA
		umaDynamicAvatar.UpdateNewRace();
		
		go.transform.SetParent(this.gameObject.transform);
		go.transform.localPosition = Vector3.zero;
		go.transform.localRotation = Quaternion.identity;
	}
	
	void CreateMale() {
		// grab reference to receipe
		var recipe = umaDynamicAvatar.umaData.umaRecipe;

		recipe.SetRace(raceLibrary.GetRace("HumanMale"));
		
		SlotData eyes = slotLibrary.InstantiateSlot("MaleEyes");
		eyes.AddOverlay(overlayLibrary.InstantiateOverlay("EyeOverlay"));
		recipe.slotDataList[0] = eyes;
		
		SlotData mouth = slotLibrary.InstantiateSlot("MaleInnerMouth");
		mouth.AddOverlay(overlayLibrary.InstantiateOverlay("InnerMouth"));
		recipe.slotDataList[1] = mouth;
		

		recipe.slotDataList[2] = slotLibrary.InstantiateSlot("MaleFace", new List<OverlayData> {
			overlayLibrary.InstantiateOverlay("MaleHead02"),
			overlayLibrary.InstantiateOverlay("MaleEyebrow01", Color.black)
		});;
		
		SlotData torso = slotLibrary.InstantiateSlot("MaleTorso", new List<OverlayData> {
			overlayLibrary.InstantiateOverlay("MaleBody02"),
			overlayLibrary.InstantiateOverlay("MaleUnderwear01"),
			overlayLibrary.InstantiateOverlay("SA_Tee"),
			overlayLibrary.InstantiateOverlay("SA_Logo")
		});
		recipe.slotDataList[3] = torso;
		
		recipe.slotDataList[4] = slotLibrary.InstantiateSlot("MaleHands", torso.GetOverlayList());
		recipe.slotDataList[5] = slotLibrary.InstantiateSlot("MaleLegs", torso.GetOverlayList());		
		recipe.slotDataList[6] = slotLibrary.InstantiateSlot("MaleFeet", torso.GetOverlayList());
	}
	
	private void SetBodyMass(float mass) {
		umaDna.upperMuscle = mass;
		umaDna.upperWeight = mass;
		umaDna.lowerMuscle = mass;
		umaDna.lowerWeight = mass;
		umaDna.armWidth = mass;
		umaDna.forearmWidth = mass;
	}
	
	private SlotData[] MaleSlots() {
		
		return new SlotData[0];
	}
}
