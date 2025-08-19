using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class UIKeys : MonoBehaviour {


	public ReflectionProbe[] probes;

	private ThemeManager ScifiThemeManager;
	private bool isSceneLightsOn = false;
	private bool isAlarmOn = false;


	void OnEnable() {

		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	void OnDisable() {

		SceneManager.sceneLoaded -= OnSceneLoaded;
	}

	// Use this for initialization
	void Start () {
		
		string sceneName =  SceneManager.GetActiveScene().name;
		isSceneLightsOn = (sceneName == "scene_lightsOn")?true:false;
			
	}

	void OnSceneLoaded(Scene scene, LoadSceneMode mode) {

		ScifiThemeManager = GetComponentInChildren<ThemeManager>();
		ScifiThemeManager.setTheme(0);
		ScifiThemeManager.setWarningLights(false);

		if(scene.name == "scene_lightsOff") {
			ScifiThemeManager.setDirtAmount(3);
			ScifiThemeManager.setTorch(true);
			isAlarmOn = false;
		} 

	}

	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown(KeyCode.T)) {
			if(isSceneLightsOn)
				SceneManager.LoadScene("scene_lightsOff");
			else
				SceneManager.LoadScene("scene_lightsOn");
		}

		if (Input.GetKeyDown(KeyCode.Alpha1) && isSceneLightsOn) {
			ScifiThemeManager.setTheme(0);
		}

		if (Input.GetKeyDown(KeyCode.Alpha2) && isSceneLightsOn) {
			ScifiThemeManager.setTheme(1);
		}

		if (Input.GetKeyDown(KeyCode.Alpha3) && isSceneLightsOn) {
			ScifiThemeManager.setTheme(2);
		}

		if (Input.GetKeyDown(KeyCode.Alpha4) && isSceneLightsOn) {
			ScifiThemeManager.setTheme(3);
		}

		if (Input.GetKeyDown(KeyCode.Alpha5) && isSceneLightsOn) {
			ScifiThemeManager.setDirtAmount(1);
		}
		if (Input.GetKeyDown(KeyCode.Alpha6) && isSceneLightsOn) {
			ScifiThemeManager.setDirtAmount(2);
		}
		if (Input.GetKeyDown(KeyCode.Alpha7) && isSceneLightsOn) {
			ScifiThemeManager.setDirtAmount(3);
		}

		if (Input.GetKeyDown(KeyCode.Alpha8) && isSceneLightsOn) {
			ScifiThemeManager.setPostProcessingProfile(0);
		}
		if (Input.GetKeyDown(KeyCode.Alpha9) && isSceneLightsOn) {
			ScifiThemeManager.setPostProcessingProfile(1);

		}
		if (Input.GetKeyDown(KeyCode.Alpha0) && isSceneLightsOn) {
			ScifiThemeManager.setPostProcessingProfile(2);

		}

		if (Input.GetKeyDown(KeyCode.Z) && !isSceneLightsOn) {

			if(isAlarmOn) {
				ScifiThemeManager.setWarningLights(false);
				ScifiThemeManager.setTorch(true);
				isAlarmOn = false;
			}
			else {
				ScifiThemeManager.setWarningLights(true);
				ScifiThemeManager.setTorch(false);
				isAlarmOn = true;
			}

		}

		if (Input.GetKeyDown(KeyCode.Escape)) {
			Screen.fullScreen = false;
			Cursor.visible = true;
		}


	}
}
