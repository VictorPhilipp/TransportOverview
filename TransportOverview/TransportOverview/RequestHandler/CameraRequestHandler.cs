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
	public class CameraRequestHandler : RequestHandlerBase {
		public static CameraRequestHandler Instance { get; private set; }

		public enum InstanceType {
			None = 0,
			Building = 1,
			CitizenInstance = 2,
			Node = 3,
			ParkedVehicle = 4,
			Segment = 5,
			Vehicle = 6
		}

		public const string INSTANCE_TYPE = "instanceType";
		public const string INSTANCE_ID = "instanceId";
		public const string X = "x";
		public const string Z = "z";

		public CameraRequestHandler(IWebServer server)
			: base(server, Guid.NewGuid(), "Camera API", "Victor-Philipp Negoescu (@LinuxFan)", 100, "/PTO/Camera") {
			Instance = this;
		}

		public override IResponseFormatter Handle(HttpListenerRequest request) {
			if (request.QueryString.HasKey(INSTANCE_TYPE) && request.QueryString.HasKey(INSTANCE_ID)) {
				InstanceType instanceType = (InstanceType)Enum.Parse(typeof(InstanceType), request.QueryString.Get(INSTANCE_TYPE), true);

				switch (instanceType) {
					case InstanceType.None:
					default:
						throw new ArgumentException($"Invalid {INSTANCE_TYPE} given: {request.QueryString.Get(INSTANCE_TYPE)}");
					case InstanceType.Building:
						Constants.FacadeFactory.CameraFacade.GoToBuilding(ushort.Parse(request.QueryString.Get(INSTANCE_ID)), true);
						break;
					case InstanceType.CitizenInstance:
						Constants.FacadeFactory.CameraFacade.GoToCitizenInstance(ushort.Parse(request.QueryString.Get(INSTANCE_ID)), true);
						break;
					case InstanceType.Node:
						Constants.FacadeFactory.CameraFacade.GoToNode(ushort.Parse(request.QueryString.Get(INSTANCE_ID)), true);
						break;
					case InstanceType.ParkedVehicle:
						Constants.FacadeFactory.CameraFacade.GoToParkedVehicle(ushort.Parse(request.QueryString.Get(INSTANCE_ID)), true);
						break;
					case InstanceType.Segment:
						Constants.FacadeFactory.CameraFacade.GoToSegment(ushort.Parse(request.QueryString.Get(INSTANCE_ID)), true);
						break;
					case InstanceType.Vehicle:
						Constants.FacadeFactory.CameraFacade.GoToVehicle(ushort.Parse(request.QueryString.Get(INSTANCE_ID)), true);
						break;
				}
				return JsonResponse<bool>(true);
			} else if (request.QueryString.HasKey(X) && request.QueryString.HasKey(Z)) {
				Vector3 pos = new Vector3(float.Parse(request.QueryString.Get(X)), 0f, float.Parse(request.QueryString.Get(Z)));
				Constants.FacadeFactory.CameraFacade.GoToPos(pos);
				return JsonResponse<bool>(true);
			}
			return JsonResponse<bool>(false);
		}
	}
}
