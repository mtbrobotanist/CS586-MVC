import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { PropertiesComponent } from './components/properties/properties.component';
import { TenantsComponent } from './components/tenants/tenants.component';
import { LeasesComponent } from './components/leases/leases.component';
import { LeaseDetailComponent } from './components/lease-detail/lease-detail.component';
import { TenantDetailComponent } from './components/tenant-detail/tenant-detail.component';
import { PropertyDetailComponent } from './components/property-detail/property-detail.component';

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        HomeComponent,
        PropertiesComponent,
        TenantsComponent,
        LeasesComponent,
        LeaseDetailComponent,
        TenantDetailComponent,
        PropertyDetailComponent
    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'home', component: HomeComponent },
            { path: 'properties', component: PropertiesComponent },
            { path: 'property-detail/:id', component: PropertyDetailComponent },
            { path: 'tenants', component: TenantsComponent },
            { path: 'tenant-detail/:id', component: TenantDetailComponent},
            { path: 'leases', component: LeasesComponent },
            { path: 'lease-detail/:id', component: LeaseDetailComponent },
            { path: '**', redirectTo: 'home' }
        ])
    ]
})
export class AppModuleShared {
}
