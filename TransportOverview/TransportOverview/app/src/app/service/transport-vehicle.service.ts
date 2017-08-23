import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
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

	addTransportVehicle(lineId : number, index : number) : void {
		this.http.get('/PTO/TransportVehicles', {
				params: new HttpParams()
				.set('action', 'add')
				.set('lineId', lineId.toString())
				.set('index', index.toString())
			})
			.map(res => res)
			.catch(this.handleError)
			.subscribe(res => {});
	}

	removeTransportVehicle(lineId : number, index : number) : void {
		this.http.get('/PTO/TransportVehicles', {
				params: new HttpParams()
				.set('action', 'remove')
				.set('lineId', lineId.toString())
				.set('index', index.toString())
			})
			.map(res => res)
			.catch(this.handleError)
			.subscribe(res => {});
	}

	private handleError (error: any) {
		console.error(error);
		return Promise.reject(error.message || error.json().error || 'Server error');
	}
}
