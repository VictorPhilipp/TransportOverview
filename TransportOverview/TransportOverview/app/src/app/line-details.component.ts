import { Component, ViewChild, ViewChildren, ElementRef, QueryList, Input, OnInit, AfterViewInit, AfterViewChecked, NgZone } from '@angular/core';
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

import { CameraService } from './service/camera.service';
import { TransportLineService } from './service/transport-line.service';
import { TransportStopService } from './service/transport-stop.service';
import { TransportVehicleService } from './service/transport-vehicle.service';

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
	numColumns : number = 100;
	leftBorder : number = 5;
	selectedPrefabIndex : number = 0;

	@Input()
	lineId : number;
  
	constructor(
		private transportLineService : TransportLineService,
		private transportStopService : TransportStopService,
		private transportVehicleService : TransportVehicleService,
		private cameraService : CameraService,
		private route : ActivatedRoute,
		private location : Location,
		private zone : NgZone
	) {
		this.math = Math;
		window['lineDetailsComponent'] = {
			instance: this,
			zone: zone
		};
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
			this.enableUI();
		});
	}

	trackStopById(index : number, stop : TransportStop) : number {
		return stop.id;
	}

	trackVehicleById(index : number, vehicle : TransportVehicle) : number {
		return vehicle.id;
	}

	addTransportVehicle(lineId : number, index : number) : void {
		this.disableUI();
		this.getTransportVehicleService().addTransportVehicle(lineId, index);
	}

	removeTransportVehicle(lineId : number, index : number) : void {
		this.disableUI();
		this.getTransportVehicleService().removeTransportVehicle(lineId, index);
	}

	enableUI() : void {
		$('button').prop('disabled', false);
	}

	disableUI() : void {
		$('button').prop('disabled', true);
	}

	getTransportLineService() : TransportLineService {
		return this.transportLineService;
	}

	getTransportStopService() : TransportStopService {
		return this.transportStopService;
	}

	getTransportVehicleService() : TransportVehicleService {
		return this.transportVehicleService;
	}

	getCameraService() : CameraService {
		return this.cameraService;
	}
}
