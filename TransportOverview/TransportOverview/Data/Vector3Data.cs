using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace TransportOverview.Data {
	public class Vector3Data {
		public float x;
		public float y;
		public float z;

		public Vector3Data(ref Vector3 v) {
			this.x = v.x;
			this.y = v.y;
			this.z = v.z;
		}
	}
}
