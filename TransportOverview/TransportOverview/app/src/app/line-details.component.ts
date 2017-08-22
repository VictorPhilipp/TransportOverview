import { Component, ViewChild, ElementRef, Input, OnInit, AfterViewInit, AfterViewChecked, NgZone } from '@angular/core';
import { ActivatedRoute, ParamMap } from '@angular/router';
import { Location } from '@angular/common';
import { IntervalObservable } from 'rxjs/observable/IntervalObservable';
import 'rxjs/Rx';
//import 'rxjs/add/operator/switchMap';
import { Observable } from 'rxjs/Observable';
import { Subject } from 'rxjs/Rx';

import { TransportLine } from './dto/transport-line';
import { TransportStop } from './dto/transport-stop';
import { TransportVehicle } from './dto/transport-vehicle';
import { TransportLineService } from './service/transport-line.service';

declare var $;

@Component({
	selector: 'line-details',
	templateUrl: './line-details.component.html',
	styleUrls: ['./app.css']
})
export class LineDetailsComponent implements OnInit, AfterViewInit, AfterViewChecked {
	title = 'Transport line';
	math = null;
	line : TransportLine = null;

	@Input()
	lineId : number;
  
	constructor(
		private transportLineService : TransportLineService,
		private route : ActivatedRoute,
		private location : Location,
		private zone : NgZone
	) {
		this.math = Math;		
	}

	ngAfterViewChecked(): void {
		(<any>$("#line-map-data")).subwayMap({ debug: false });
	}

	ngAfterViewInit(): void {
		
	}

	ngOnInit() : void {
		this.route.params.subscribe(params => {
			this.lineId = params['lineId'];
		});

		/*this.route.paramMap
			.switchMap((params: ParamMap) => params.get('lineId'))
			.subscribe(lineId => {
				console.log(lineId);
				this.lineId = Number(lineId);
			});
		*/

		// set up reload timer
		let timer = Observable.timer(0, 2000);
		timer.subscribe(t => this.reloadLine());
	}

	reloadLine() : void {
		this.transportLineService.getTransportLine(this.lineId)
		.subscribe((line : TransportLine) => {
			this.line = line;
		});
	}

	trackStopById(index : number, stop : TransportStop) : number {
		return stop.id;
	}

	trackVehicleById(index : number, vehicle : TransportVehicle) : number {
		return vehicle.id;
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
