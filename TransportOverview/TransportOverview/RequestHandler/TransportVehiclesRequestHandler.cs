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
	public class TransportVehiclesRequestHandler : RequestHandlerBase {
		public static TransportVehiclesRequestHandler Instance { get; private set; }

		public const string LINE_ID = "lineId";
		public const string ACTION = "action";
		public const string ADD = "add";
		public const string REMOVE = "remove";
		public const string INDEX = "index";

		public TransportVehiclesRequestHandler(IWebServer server)
			: base(server, Guid.NewGuid(), "Transport Vehicles API", "Victor-Philipp Negoescu (@LinuxFan)", 100, "/PTO/TransportVehicles") {
			Instance = this;
		}

		public override IResponseFormatter Handle(HttpListenerRequest request) {
			ushort lineId = ushort.Parse(request.QueryString.Get(LINE_ID));

			if (request.QueryString.HasKey(ACTION)) {
				string indexStr = request.QueryString.Get(INDEX);
				int? index = null;
				if (indexStr != null) {
					index = int.Parse(indexStr);
				}

				switch (request.QueryString.Get(ACTION)) {
					case ADD:
						Constants.FacadeFactory.TransportVehicleFacade.AddTransportVehicle(lineId, index);
						return JsonResponse<bool>(true);
					case REMOVE:
						Constants.FacadeFactory.TransportVehicleFacade.RemoveTransportVehicle(lineId, index);
						return JsonResponse<bool>(true);
				}
			}

			return JsonResponse<TransportVehicleData[]>(Constants.FacadeFactory.TransportVehicleFacade.GetTransportLineVehicles(lineId).ToArray());
		}
	}
}
