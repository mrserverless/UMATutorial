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
	
	private int numberOfSlots = 20; // 
}
