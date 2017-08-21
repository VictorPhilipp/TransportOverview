import { Vector3 } from './vector3';
import { Balance } from './balance';

export class TransportStop {
	id : number;
	name : string;
	position : Vector3;
	districtName : string;
	relLinePos : number;
	problems : number;
	numWaitingPassengers : number;
	lastWeekServedPassengers : Balance;
}