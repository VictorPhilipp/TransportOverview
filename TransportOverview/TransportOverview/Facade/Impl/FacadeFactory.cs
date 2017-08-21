﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TransportOverview.Facade.Impl {
	public class FacadeFactory : IFacadeFactory {
		public static IFacadeFactory Instance = new FacadeFactory();

		public ITransportLineFacade TransportLineFacade {
			get {
				return Impl.TransportLineFacade.Instance;
			}
		}

		public ITransportStopFacade TransportStopFacade {
			get {
				return Impl.TransportStopFacade.Instance;
			}
		}

		public ITransportVehicleFacade TransportVehicleFacade {
			get {
				return Impl.TransportVehicleFacade.Instance;
			}
		}
	}
}
