using UnityEngine;
using System.Collections;
using UMA;
using Zenject;

public class InjectableUMAContext : UMAContext {

//	private RaceLibraryBase raceLibrary;
//	private SlotLibraryBase slotLibrary;
//	private OverlayLibraryBase overlayLibrary;

	new void Start () {
		Instance = this;
	}
	
	[PostInject]
	void Init(SlotLibrary slotLibrary, OverlayLibrary overlayLibrary, RaceLibrary raceLibrary) {
		this.slotLibrary = slotLibrary;
		this.overlayLibrary = overlayLibrary;
		this.raceLibrary = raceLibrary;
	}
	
	// Use Zenject to return singleton instance
	public new static UMAContext FindInstance() {
		return Instance;
	}
}
