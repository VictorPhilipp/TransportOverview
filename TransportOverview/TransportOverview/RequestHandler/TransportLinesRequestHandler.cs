using CityWebServer.Extensibility;
using System;
using System.Collections.Generic;
using System.Net;
using ColossalFramework;
using ImprovedPublicTransport2.Detour;
using TransportOverview.Data;
using UnityEngine;
using System.Linq;
using TransportOverview.Extension;

namespace TransportOverview.RequestHandler {
	public class TransportLinesRequestHandler : RequestHandlerBase {
		public static TransportLinesRequestHandler Instance { get; private set; }

		public const string LINE_ID = "lineId";

		public TransportLinesRequestHandler(IWebServer server)
			: base(server, Guid.NewGuid(), "Transport Lines API", "Victor-Philipp Negoescu (@LinuxFan)", 100, "/PTO/TransportLines") {
			Instance = this;
		}

		public override IResponseFormatter Handle(HttpListenerRequest request) {
			if (request.QueryString.HasKey(LINE_ID)) {
				return JsonResponse<TransportLineData>(Constants.FacadeFactory.TransportLineFacade.GetTransportLine(ushort.Parse(request.QueryString.Get(LINE_ID))));
			} else {
				return JsonResponse<TransportLineData[]>(Constants.FacadeFactory.TransportLineFacade.GetTransportLines().ToArray());
			}
		}
	}
}
