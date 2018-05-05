using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class furie : MonoBehaviour {

	private int qSamples = 1024; 
	private float threshold = 0.04f; 
	private float pitchValue;

	private List<int> musickey = new List<int> ();

	private float[] spectrum = new float[1024]; 
	private float fSample;

	private float gap;
	private int point;
	private int key;
	private int musicplay;


	private float[] keyhz = {
		27.500f, //A0
		29.135f, //A#0
		30.868f, //B0
		32.703f, //C1
		34.648f, //C#0
		36.708f, //D1
		38.891f, //D#1
		41.203f, //E1
		43.654f, //F1
		46.249f, //F#1
		48.999f, //G1
		51.913f, //G#1
		55.000f, //A1
		58.270f,
		61.735f,
		65.406f,
		69.296f,
		73.416f,
		77.782f,
		82.407f,
		87.307f,
		92.499f,
		97.999f,
		103.826f,
		110.000f, //A2
		116.541f,
		123.471f,
		130.813f,
		138.591f,
		146.832f,
		155.563f,
		164.814f,
		174.614f,
		184.997f,
		195.998f,
		207.652f,
		220.000f, //A3
		233.082f,
		246.942f,
		261.626f,
		277.183f,
		293.665f,
		311.127f,
		329.628f,
		349.228f,
		369.994f,
		391.995f,
		415.305f,
		440.000f, //A4
		466.164f,
		493.883f,
		523.251f,
		554.365f,
		587.330f,
		622.254f,
		659.255f,
		698.456f,
		739.989f,
		783.991f,
		830.609f,
		880.000f, //A5
		932.328f,
		987.767f,
		1046.502f,
		1108.731f,
		1174.659f,
		1244.508f,
		1318.510f,
		1396.913f,
		1479.978f,
		1567.982f,
		1661.219f,
		1760.000f, //A6
		1864.655f,
		1975.533f,
		2093.005f,
		2217.461f,
		2349.318f,
		2489.016f,
		2637.020f,
		2793.826f,
		2959.955f,
		3135.963f,
		3322.438f,
		3520.000f, //A7
		3729.310f,
		3951.066f,
		4186.009f
	};

	public int start_time;
	public AudioClip musics;
	public AudioSource sounds;
	public AudioSource museca;

	public LineRenderer lr;
	private int count;

	//private string[] dname; 

	// Use this for initialization
	void Start () {
		sounds.clip = Microphone.Start(null,true,1,44100);
		while (!(Microphone.GetPosition (null) > 0)) {};
		sounds.Play ();
		museca.clip = musics;
		//museca.Play ();
		sounds.time = start_time;
		fSample = AudioSettings.outputSampleRate;

		count = 0;
		lr.SetVertexCount(count);

	}
	
	// Update is called once per frame
	void Update () {
		sounds.GetSpectrumData(spectrum, 0, FFTWindow.Rectangular);
		float maxV = 0;
		int maxN = 0;

		for (int i = 0; i < qSamples; i++)
		{
			if (spectrum[i] > maxV && spectrum[i] > threshold)
			{
				maxV = spectrum[i];
				maxN = i;
			}
		}

		float freqN = maxN;
		if (maxN > 0 && maxN < qSamples - 1)
		{

			float dL = spectrum[maxN - 1] / spectrum[maxN];
			float dR = spectrum[maxN + 1] / spectrum[maxN];
			freqN += 0.5f * (dR * dR - dL * dL);

		}
		pitchValue = freqN * (fSample / 2) / qSamples;

		gap = 0;

		for (int i = 0; i <= 87; i++) {

			if (gap == 0 || gap >= Mathf.Abs (keyhz [i] - pitchValue)) {
				gap = Mathf.Abs (keyhz [i] - pitchValue);
				point = i;
			}
		}

		key = point % 12;

		if (key == 0)  Debug.Log ("A");
		if (key == 1)  Debug.Log ("A#");
		if (key == 2)  Debug.Log ("B");
		if (key == 3)  Debug.Log ("C");
		if (key == 4)  Debug.Log ("C#");
		if (key == 5)  Debug.Log ("D");
		if (key == 6)  Debug.Log ("D#");
		if (key == 7)  Debug.Log ("E");
		if (key == 8)  Debug.Log ("F");
		if (key == 9)  Debug.Log ("F#");
		if (key == 10) Debug.Log ("G");
		if (key == 11) Debug.Log ("G#");

		//Debug.Log (pitchValue);

		if(count > 400) count = 0;
		count++;
		lr.SetVertexCount(count);
		lr.SetPosition(count - 1, new Vector3(-200 + count,key*10, 0));
	}


	public void OnValueChanged(int result){
		if (result == 1) sounds.clip = musics;
		if (result == 0) sounds.clip = Microphone.Start(null,true,1,44100);
	}

	public void Onclick(){
	    easygi
	}
}
