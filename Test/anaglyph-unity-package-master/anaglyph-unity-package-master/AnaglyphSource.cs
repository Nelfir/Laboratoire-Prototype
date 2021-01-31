using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

#if UNITY_EDITOR
using UnityEditor; // to access "AssetDatabase"
#endif

public class AnaglyphSource : MonoBehaviour {

	// public locals
	private bool spawnedAudioMixer = false;
	// public bool overrideParametersWithConfig = false;

	// private locals
	private AudioMixer audioMixer;
	private Transform listenerHead;
	private string audioMixerName;

	// Use this for initialization
	void Start () {

		// point listener head transform to  main camera's
		listenerHead = Camera.main.transform; 

		if( spawnedAudioMixer ){ audioMixer = SpawnAudioMixer(); }
		else{ audioMixer = GetCurrentAudioMixer(); }
		
		// setup default based on config values
		bypassReverb = AnaglyphConfig.bypassReverb;
		reverbOrder = AnaglyphConfig.reverbOrder;
		roomId = AnaglyphConfig.roomId;
	}

	private AudioMixer GetCurrentAudioMixer() {

		// get all audio sources attached to object
		AudioSource[] audioSources = GetComponents<AudioSource>();

		// loop over audio sources and set output to Anaglyph audio mixer
		foreach (var audioSource in audioSources)
		{
			if( audioSource.outputAudioMixerGroup != null )
			{ 
				return audioSource.outputAudioMixerGroup.audioMixer;
			}
		}

		Debug.LogError("Anaglyph source spawnedAudioMixer set to " + spawnedAudioMixer + " : need to assign an audio mixer to all your sources (same for all sources)");
		return null;
	}

	private AudioMixer SpawnAudioMixer() {

		#if UNITY_EDITOR

		// define audio mixer name based on object ID
		audioMixerName = "AnaglyphAudioMixer"+ gameObject.GetInstanceID ();

		// create audio mixer (unity editor only)
		AssetDatabase.CopyAsset (AnaglyphConfig.GetAudioMixerAssetPath(), AnaglyphConfig.GetAudioMixerAssetPath(audioMixerName));

		// load audio mixer
		AudioMixer audMix = Resources.Load<AudioMixer> (audioMixerName);
		Debug.Log (gameObject.name + " audio mixer: " + audMix.name);

		// get all audio sources attached to object
		AudioSource[] audioSources = GetComponents<AudioSource>();

		// loop over audio sources and set output to Anaglyph audio mixer
		foreach (var audioSource in audioSources)
		{
			Debug.Log ("object " + gameObject.name + ", audio source: " + audioSource.name);
			audioSource.outputAudioMixerGroup = audMix.FindMatchingGroups("Master")[0];
		}

		return audMix;
		
		#else
		
		return null;
		
		#endif
	}

	// Update is called once per frame
	void Update () {

		// discard if audio mixer not yet created
		if( audioMixer == null ){ return; }
		
		// get position relative to camera
		// go wonder, Camera.current triggers segfault when Unity launched from Xcode debugger
		// Transform cameraTransform = GameObject.Find("FirstPersonCharacter").transform;
		Vector3 relPos = listenerHead.InverseTransformDirection(transform.position - listenerHead.position);

		// convert coords to spherical 
		relPos = Utils.CartToSph (relPos);

		// map coords to vst input format (in [0:1])
		relPos = Utils.MapSphToVst (relPos);

		// update audio mixer parameters
		audioMixer.SetFloat ("Azimuth", relPos[0]);
		audioMixer.SetFloat ("Elevation", relPos[1]);
		audioMixer.SetFloat ("Distance", relPos[2]);
	}

	void OnDestroy(){
		
		#if UNITY_EDITOR

		// discard if script disabled (as OnDestroy method may be called even then..) or if spawn audio mixer option disabled
		if( !this.enabled || !spawnedAudioMixer ){ return; }
		
		// delete audio mixer created for object instance
		AssetDatabase.DeleteAsset ( AnaglyphConfig.GetAudioMixerAssetPath(audioMixerName) );
		
		#endif
	}

	// Anaglyph Audio parameters definition (don't go overboard here, this is not the way to go and you know it)
	
	private bool _bypassReverb;
	public bool bypassReverb
	{
		get { return _bypassReverb; }
		set 
		{
			_bypassReverb = value;
			if( audioMixer == null ){ return; }
			audioMixer.SetFloat ("BypassReverb", _bypassReverb ? 1f:0f);
		}
	}

	private int _roomId;
	public int roomId
	{
		get { return _roomId; }
		set 
		{
			_roomId = value;
			if( audioMixer == null ){ return; }
			audioMixer.SetFloat ("RoomId", ( (float)value / 2.0F ) );
		}
	}

	private int _reverbOrder;
	public int reverbOrder
	{
		get { return _reverbOrder; }
		set 
		{
			_reverbOrder = value;
			if( audioMixer == null ){ return; }
			audioMixer.SetFloat ("ReverbOrder", (float)value / 3.0F );
		}
	}
}

public static class Utils {
	
	public static float Map (float x, float x1, float x2, float y1,  float y2)
	{
		var m = (y2 - y1) / (x2 - x1);
		var c = y1 - m * x1; // point of interest: c is also equal to y2 - m * x2, though float math might lead to slightly different results.

		return m * x + c;
	}

	// same convention as Sph2Cart
	public static Vector3 CartToSph(Vector3 xyz) {
		Vector3 aed = new Vector3();
		// radius
		aed[2] = Mathf.Sqrt (xyz[0]*xyz[0] + xyz[1]*xyz[1] + xyz[2]*xyz[2]);
		// azimuth
		if (xyz[2] == 0) { xyz[2] = Mathf.Epsilon; }
		aed[0] = Mathf.Atan(xyz[0] / xyz[2]);
		if (xyz[2] < 0) { aed[0] += Mathf.PI; }
		if (aed [0] > Mathf.PI) { aed [0] -= 2 * Mathf.PI; } // remap 180:270 to -180:-90
		// elevation
		if (aed[2] == 0) { aed[2] = Mathf.Epsilon; }
		aed[1] = Mathf.Asin(xyz[1] / aed[2]);
		// rad to deg
		aed[0] = Mathf.Rad2Deg * aed[0];
		aed[1] = Mathf.Rad2Deg * aed[1];
		return aed;
	}

	public static Vector3 MapSphToVst(Vector3 aed){
		Vector3 aedNorm = new Vector3();
		// azimuth
		aedNorm[0] = Utils.Map (aed [0], -180, 180, 0, 1);
		// elevation
		aedNorm[1] = Utils.Map (aed [1], -90, 90, 0, 1);
		// distance
		aedNorm[2] = Mathf.Max( Mathf.Min (aed [2], 10.0F), 0.1F); // constrain distance in [0.1:10]
		aedNorm[2] = Utils.Map (aedNorm [2], 0.1F, 10F, 0F, 1F);

		return aedNorm;
	}

}