import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { DataTablesModule } from "angular-datatables";

import { CameraService } from "./service/camera.service";
import { TransportLineService } from "./service/transport-line.service";
import { TransportStopService } from "./service/transport-stop.service";
import { TransportVehicleService } from "./service/transport-vehicle.service";

import { AppComponent } from './app.component';
import { LinesOverviewComponent } from './lines-overview.component';
import { LineDetailsComponent } from './line-details.component';

import { RouterModule } from '@angular/router';

@NgModule({
	declarations: [
		AppComponent,
		LinesOverviewComponent,
		LineDetailsComponent
	],
	imports: [
		RouterModule.forRoot([
			{
				path: 'lines-overview',
				component: LinesOverviewComponent
			},
			{
				path: 'line-details/:lineId',
				component: LineDetailsComponent
			},
			{
				path: '',
				component: LinesOverviewComponent,
				//redirectTo: '/lines-overview',
				pathMatch: 'full',
				//skipLocationChange: true
			},
		]),
		BrowserModule,
		HttpClientModule,
		DataTablesModule
	],
	providers: [
		CameraService,
		TransportLineService,
		TransportStopService,
		TransportVehicleService
	],
	bootstrap: [AppComponent]
})
export class AppModule { }
