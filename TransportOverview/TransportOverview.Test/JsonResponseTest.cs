using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using TransportOverview.Data;
using UnityEngine;

namespace TransportOverview.Test {
	[TestClass]
	public class JsonResponseTest {
		[TestMethod]
		public void TestJsonResponse() {
			List<TransportLineData> lines = new List<TransportLineData>();
			TransportLineData line = new TransportLineData();

			line.type = ItemClass.SubService.PublicTransportBus;
			//line.color = Color.white;
			line.name = "Test";
			line.flags = (uint)TransportLine.Flags.Created;
			line.budgetInPercent = 55;
			line.problems = (uint)Notification.Problem.NoCustomers;
			line.passengerStats = default(TransportPassengersData);

			line.passengerStats.adultPassengers.average = 32;
			line.passengerStats.adultPassengers.total = 50;

			line.passengerStats.carOwningPassengers.average = 33;
			line.passengerStats.carOwningPassengers.total = 51;

			line.passengerStats.childPassengers.average = 34;
			line.passengerStats.childPassengers.total = 52;

			line.passengerStats.residentPassengers.average = 35;
			line.passengerStats.residentPassengers.total = 53;

			line.passengerStats.seniorPassengers.average = 36;
			line.passengerStats.seniorPassengers.total = 54;

			line.passengerStats.teenPassengers.average = 37;
			line.passengerStats.teenPassengers.total = 55;

			line.passengerStats.touristPassengers.average = 38;
			line.passengerStats.touristPassengers.total = 56;

			line.passengerStats.youngPassengers.average = 39;
			line.passengerStats.youngPassengers.total = 57;

			line.targetNumVehicles = 12;

			List<TransportVehicleData> vehicles = new List<TransportVehicleData>();
			TransportVehicleData vehicle0 = new TransportVehicleData();
			vehicle0.name = "Victor's Bus";
			vehicle0.relLinePos = 0.42f;
			vehicle0.numPassengers = 42;
			vehicle0.numPassengers = 100;
			vehicle0.lastWeekNumPassengers = 250;
			vehicle0.lastWeekIncome = 902;
			vehicles.Add(vehicle0);

			TransportVehicleData vehicle1 = new TransportVehicleData();
			vehicle1.name = "Elle's Bus";
			vehicle1.relLinePos = 0.84f;
			vehicle1.numPassengers = 24;
			vehicle1.numPassengers = 50;
			vehicle1.lastWeekNumPassengers = 140;
			vehicle1.lastWeekIncome = -102;
			vehicles.Add(vehicle1);

			line.vehicles = vehicles.ToArray();

			List<TransportStopData> stops = new List<TransportStopData>();
			TransportStopData stop0 = new TransportStopData();
			stop0.name = "Stop #1";
			stop0.districtName = "Some district";
			stop0.relLinePos = 0f;
			stop0.problems = (uint)Notification.Problem.None;
			stop0.numWaitingPassengers = 59;
			stop0.lastWeekServedPassengers = new BalanceData(25, 80);
			stops.Add(stop0);

			TransportStopData stop1 = new TransportStopData();
			stop1.name = "Stop #2";
			stop1.districtName = "Some other district";
			stop1.relLinePos = 0.25f;
			stop1.problems = (uint)Notification.Problem.NoCustomers;
			stop1.numWaitingPassengers = 12;
			stop1.lastWeekServedPassengers = new BalanceData(41, 2);
			stops.Add(stop1);

			line.stops = stops.ToArray();

			lines.Add(line);

			var writer = new JsonFx.Json.JsonWriter();
			string output = writer.Write(lines.ToArray());
			System.Diagnostics.Debug.WriteLine(output);
		}
	}
}
