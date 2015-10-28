using UnityEngine;
using System.Collections;
using UMA;

public class UMAMaker : MonoBehaviour {
	
	public UMAGeneratorBase generator;
	public SlotLibrary slotLibrary;
	public OverlayLibrary overlayLibrary;
	public RaceLibrary raceLibrary;
	
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
		
		// Generate UMA
		umaDynamicAvatar.UpdateNewRace();
		
	}
	
	void CreateMale() {
		// grab reference to receipe
		var umaRecipe = umaDynamicAvatar.umaData.umaRecipe;
		
		umaRecipe.SetRace(raceLibrary.GetRace("HumanMale"));
			
		SlotData maleFace = slotLibrary.InstantiateSlot("MaleFace");
		maleFace.AddOverlay(overlayLibrary.InstantiateOverlay("MaleHead02"));
		umaData.umaRecipe.slotDataList[0] = maleFace;
		
		
	}
}
