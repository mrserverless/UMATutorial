using UnityEngine;
using System.Collections.Generic;
using UMA;

public class UMAMaker : MonoBehaviour {
	
	public UMAGeneratorBase generator;
	public SlotLibrary slotLibrary;
	public OverlayLibrary overlayLibrary;
	public RaceLibrary raceLibrary;
	public RuntimeAnimatorController animController;
	
	private UMADynamicAvatar umaDynamicAvatar; // needed to display uma character
	private UMAData umaData; // used by dynamic avatar
	private UMADnaHumanoid umaDna; 
	private UMADnaTutorial umaTutorialDna; // optional make your own dna
	
	
	private int numberOfSlots = 20; // slots to be added to UMA
	
	void Start() {
		GenerateUMA();	
	}
	
	void GenerateUMA () {
		
		GameObject go = new GameObject("MyUMA");
		umaDynamicAvatar = go.AddComponent<UMADynamicAvatar>();
		
		// initialize avatar and grab reference to data
		umaDynamicAvatar.Initialize();
		umaData = umaDynamicAvatar.umaData;
		
		// assign generator
		umaDynamicAvatar.umaGenerator = generator;
		umaData.umaGenerator = generator;
		
		// setup slot array
		umaData.umaRecipe.slotDataList = new SlotData[numberOfSlots];
		
		umaDna = new UMADnaHumanoid();
		umaTutorialDna = new UMADnaTutorial();
		umaData.umaRecipe.AddDna(umaDna);
		umaData.umaRecipe.AddDna(umaTutorialDna);
		
		CreateMale();
		GenerateDNA();
		
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
		
		SlotData head = slotLibrary.InstantiateSlot("MaleFace", new List<OverlayData> {
			overlayLibrary.InstantiateOverlay("MaleHead02"),
			overlayLibrary.InstantiateOverlay("MaleEyebrow01", Color.black)
		});
		recipe.slotDataList[2] = head;
		
		SlotData torso = slotLibrary.InstantiateSlot("MaleTorso", new List<OverlayData> {
			overlayLibrary.InstantiateOverlay("MaleBody02"),
			overlayLibrary.InstantiateOverlay("MaleUnderwear01")
		});
		recipe.slotDataList[3] = torso;
		
		SlotData hands = slotLibrary.InstantiateSlot("MaleHands");
		hands.AddOverlay(overlayLibrary.InstantiateOverlay("MaleBody02"));
		recipe.slotDataList[4] = hands;
		
		SlotData legs = slotLibrary.InstantiateSlot("MaleLegs", new List<OverlayData>(){
			overlayLibrary.InstantiateOverlay("MaleBody02"),
			overlayLibrary.InstantiateOverlay("MaleUnderwear01")
		});
		recipe.slotDataList[5] = legs;
		
		SlotData feet = slotLibrary.InstantiateSlot("MaleFeet");
		feet.AddOverlay(overlayLibrary.InstantiateOverlay("MaleBody02"));
		recipe.slotDataList[6] = feet;
		
		
	}
	
	private void GenerateDNA() {
		umaDna.headSize = 0.5f;
	}
	
	private SlotData[] MaleSlots() {
		
		return new SlotData[0];
	}
}
