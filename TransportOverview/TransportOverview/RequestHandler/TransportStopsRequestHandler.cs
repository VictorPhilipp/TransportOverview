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
	public class TransportStopsRequestHandler : RequestHandlerBase {
		public static TransportStopsRequestHandler Instance { get; private set; }

		public const string LINE_ID = "lineId";

		public TransportStopsRequestHandler(IWebServer server)
			: base(server, Guid.NewGuid(), "Transport Stops API", "Victor-Philipp Negoescu (@LinuxFan)", 100, "/PTO/TransportStops") {
			Instance = this;
		}

		public override IResponseFormatter Handle(HttpListenerRequest request) {
			return JsonResponse<TransportStopData[]>(Constants.FacadeFactory.TransportStopFacade.GetTransportLineStops(ushort.Parse(request.QueryString.Get(LINE_ID))).ToArray());
		}
	}
}
