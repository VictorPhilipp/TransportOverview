import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import 'rxjs/Rx';
import { Observable } from 'rxjs/Observable';

import { TransportStop } from '../dto/transport-stop';

@Injectable()
export class TransportStopService {
	constructor(
		private http : HttpClient
	) {}
	
	getTransportLineStops(lineId : number) : Observable<TransportStop[]> {
		return this.http.get('/PTO/TransportStops')
			.map(res => <TransportStop[]>res)
			.catch(this.handleError);
	}

	private handleError (error: any) {
		console.error(error);
		return Promise.reject(error.message || error.json().error || 'Server error');
	}
}
