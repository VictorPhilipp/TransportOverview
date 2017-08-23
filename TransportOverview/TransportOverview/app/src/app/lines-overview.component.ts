import { Component, ViewChild, ElementRef } from '@angular/core';
import { OnInit, AfterViewChecked } from '@angular/core';
import { IntervalObservable } from 'rxjs/observable/IntervalObservable';
import { HttpClient } from "@angular/common/http";
import 'rxjs/Rx';
import { Observable} from 'rxjs/Observable';
import { Subject } from 'rxjs/Rx';
import { DataTableDirective } from 'angular-datatables';

import { TransportLine } from './dto/transport-line';
import { TransportStop } from './dto/transport-stop';
import { TransportVehicle } from './dto/transport-vehicle';
import { TransportLineService } from './service/transport-line.service';

declare var $;

@Component({
	selector: 'lines-overview',
	templateUrl: './lines-overview.component.html',
	styleUrls: ['./app.css']
})
export class LinesOverviewComponent implements OnInit, AfterViewChecked {
	title = 'Lines overview';
	
	lines : TransportLine[] = null;
	math = null;
	
	@ViewChild(DataTableDirective)
    dtElement: DataTableDirective;
	
	@ViewChild('reloadLinesButton')
	reloadLinesButton : ElementRef;
	
	dtTrigger: Subject<any> = new Subject();
	dtOptions: DataTables.Settings = {};
  
	constructor(
		private transportLineService : TransportLineService
	) {
		this.math = Math;
		this.dtOptions = {
			pageLength : 25,
			order: [[8, 'desc'], [15, 'asc']]
		};
	}
	
	ngAfterViewChecked(): void {
		
	}

	ngOnInit() : void {
		this.reloadLines();
	}
	
	reloadLines() : void {
		this.reloadLinesButton.nativeElement.disabled = true;
		
		this.transportLineService.getTransportLines()
			.subscribe((lines : TransportLine[]) => {
				this.lines = lines;

				if (this.dtElement.dtInstance != null) {
					this.dtElement.dtInstance.then((dtInstance: DataTables.Api) => {
					// Destroy the table first
					dtInstance.destroy();
					// Call the dtTrigger to rerender again
					this.dtTrigger.next();
				  });
				} else {
					this.dtTrigger.next();
				}

				this.reloadLinesButton.nativeElement.disabled = false;
			});
	}
	
	trackStopById(index : number, stop : TransportStop) : number {
		return stop.id;
	}

	trackVehicleById(index : number, vehicle : TransportVehicle) : number {
		return vehicle.id;
	}

	getTransportLineService() : TransportLineService {
		return this.transportLineService;
	}
}
