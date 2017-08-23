using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TransportOverview.Facade {
	public interface IFacadeFactory {
		ICameraFacade CameraFacade { get; }
		ITransportLineFacade TransportLineFacade { get; }
		ITransportVehicleFacade TransportVehicleFacade { get; }
		ITransportStopFacade TransportStopFacade { get; }
	}
}
