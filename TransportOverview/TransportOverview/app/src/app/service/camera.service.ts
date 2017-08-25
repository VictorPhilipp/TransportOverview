import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import 'rxjs/Rx';
import { Observable } from 'rxjs/Observable';

@Injectable()
export class CameraService {
	constructor(
		private http : HttpClient
	) {}
	
	goToVehicle(vehicleId : number) : void {
		this.http.get('/PTO/Camera', {
			params: new HttpParams()
			.set('instanceType', 'Vehicle')
			.set('instanceId', vehicleId.toString())
		})
		.map(res => res)
		.catch(this.handleError)
		.subscribe(res => {});
	}

	goToNode(nodeId : number) : void {
		this.http.get('/PTO/Camera', {
			params: new HttpParams()
			.set('instanceType', 'Node')
			.set('instanceId', nodeId.toString())
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
