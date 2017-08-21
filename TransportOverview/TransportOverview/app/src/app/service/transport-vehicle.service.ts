import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import 'rxjs/Rx';
import { Observable } from 'rxjs/Observable';

import { TransportVehicle } from '../dto/transport-vehicle';

@Injectable()
export class TransportVehicleService {
	constructor(
		private http : HttpClient
	) {}
	
	getTransportLineVehicles(lineId : number) : Observable<TransportVehicle[]> {
		return this.http.get('/PTO/TransportVehicles')
			.map(res => <TransportVehicle[]>res)
			.catch(this.handleError);
	}

	private handleError (error: any) {
		console.error(error);
		return Promise.reject(error.message || error.json().error || 'Server error');
	}
}
