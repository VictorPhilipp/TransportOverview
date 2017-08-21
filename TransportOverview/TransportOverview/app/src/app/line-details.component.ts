import { Component, ViewChild, ElementRef, Input, OnInit } from '@angular/core';
import { ActivatedRoute, ParamMap } from '@angular/router';
import { Location } from '@angular/common';
import { IntervalObservable } from 'rxjs/observable/IntervalObservable';
import 'rxjs/Rx';
//import 'rxjs/add/operator/switchMap';
import { Observable } from 'rxjs/Observable';
import { Subject } from 'rxjs/Rx';

import { TransportLine } from './dto/transport-line';
import { TransportLineService } from './service/transport-line.service';

@Component({
	selector: 'line-details',
	templateUrl: './line-details.component.html',
	styleUrls: ['./app.css']
})
export class LineDetailsComponent implements OnInit {
	title = 'Transport line';
	math = null;

	@Input()
	lineId;
  
	constructor(
		private transportLineService : TransportLineService,
		private route : ActivatedRoute,
		private location : Location
	) {
		this.math = Math;		
	}
	
	ngOnInit() : void {
		this.route.paramMap
			.switchMap((params: ParamMap) => params.get('lineId'))
			.subscribe(lineId => this.lineId = lineId);
		this.reloadLine();
	}

	reloadLine() : void {
		this.transportLineService.getTransportLine(this.lineId);
	}
	
	getTotalLineIncome(line : TransportLine) : string {
		return this.transportLineService.getTotalLineIncome(line);
	}
	
	getTotalNumLinePassengersLastWeek(line : TransportLine) : number {
		return this.transportLineService.getTotalNumLinePassengersLastWeek(line);
	}
	
	getTotalNumLineWaitingPassengers(line : TransportLine) : number {
		return this.transportLineService.getTotalNumLineWaitingPassengers(line);
	}
	
	getAverageLineIncome(line : TransportLine) : string {
		return this.transportLineService.getAverageLineIncome(line);
	}
	
	getAverageNumLinePassengers(line : TransportLine) : number {
		return this.transportLineService.getAverageNumLinePassengers(line);
	}
	
	getTotalNumLinePassengersOnBoard(line : TransportLine) : number {
		return this.transportLineService.getTotalNumLinePassengersOnBoard(line);
	}
	
	getTotalMaxNumLinePassengers(line : TransportLine) : number {
		return this.transportLineService.getTotalMaxNumLinePassengers(line);
	}
}
