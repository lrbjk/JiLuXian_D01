using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;



public interface MatPropertySet {}


public class MatPropertySetBase : MatPropertySet{

	public Color BaseColor;
	public Color BaseColorOverlay;
	public Color BaseDirtColor;
	public Color DetailColor;

	public float BaseNormalStrength;
	public float BaseSmoothness;
	public float BaseDirtStrength;
	public float BaseMetallic;
	public float DetailEdgeWear;
	public float DetailEdgeSmoothness;
	public float DetailDirtStrength;
	public float DetailOcclusionStrength;
    

}


public class MatPropertySetStripes : MatPropertySet {
	public Color TintColor;
	public float WearAmount;
	public float DirtAmount;
	public float ScratchesAmount;

}


public class SciFiTheme {

	public static int NUM_BASEMAT = 3;
	public MatPropertySetBase[] MatBaseProperties = new MatPropertySetBase[NUM_BASEMAT];

	public MatPropertySetStripes MatStripeProperties = new MatPropertySetStripes();

}

/*
 * 
 * ThemeManager class
 * 
 */
public class ThemeManager : MonoBehaviour {

	public Material[] BaseMaterials = new Material[SciFiTheme.NUM_BASEMAT];
	public Material StripeMaterial;
	public Material[] ApplyDirtMaterials;
	public GameObject WarningLights;
	public Material WarningLigthsMat;
	public Light Torch;
    public PostProcessVolume[] PPV;


    private SciFiTheme[] scifiThemesArray = new SciFiTheme[4];
	private ReflectionProbe [] probes;
	private Light [] wLights;
	private Animation [] wAnimation;
	private Camera mainCam;


	void OnEnable () {
		
		mainCam = Camera.main;
		Initialze();

		probes = FindObjectsOfType<ReflectionProbe>();
		wAnimation = WarningLights.GetComponentsInChildren<Animation>(); 
		wLights = WarningLights.GetComponentsInChildren<Light>(); 

	}

	public void setTheme(int themeId) {

		float dirtStrength =0f;

		for(int i=0;i<SciFiTheme.NUM_BASEMAT;i++) {
			
			MatPropertySetBase baseSet = scifiThemesArray[themeId].MatBaseProperties[i];

			if(i==0)
				dirtStrength = baseSet.BaseDirtStrength;

			BaseMaterials[i].SetColor("_BaseColor",baseSet.BaseColor);
			BaseMaterials[i].SetColor("_BaseColorOverlay", baseSet.BaseColorOverlay);
			BaseMaterials[i].SetColor("_BaseDirtColor", baseSet.BaseDirtColor);
			BaseMaterials[i].SetFloat("_BaseDirtStrength", baseSet.BaseDirtStrength);
			BaseMaterials[i].SetFloat("_BaseMetallic", baseSet.BaseMetallic);
			BaseMaterials[i].SetFloat("_BaseNormalStrength", baseSet.BaseNormalStrength);
			BaseMaterials[i].SetFloat("_BaseSmoothness", baseSet.BaseSmoothness);
			BaseMaterials[i].SetColor("_DetailColor", baseSet.DetailColor);
			BaseMaterials[i].SetFloat("_DetailDirtStrength", baseSet.DetailDirtStrength);
			BaseMaterials[i].SetFloat("_DetailEdgeSmoothness", baseSet.DetailEdgeSmoothness);
			BaseMaterials[i].SetFloat("_DetailEdgeWear", baseSet.DetailEdgeWear);
			BaseMaterials[i].SetFloat("_DetailOcclusionStrength", baseSet.DetailOcclusionStrength);

		}

		MatPropertySetStripes stripes = scifiThemesArray[themeId].MatStripeProperties;

		StripeMaterial.SetColor("_TintColor",stripes.TintColor);
		StripeMaterial.SetFloat("_WearAmount", stripes.WearAmount);
		StripeMaterial.SetFloat("_DirtAmount", stripes.DirtAmount);
		StripeMaterial.SetFloat("_ScratchesAmount", stripes.ScratchesAmount);

		setDirtAmount(dirtStrength);

		for(int i=0;i<probes.Length;i++) {
			probes[i].RenderProbe();
		}

		DynamicGI.UpdateEnvironment();

	}

	public void setDirtAmount(float amount) {

		amount += 0.2f;

		for(int i=0;i<ApplyDirtMaterials.Length;i++) {
			ApplyDirtMaterials[i].SetFloat("_BaseDirtStrength", amount);
		}

	}

	public void setDirtAmount(int strength) {

		float amount=0;
		float scratches=0;
		float wear=0;

		switch (strength) {
			case 1:
				amount = 0.05f;
				scratches = 0.1f;
				wear = 0.08f;
				break;
			case 2:
				amount = 0.25f;
				scratches = 0.5f;
				wear = 0.25f;
				break;
			case 3:
				amount = 0.6f;
				scratches = 0.75f;
				wear = 0.5f;
				break;
			

		}

		for(int i=0;i<SciFiTheme.NUM_BASEMAT;i++) {
			BaseMaterials[i].SetFloat("_BaseDirtStrength", amount);
		}

		for(int i=0;i<ApplyDirtMaterials.Length;i++) {
			ApplyDirtMaterials[i].SetFloat("_BaseDirtStrength", amount);
		}

		StripeMaterial.SetFloat("_WearAmount", wear);
		StripeMaterial.SetFloat("_DirtAmount", amount);
		StripeMaterial.SetFloat("_ScratchesAmount", scratches);

	}



	public void setWarningLights(bool enabled) {
		
		for(int i=0;i<wLights.Length;i++){
			wLights[i].enabled = enabled;
		}

		for(int i=0;i<wAnimation.Length;i++) {
			if(enabled)
				wAnimation[i].Play();
			else
				wAnimation[i].Stop();
		}

		AudioSource warningSound = GetComponent<AudioSource>();

		if(enabled) {
			WarningLigthsMat.SetColor("_EmissionColor",new Color(1,0.22f,0f) * 70f);
			warningSound.Play();
		}
		else {
			WarningLigthsMat.SetColor("_EmissionColor",new Color(0,0.0f,0f));
			warningSound.Stop();
		}
	}

	public void setTorch(bool enabled) {
		Torch.enabled = enabled;
	}


	public void setPostProcessingProfile(int ppId) {
        for (int i = 0; i < PPV.Length; i++) {
            PPV[i].enabled = false;
        }
        PPV[ppId].enabled = true;
		
	}


	/*
	 * 
	 * Initializing
	 * 
	 */


	void Initialze () {
		InitializeTheme_1();
		InitializeTheme_2();
		InitializeTheme_3();
		InitializeTheme_4();
	}

	void InitializeTheme_1 () {

		SciFiTheme scifiTheme = new SciFiTheme();

		// Color 1

		MatPropertySetBase mpsb_1 =  new MatPropertySetBase();

		mpsb_1.BaseColor = ColorConverter.HexToColor("CED3DB");
		mpsb_1.BaseColorOverlay = ColorConverter.HexToColor("DBDBDB");
		mpsb_1.BaseDirtColor = ColorConverter.HexToColor("654F1F");
		mpsb_1.BaseDirtStrength = 0.05f;
		mpsb_1.BaseMetallic = 0f;
		mpsb_1.BaseNormalStrength = 0.4f;
		mpsb_1.BaseSmoothness = 0.95f;
		mpsb_1.DetailColor = ColorConverter.HexToColor("ABAFB7");
		mpsb_1.DetailDirtStrength = 0.18f;
		mpsb_1.DetailEdgeSmoothness = 0.68f;
		mpsb_1.DetailEdgeWear = 0.3f;
		mpsb_1.DetailOcclusionStrength = 0.5f;

		scifiTheme.MatBaseProperties[0] = mpsb_1;

		// Color 2

		MatPropertySetBase mpsb_2 =  new MatPropertySetBase();

		mpsb_2.BaseColor = ColorConverter.HexToColor("A4AAB7");
		mpsb_2.BaseColorOverlay = ColorConverter.HexToColor("ABAFB7");
		mpsb_2.BaseDirtColor = ColorConverter.HexToColor("654F1F");
		mpsb_2.BaseDirtStrength = 0.05f;
		mpsb_2.BaseMetallic = 0f;
		mpsb_2.BaseNormalStrength = 0.4f;
		mpsb_2.BaseSmoothness = 0.9f;
		mpsb_2.DetailColor = ColorConverter.HexToColor("818795");
		mpsb_2.DetailDirtStrength = 0.18f;
		mpsb_2.DetailEdgeSmoothness = 0.68f;
		mpsb_2.DetailEdgeWear = 0.3f;
		mpsb_2.DetailOcclusionStrength = 0.5f;

		scifiTheme.MatBaseProperties[1] = mpsb_2;

		// Color 3

		MatPropertySetBase mpsb_3 =  new MatPropertySetBase();

		mpsb_3.BaseColor = ColorConverter.HexToColor("767980");
		mpsb_3.BaseColorOverlay = ColorConverter.HexToColor("676A70");
		mpsb_3.BaseDirtColor = ColorConverter.HexToColor("654F1F");
		mpsb_3.BaseDirtStrength = 0.05f;
		mpsb_3.BaseMetallic = 0f;
		mpsb_3.BaseNormalStrength = 0.4f;
		mpsb_3.BaseSmoothness = 0.85f;
		mpsb_3.DetailColor = ColorConverter.HexToColor("AEA635");
		mpsb_3.DetailDirtStrength = 0.18f;
		mpsb_3.DetailEdgeSmoothness = 0.68f;
		mpsb_3.DetailEdgeWear = 0.3f;
		mpsb_3.DetailOcclusionStrength = 0.5f;

		scifiTheme.MatBaseProperties[2] = mpsb_3;

		// Stripes

		MatPropertySetStripes mpss = new MatPropertySetStripes();

		mpss.TintColor = ColorConverter.HexToColor("767980");
		mpss.WearAmount = 0.08f;
		mpss.DirtAmount = 0.118f;
		mpss.ScratchesAmount = 0.1f;

		scifiTheme.MatStripeProperties = mpss;

		scifiThemesArray[0] = scifiTheme;

	}


	void InitializeTheme_2 () {

		SciFiTheme scifiTheme = new SciFiTheme();

		// Color 1

		MatPropertySetBase mpsb_1 =  new MatPropertySetBase();

		mpsb_1.BaseColor = ColorConverter.HexToColor("F2F1ED");
		mpsb_1.BaseColorOverlay = ColorConverter.HexToColor("EDF0F2");
		mpsb_1.BaseDirtColor = ColorConverter.HexToColor("342A13");
		mpsb_1.BaseDirtStrength = 0.01f;
		mpsb_1.BaseMetallic = 1f;
		mpsb_1.BaseNormalStrength = 0.2f;
		mpsb_1.BaseSmoothness = 0.85f;
		mpsb_1.DetailColor = ColorConverter.HexToColor("525252");
		mpsb_1.DetailDirtStrength = 0.3f;
		mpsb_1.DetailEdgeSmoothness = 0.36f;
		mpsb_1.DetailEdgeWear = 0.1f;
		mpsb_1.DetailOcclusionStrength = 0.5f;

		scifiTheme.MatBaseProperties[0] = mpsb_1;

		// Color 2

		MatPropertySetBase mpsb_2 =  new MatPropertySetBase();

		mpsb_2.BaseColor = ColorConverter.HexToColor("808080");
		mpsb_2.BaseColorOverlay = ColorConverter.HexToColor("838380");
		mpsb_2.BaseDirtColor = ColorConverter.HexToColor("342A13");
		mpsb_2.BaseDirtStrength = 0.05f;
		mpsb_2.BaseMetallic = 1f;
		mpsb_2.BaseNormalStrength = 0.2f;
		mpsb_2.BaseSmoothness = 0.55f;
		mpsb_2.DetailColor = ColorConverter.HexToColor("FEFEFE");
		mpsb_2.DetailDirtStrength = 0.18f;
		mpsb_2.DetailEdgeSmoothness = 0.5f;
		mpsb_2.DetailEdgeWear = 0.1f;
		mpsb_2.DetailOcclusionStrength = 0.5f;

		scifiTheme.MatBaseProperties[1] = mpsb_2;

		// Color 3

		MatPropertySetBase mpsb_3 =  new MatPropertySetBase();

		mpsb_3.BaseColor = ColorConverter.HexToColor("F2F1ED");
		mpsb_3.BaseColorOverlay = ColorConverter.HexToColor("EDF0F2");
		mpsb_3.BaseDirtColor = ColorConverter.HexToColor("342A13");
		mpsb_3.BaseDirtStrength = 0.01f;
		mpsb_3.BaseMetallic = 1f;
		mpsb_3.BaseNormalStrength = 0.4f;
		mpsb_3.BaseSmoothness = 0.95f;
		mpsb_3.DetailColor = ColorConverter.HexToColor("525252");
		mpsb_3.DetailDirtStrength = 0.18f;
		mpsb_3.DetailEdgeSmoothness = 0.45f;
		mpsb_3.DetailEdgeWear = 0.1f;
		mpsb_3.DetailOcclusionStrength = 0.5f;

		scifiTheme.MatBaseProperties[2] = mpsb_3;

		// Stripes

		MatPropertySetStripes mpss = new MatPropertySetStripes();

		mpss.TintColor = ColorConverter.HexToColor("CCB012");
		mpss.WearAmount = 0.08f;
		mpss.DirtAmount = 0.118f;
		mpss.ScratchesAmount = 0.1f;

		scifiTheme.MatStripeProperties = mpss;

		scifiThemesArray[1] = scifiTheme;

	}


	void InitializeTheme_3 () {

		SciFiTheme scifiTheme = new SciFiTheme();

		// Color 1

		MatPropertySetBase mpsb_1 =  new MatPropertySetBase();

		mpsb_1.BaseColor = ColorConverter.HexToColor("EFECCA");
		mpsb_1.BaseColorOverlay = ColorConverter.HexToColor("E3DFB8");
		mpsb_1.BaseDirtColor = ColorConverter.HexToColor("654F1F");
		mpsb_1.BaseDirtStrength = 0.05f;
		mpsb_1.BaseMetallic = 0f;
		mpsb_1.BaseNormalStrength = 0.4f;
		mpsb_1.BaseSmoothness = 0.95f;
		mpsb_1.DetailColor = ColorConverter.HexToColor("ABAFB7");
		mpsb_1.DetailDirtStrength = 0.18f;
		mpsb_1.DetailEdgeSmoothness = 0.68f;
		mpsb_1.DetailEdgeWear = 0.3f;
		mpsb_1.DetailOcclusionStrength = 0.5f;

		scifiTheme.MatBaseProperties[0] = mpsb_1;

		// Color 2

		MatPropertySetBase mpsb_2 =  new MatPropertySetBase();

		mpsb_2.BaseColor = ColorConverter.HexToColor("A7A37E");
		mpsb_2.BaseColorOverlay = ColorConverter.HexToColor("B2AE8A");
		mpsb_2.BaseDirtColor = ColorConverter.HexToColor("654F1F");
		mpsb_2.BaseDirtStrength = 0.05f;
		mpsb_2.BaseMetallic = 0f;
		mpsb_2.BaseNormalStrength = 0.4f;
		mpsb_2.BaseSmoothness = 0.9f;
		mpsb_2.DetailColor = ColorConverter.HexToColor("818795");
		mpsb_2.DetailDirtStrength = 0.18f;
		mpsb_2.DetailEdgeSmoothness = 0.68f;
		mpsb_2.DetailEdgeWear = 0.3f;
		mpsb_2.DetailOcclusionStrength = 0.5f;

		scifiTheme.MatBaseProperties[1] = mpsb_2;

		// Color 3

		MatPropertySetBase mpsb_3 =  new MatPropertySetBase();

		mpsb_3.BaseColor = ColorConverter.HexToColor("60828D");
		mpsb_3.BaseColorOverlay = ColorConverter.HexToColor("5D8090");
		mpsb_3.BaseDirtColor = ColorConverter.HexToColor("654F1F");
		mpsb_3.BaseDirtStrength = 0.05f;
		mpsb_3.BaseMetallic = 0f;
		mpsb_3.BaseNormalStrength = 0.4f;
		mpsb_3.BaseSmoothness = 0.85f;
		mpsb_3.DetailColor = ColorConverter.HexToColor("AEA635");
		mpsb_3.DetailDirtStrength = 0.18f;
		mpsb_3.DetailEdgeSmoothness = 0.68f;
		mpsb_3.DetailEdgeWear = 0.3f;
		mpsb_3.DetailOcclusionStrength = 0.5f;

		scifiTheme.MatBaseProperties[2] = mpsb_3;

		// Stripes

		MatPropertySetStripes mpss = new MatPropertySetStripes();

		mpss.TintColor = ColorConverter.HexToColor("60828D");
		mpss.WearAmount = 0.08f;
		mpss.DirtAmount = 0.118f;
		mpss.ScratchesAmount = 0.1f;

		scifiTheme.MatStripeProperties = mpss;

		scifiThemesArray[2] = scifiTheme;

	}

	void InitializeTheme_4 () {

		SciFiTheme scifiTheme = new SciFiTheme();

		// Color 1

		MatPropertySetBase mpsb_1 =  new MatPropertySetBase();

		mpsb_1.BaseColor = ColorConverter.HexToColor("E7D995");
		mpsb_1.BaseColorOverlay = ColorConverter.HexToColor("E8D997");
		mpsb_1.BaseDirtColor = ColorConverter.HexToColor("654F1F");
		mpsb_1.BaseDirtStrength = 0.05f;
		mpsb_1.BaseMetallic = 0f;
		mpsb_1.BaseNormalStrength = 0.4f;
		mpsb_1.BaseSmoothness = 0.95f;
		mpsb_1.DetailColor = ColorConverter.HexToColor("FF6B34");
		mpsb_1.DetailDirtStrength = 0.18f;
		mpsb_1.DetailEdgeSmoothness = 0.68f;
		mpsb_1.DetailEdgeWear = 0.3f;
		mpsb_1.DetailOcclusionStrength = 0.5f;

		scifiTheme.MatBaseProperties[0] = mpsb_1;

		// Color 2

		MatPropertySetBase mpsb_2 =  new MatPropertySetBase();

		mpsb_2.BaseColor = ColorConverter.HexToColor("238A9E");
		mpsb_2.BaseColorOverlay = ColorConverter.HexToColor("1C6D7C");
		mpsb_2.BaseDirtColor = ColorConverter.HexToColor("654F1F");
		mpsb_2.BaseDirtStrength = 0.05f;
		mpsb_2.BaseMetallic = 0f;
		mpsb_2.BaseNormalStrength = 0.4f;
		mpsb_2.BaseSmoothness = 0.9f;
		mpsb_2.DetailColor = ColorConverter.HexToColor("FFCC45");
		mpsb_2.DetailDirtStrength = 0.18f;
		mpsb_2.DetailEdgeSmoothness = 0.68f;
		mpsb_2.DetailEdgeWear = 0.3f;
		mpsb_2.DetailOcclusionStrength = 0.5f;

		scifiTheme.MatBaseProperties[1] = mpsb_2;

		// Color 3

		MatPropertySetBase mpsb_3 =  new MatPropertySetBase();

		mpsb_3.BaseColor = ColorConverter.HexToColor("D2F8FF");
		mpsb_3.BaseColorOverlay = ColorConverter.HexToColor("D1F7FC");
		mpsb_3.BaseDirtColor = ColorConverter.HexToColor("654F1F");
		mpsb_3.BaseDirtStrength = 0.05f;
		mpsb_3.BaseMetallic = 0f;
		mpsb_3.BaseNormalStrength = 0.4f;
		mpsb_3.BaseSmoothness = 0.85f;
		mpsb_3.DetailColor = ColorConverter.HexToColor("AEA635");
		mpsb_3.DetailDirtStrength = 0.18f;
		mpsb_3.DetailEdgeSmoothness = 0.68f;
		mpsb_3.DetailEdgeWear = 0.3f;
		mpsb_3.DetailOcclusionStrength = 0.5f;

		scifiTheme.MatBaseProperties[2] = mpsb_3;

		// Stripes

		MatPropertySetStripes mpss = new MatPropertySetStripes();

		mpss.TintColor = ColorConverter.HexToColor("D2F8FF");
		mpss.WearAmount = 0.08f;
		mpss.DirtAmount = 0.118f;
		mpss.ScratchesAmount = 0.1f;

		scifiTheme.MatStripeProperties = mpss;

		scifiThemesArray[3] = scifiTheme;

	}

	

}
