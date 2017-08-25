import { Component, ViewChild, ViewChildren, ElementRef, QueryList, Input, OnInit, AfterViewInit, AfterViewChecked, OnDestroy, NgZone } from '@angular/core';
import { ActivatedRoute, ParamMap } from '@angular/router';
import { Location } from '@angular/common';
import { IntervalObservable } from 'rxjs/observable/IntervalObservable';
import 'rxjs/Rx';
//import 'rxjs/add/operator/switchMap';
import { Observable } from 'rxjs/Observable';
import { Subject, Subscription } from 'rxjs/Rx';

import { TransportLine } from './dto/transport-line';
import { TransportStop } from './dto/transport-stop';
import { TransportVehicle } from './dto/transport-vehicle';

import { CameraService } from './service/camera.service';
import { TransportLineService } from './service/transport-line.service';
import { TransportStopService } from './service/transport-stop.service';
import { TransportVehicleService } from './service/transport-vehicle.service';
import { TransportVehiclePrefabService } from './service/transport-vehicle-prefab.service';

declare var $;

@Component({
	selector: 'line-details',
	templateUrl: './line-details.component.html',
	styleUrls: ['./app.css']
})
export class LineDetailsComponent implements OnInit, AfterViewInit, AfterViewChecked, OnDestroy {
	title = 'Transport line';
	math = null;
	line : TransportLine = null;
	allVehiclePrefabs : string[] = null;
	numColumns : number = 100;
	leftBorder : number = 5;
	selectedPrefabIndex : number = 0;
	reloadSubscription : Subscription;

	@Input()
	lineId : number;

	@ViewChild('prefabSelect')
	prefabSelect : ElementRef;
  
	constructor(
		private transportLineService : TransportLineService,
		private transportStopService : TransportStopService,
		private transportVehicleService : TransportVehicleService,
		private transportVehiclePrefabService : TransportVehiclePrefabService,
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

	ngOnDestroy(): void {
		console.log('destroy');
		this.reloadSubscription.unsubscribe();
	}

	ngAfterViewChecked(): void {
		(<any>$("#line-map-data")).subwayMap({ debug: false });
	}

	ngAfterViewInit(): void {
		
	}

	ngOnInit() : void {
		console.log('init');

		this.route.params.subscribe(params => {
			this.lineId = params['lineId'];
		});

		// set up reload timer
		this.reloadSubscription = Observable.timer(0, 2000)
		.subscribe(t => this.reloadLine());
	}

	reloadLine() : void {
		this.transportLineService.getTransportLine(this.lineId)
		.subscribe((line : TransportLine) => {
			this.transportVehiclePrefabService.getTransportVehiclePrefabs(line.service, line.subService, line.level).subscribe(p => {
				this.zone.run(() => {
					this.allVehiclePrefabs = p;
					this.line = line;
				});
				this.enableUI();
			});
		});
	}

	trackStopById(index : number, stop : TransportStop) : number {
		return stop.id;
	}

	trackVehicleById(index : number, vehicle : TransportVehicle) : number {
		return vehicle.id;
	}

	toggleAllowedVehiclePrefab(e : any) : void {
		if (e.target.checked) {
			this.getTransportVehiclePrefabService().addPrefabToTransportLine(this.lineId, e.target.value);
		} else {
			this.getTransportVehiclePrefabService().removePrefabFromTransportLine(this.lineId, e.target.value);
		}
	}

	addTransportVehicle(lineId : number, index : number) : void {
		this.disableUI();
		this.getTransportVehicleService().addTransportVehicle(lineId, index);
		// select a random prefab
		this.selectedPrefabIndex = this.prefabSelect.nativeElement.selectedIndex = Math.floor((Math.random() * this.prefabSelect.nativeElement.length));
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

	getTransportVehiclePrefabService() : TransportVehiclePrefabService {
		return this.transportVehiclePrefabService;
	}

	getCameraService() : CameraService {
		return this.cameraService;
	}
}
