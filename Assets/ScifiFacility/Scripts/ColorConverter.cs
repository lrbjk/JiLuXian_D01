using UnityEngine;

public class ColorConverter  {
	
	private static int HexToInt (char hexChar) {
		string hex = "" + hexChar;
		switch (hex) {
		case "0": return 0;
		case "1": return 1;
		case "2": return 2;
		case "3": return 3;
		case "4": return 4;
		case "5": return 5;
		case "6": return 6;
		case "7": return 7;
		case "8": return 8;
		case "9": return 9;
		case "A": return 10;
		case "B": return 11;
		case "C": return 12;
		case "D": return 13;
		case "E": return 14;
		case "F": return 15;
		}
		return -1;
	}

	public static Color HexToColor (string color) {
		float red = (HexToInt(color[1]) + HexToInt(color[0]) * 16f) / 255;
		float green = (HexToInt(color[3]) + HexToInt(color[2]) * 16f) / 255;
		float blue = (HexToInt(color[5]) + HexToInt(color[4]) * 16f) / 255;
		Color finalColor = new Color();
		finalColor.r = red;
		finalColor.g = green;
		finalColor.b = blue;
		finalColor.a = 1;
		return finalColor;
	}

}
