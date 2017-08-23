import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import 'rxjs/Rx';
import { Observable } from 'rxjs/Observable';

import { TransportLine } from '../dto/transport-line';

@Injectable()
export class TransportLineService {
	constructor(
		private http : HttpClient
	) {}
	
	getTransportLines() : Observable<TransportLine[]> {
		return this.http.get('/PTO/TransportLines')
			.map(res => <TransportLine[]>res)
			.catch(this.handleError);
	}

	getTransportLine(lineId : number) : Observable<TransportLine> {
		return this.http.get('/PTO/TransportLines', {
				params: new HttpParams().set('lineId', lineId.toString())
			})
			.map(res => <TransportLine>res)
			.catch(this.handleError);
	}
	
	getTotalLineIncome(line : TransportLine) : string {
		var ret = 0;
		for (var i = 0; i < line.vehicles.length; ++i) {
			ret += line.vehicles[i].lastWeekIncome;
		}
		return (ret / 100).toFixed(2);
	}
	
	getTotalNumLinePassengersLastWeek(line : TransportLine) : number {
		var ret = 0;
		for (var i = 0; i < line.vehicles.length; ++i) {
			ret += line.vehicles[i].lastWeekNumPassengers;
		}
		return ret;
	}
	
	getTotalNumLineWaitingPassengers(line : TransportLine) : number {
		var ret = 0;
		for (var i = 0; i < line.stops.length; ++i) {
			ret += line.stops[i].numWaitingPassengers;
		}
		return ret;
	}
	
	getAverageLineIncome(line : TransportLine) : string {
		var ret = 0;
		for (var i = 0; i < line.vehicles.length; ++i) {
			ret += line.vehicles[i].averageIncome;
		}
		return (ret / 100).toFixed(2);
	}
	
	getAverageNumLinePassengers(line : TransportLine) : number {
		var ret = 0;
		for (var i = 0; i < line.vehicles.length; ++i) {
			ret += line.vehicles[i].averageNumPassengers;
		}
		return ret;
	}
	
	getTotalNumLinePassengersOnBoard(line : TransportLine) : number {
		var ret = 0;
		for (var i = 0; i < line.vehicles.length; ++i) {
			ret += line.vehicles[i].numPassengers;
		}
		return ret;
	}
	
	getTotalMaxNumLinePassengers(line : TransportLine) : number {
		var ret = 0;
		for (var i = 0; i < line.vehicles.length; ++i) {
			ret += line.vehicles[i].maxNumPassengers;
		}
		return ret;
	}

	private handleError (error : any) {
		console.error(error);
		return Promise.reject(error.message || error.json().error || 'Server error');
	}
}
