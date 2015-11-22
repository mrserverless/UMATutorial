using UnityEngine;
using System.Collections.Generic;
using UMA;
using UMA.DI;
using UnityStandardAssets.Characters.ThirdPerson;
using Zenject;

public class UMAMaker : ITickable {

	private SlotLibraryBase slotLibrary;
	private OverlayLibraryBase overlayLibrary;
	private RaceLibraryBase raceLibrary;

	private UMAAvatarBase umaDynamicAvatar; // needed to display uma character
	private UMAData umaData;
	private UMADnaHumanoid umaDna; 
	private UMADnaTutorial umaTutorialDna; // optional make your own dna
	private UMADiAvatar.Factory avatarGOFactory;
		
	[Range (0.0f,1.0f)]
	public float bodyMass = 0.5f;
	
	private int numberOfSlots = 20; // slots to be added to UMA
	
	public bool vestState = false;
	private bool lastVestState = false;
	public Color vestColor;
	private Color lastVestColor;
	
	[PostInject]
	public void Initialize(UMADiAvatar.Factory avatarGOFactory,
		UMADnaHumanoid dna, UMADnaTutorial tutorial) {
		this.avatarGOFactory = avatarGOFactory;
		this.umaDna = dna;
		this.umaTutorialDna = tutorial;
		GenerateUMA();		
	}
	
	public void Tick() {
		if (bodyMass != umaDna.upperMuscle) {
			SetBodyMass(bodyMass);
			umaData.isShapeDirty = true;
			umaData.Dirty();
		}
		if (vestState && !lastVestState)
		{
			lastVestState = true;
			AddOverlay(3, "SA_Tee", Color.white);
			AddOverlay(3, "SA_Logo", Color.white);
			umaData.isTextureDirty = true;
			umaData.Dirty();
		}
		if (!vestState && lastVestState)
		{
			lastVestState = false;
			RemoveOverlay(3, "SA_Tee");
			RemoveOverlay(3, "SA_Logo");
			umaData.isTextureDirty = true;
			umaData.Dirty();
		}
		if (vestColor != lastVestColor && vestState)
		{
			lastVestColor = vestColor;
			AddOverlay(3, "SA_Tee", vestColor);
			AddOverlay(3, "SA_Logo", vestColor);
			umaData.isTextureDirty = true;
			umaData.Dirty();
		}
	}
	
	void GenerateUMA () {
		
		umaDynamicAvatar = avatarGOFactory.Create();
		GameObject player = umaDynamicAvatar.gameObject;
		player.name = "MyUMA";
		
		this.slotLibrary = umaDynamicAvatar.context.slotLibrary;
		this.overlayLibrary = umaDynamicAvatar.context.overlayLibrary;
		this.raceLibrary = umaDynamicAvatar.context.raceLibrary;
		
		umaData = player.AddComponent<UMADiData> ();
		umaDynamicAvatar.umaData = umaData;
		UMAData.UMARecipe recipe = new UMAData.UMARecipe(); 
		
		//umaData = umaDynamicAvatar.umaData;
		
		recipe.slotDataList = new SlotData[numberOfSlots];
		recipe.AddDna(umaDna);
		recipe.AddDna(umaTutorialDna);
		
		umaData.umaRecipe = recipe;
		
		CreateMale();
		
		// Generate UMA
		umaDynamicAvatar.UpdateNewRace();
		
		//GameObject controllerGO = GameObject.FindGameObjectWithTag("Character Controller");
		//player.transform.SetParent(controllerGO.transform);
		//player.transform.localPosition = Vector3.zero;
		//player.transform.localRotation = Quaternion.identity;
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
			overlayLibrary.InstantiateOverlay("MaleUnderwear01")
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
	
	void AddOverlay (int slot, string overLayName, Color color)
	{
		umaDynamicAvatar.umaData.umaRecipe.slotDataList[slot]
			.AddOverlay(overlayLibrary.InstantiateOverlay(overLayName, color));
	}
	
	void RemoveOverlay (int slot, string overLayName)
	{
		umaDynamicAvatar.umaData.umaRecipe.slotDataList[slot]
			.RemoveOverlay(overLayName);
	}
	
}
