import { Component, ViewChild, ElementRef } from '@angular/core';
import { OnInit } from '@angular/core';
import { IntervalObservable } from 'rxjs/observable/IntervalObservable';
import { HttpClient } from "@angular/common/http";
import 'rxjs/Rx';
import { Observable} from 'rxjs/Observable';
import { Subject } from 'rxjs/Rx';
import { DataTableDirective } from 'angular-datatables';

import { TransportLine } from './dto/transport-line';
import { TransportLineService } from './service/transport-line.service';

@Component({
	selector: 'lines-overview',
	templateUrl: './lines-overview.component.html',
	styleUrls: ['./app.css']
})
export class LinesOverviewComponent implements OnInit {
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
		this.dtOptions = {};
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
