import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import 'rxjs/Rx';
import { Observable } from 'rxjs/Observable';

@Injectable()
export class TransportVehiclePrefabService {
	constructor(
		private http : HttpClient
	) {}
	
	getTransportVehiclePrefabs(service : string, subService : string, level : string) : Observable<string[]> {
		return this.http.get('/PTO/TransportVehiclePrefabs', {
			params: new HttpParams()
				.set('service', service)
				.set('subService', subService)
				.set('level', level)
			})
			.map(res => <string[]>res)
			.catch(this.handleError);
	}

	addPrefabToTransportLine(lineId : number, index : number) : void {
		this.http.get('/PTO/TransportVehiclePrefabs', {
			params: new HttpParams()
				.set('action', 'add')
				.set('lineId', lineId.toString())
				.set('index', index.toString())
			})
			.map(res => res)
			.catch(this.handleError)
			.subscribe(res => {});
	}

	removePrefabFromTransportLine(lineId : number, index : number) : void {
		this.http.get('/PTO/TransportVehiclePrefabs', {
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
