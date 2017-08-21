using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TransportOverview.Data {
	public class TransportPassengersData {
		public TransportPassengersGroupData adultPassengers;
		public TransportPassengersGroupData carOwningPassengers;
		public TransportPassengersGroupData childPassengers;
		public TransportPassengersGroupData residentPassengers;
		public TransportPassengersGroupData seniorPassengers;
		public TransportPassengersGroupData teenPassengers;
		public TransportPassengersGroupData touristPassengers;
		public TransportPassengersGroupData youngPassengers;

		public TransportPassengersData(ref TransportPassengerData transportPassengerData) {
			adultPassengers = new TransportPassengersGroupData(ref transportPassengerData.m_adultPassengers);
			carOwningPassengers = new TransportPassengersGroupData(ref transportPassengerData.m_carOwningPassengers);
			childPassengers = new TransportPassengersGroupData(ref transportPassengerData.m_childPassengers);
			residentPassengers = new TransportPassengersGroupData(ref transportPassengerData.m_residentPassengers);
			seniorPassengers = new TransportPassengersGroupData(ref transportPassengerData.m_seniorPassengers);
			teenPassengers = new TransportPassengersGroupData(ref transportPassengerData.m_teenPassengers);
			touristPassengers = new TransportPassengersGroupData(ref transportPassengerData.m_touristPassengers);
			youngPassengers = new TransportPassengersGroupData(ref transportPassengerData.m_youngPassengers);
		}
	}
}
