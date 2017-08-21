using CityWebServer.Extensibility;
using System;
using System.Collections.Generic;
using System.Net;
using ColossalFramework;
using ImprovedPublicTransport2.Detour;
using TransportOverview.Data;
using UnityEngine;
using System.Linq;

namespace TransportOverview.RequestHandler {
	public class TransportVehiclesRequestHandler : RequestHandlerBase {
		public static TransportVehiclesRequestHandler Instance { get; private set; }

		public const string LINE_ID = "lineId";

		public TransportVehiclesRequestHandler(IWebServer server)
			: base(server, Guid.NewGuid(), "Transport Vehicles API", "Victor-Philipp Negoescu (@LinuxFan)", 100, "/PTO/TransportVehicles") {
			Instance = this;
		}

		public override IResponseFormatter Handle(HttpListenerRequest request) {
			return JsonResponse<TransportVehicleData[]>(Constants.FacadeFactory.TransportVehicleFacade.GetTransportLineVehicles(ushort.Parse(request.QueryString.Get(LINE_ID))).ToArray());
		}
	}
}
