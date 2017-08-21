import { Vector3 } from './vector3';

export class TransportVehicle {
	id : number;
	name : string;
	relLinePos : number;
	position : Vector3;
	numPassengers : number;
	maxNumPassengers : number;
	lastWeekNumPassengers : number;
	averageNumPassengers : number;
	lastWeekIncome : number;
	averageIncome : number;
}