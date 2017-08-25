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
	public class TransportVehiclePrefabsRequestHandler : RequestHandlerBase {
		public static TransportVehiclePrefabsRequestHandler Instance { get; private set; }

		public const string LINE_ID = "lineId";
		public const string INDEX = "index";
		public const string SERVICE = "service";
		public const string SUB_SERVICE = "subService";
		public const string LEVEL = "level";
		public const string ACTION = "action";
		public const string ADD = "add";
		public const string REMOVE = "remove";

		public TransportVehiclePrefabsRequestHandler(IWebServer server)
			: base(server, Guid.NewGuid(), "Transport Vehicle Prefabs API", "Victor-Philipp Negoescu (@LinuxFan)", 100, "/PTO/TransportVehiclePrefabs") {
			Instance = this;
		}

		public override IResponseFormatter Handle(HttpListenerRequest request) {
			if (request.QueryString.HasKey(ACTION)) {
				ushort lineId = ushort.Parse(request.QueryString.Get(LINE_ID));
				int prefabIndex = int.Parse(request.QueryString.Get(INDEX));

				switch (request.QueryString.Get(ACTION)) {
					case ADD:
						Constants.FacadeFactory.TransportVehiclePrefabFacade.AddPrefabToTransportLine(lineId, prefabIndex);
						return JsonResponse<bool>(true);
					case REMOVE:
						Constants.FacadeFactory.TransportVehiclePrefabFacade.RemovePrefabFromTransportLine(lineId, prefabIndex);
						return JsonResponse<bool>(true);
				}
			}

			ItemClass.Service service = (ItemClass.Service)Enum.Parse(typeof(ItemClass.Service), request.QueryString.Get(SERVICE), true);
			ItemClass.SubService subService = (ItemClass.SubService)Enum.Parse(typeof(ItemClass.SubService), request.QueryString.Get(SUB_SERVICE), true);
			ItemClass.Level level = (ItemClass.Level)Enum.Parse(typeof(ItemClass.Level), request.QueryString.Get(LEVEL), true);

			return JsonResponse<string[]>(Constants.FacadeFactory.TransportVehiclePrefabFacade.GetTransportVehiclePrefabs(service, subService, level).ToArray());
		}
	}
}
