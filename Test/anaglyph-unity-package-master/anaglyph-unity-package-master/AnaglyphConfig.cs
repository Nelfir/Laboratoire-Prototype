using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AnaglyphConfig {

	// Anaglyph parameters
	public static bool bypassReverb = false;
	public static int roomId = 2; // in 0:2
	public static int reverbOrder = 0; // in 0:3

	// public static float hrtfId = 0.0f;

	private const string blueprintAudioMixerName = "AnaglyphAudioMixer";

	public static string GetAudioMixerAssetPath(string audioMixerName = blueprintAudioMixerName)
	{
		return "Assets/Anaglyph/Resources/" + audioMixerName + ".mixer";
	}

}
