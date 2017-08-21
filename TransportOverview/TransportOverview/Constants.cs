using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TransportOverview.Facade;

namespace TransportOverview {
	public static class Constants {
		public static IFacadeFactory FacadeFactory {
			get {
				return Facade.Impl.FacadeFactory.Instance;
			}
		}
	}
}
