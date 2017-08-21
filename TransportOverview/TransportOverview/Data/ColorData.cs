using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace TransportOverview.Data {
	public class ColorData {
		public int r;
		public int g;
		public int b;

		public ColorData(ref Color32 c) {
			this.r = c.r;
			this.g = c.g;
			this.b = c.b;
		}

		public ColorData(ref Color c) {
			this.r = (int)(c.r * 255f);
			this.g = (int)(c.g * 255f);
			this.b = (int)(c.b * 255f);
		}
	}
}
