import { Color } from './color';
import { TransportPassengers } from './transport-passengers';
import { TransportVehicle } from './transport-vehicle';
import { TransportStop } from './transport-stop';

export class TransportLine {
	id : number;
	type : string;
	color : Color;
	name : string;
	flags : number;
	budgetInPercent : number;
	problems : number;
	passengerStats : TransportPassengers;
	targetNumVehicles : number;
	vehicles : TransportVehicle[];
	stops : TransportStop[];
	enqueuedVehiclePrefabs : string[];
	allowedVehiclePrefabs : string[];
}