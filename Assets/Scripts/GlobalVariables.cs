using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalVariables {

	public static float bpm = 120;

	public static int counter = 0;

	public static int round = 0;

	public static bool action = false;

	public static List<Color> color_list = new List<Color> {
		new Color(0.5f, 0.1875f, 0.0625f),
		Color.red,
		Color.yellow,
		Color.green,
		Color.blue,
		Color.grey
	};

	public static List<int> midi_list = new List<int> {
		0,
		69,
		71,
		73,
		76,
		78
	};

	public static bool warmingUp() {
		return round < 4;
	}

}
