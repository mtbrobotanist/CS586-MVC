import {Component, OnInit, OnDestroy, Inject} from '@angular/core';
import {ActivatedRoute, Params} from '@angular/router';
import {Http, Headers} from "@angular/http";
import {ApartmentComplexUnit, Lease} from "../leases/leases.component.interfaces";
import {Tenant} from "../tenants/tenants.component.interfaces";
import {ApartmentComplex} from "../properties/properties.component.interfaces";

@Component({
    selector: 'app-lease-create',
    templateUrl: './lease-create.component.html',
    styleUrls: ['./lease-create.component.css']
})

export class LeaseCreateComponent implements OnInit, OnDestroy {

    public lease: Lease;
    public tenants: Tenant[];
    public complexes: ApartmentComplex[];

    public vmTenant: Tenant;
    public vmComplexUnit: ApartmentComplexUnit;
    public vmComplex: ApartmentComplex;

    public vmStartDate: string;
    public vmDuration: string;
    public vmRent: string;
    public vmUnitNumber: string;
    
    constructor(private http: Http,
                @Inject('BASE_URL') private baseUrl: string,
                private route: ActivatedRoute) {

    }

    ngOnInit() {
        this.lease = new Lease();
        this.vmComplexUnit = new ApartmentComplexUnit();
        this.getApartmentComplexes();
        this.getTenants();
    }

    ngOnDestroy() {
    
    }


    private getTenants() {
        this.http
            .get(this.baseUrl + 'propertydata/tenants').subscribe(result => {
                this.tenants = result.json() as Tenant[];
        }, error => console.error(error));
    }

    private getApartmentComplexes() {
        this.http
            .get(this.baseUrl + 'propertydata/properties').subscribe(result => {
                this.complexes = result.json() as ApartmentComplex[];
        }, error => console.error(error));
    }

    cancel() {
        this.lease =  new Lease();
        this.vmTenant = new Tenant();
        this.vmComplexUnit = new ApartmentComplexUnit();
        this.vmComplex = new ApartmentComplex();

        this.vmStartDate = "";
        this.vmDuration = "";
        this.vmRent = "";
        this.vmUnitNumber = "";
    }

    submit() {
        this.copytoLease();

        let headers = new Headers();
        headers.append('Content-Type', 'application/json');
        headers.append('Date', Date.now().toString());

        let url = this.baseUrl + "propertydata/leases";

        return this.http
            .post(url, JSON.stringify(this.lease), {headers: headers})
            .subscribe(result => console.log(result),
                       error => console.log(error));
    }

    private copytoLease() {
        this.vmComplexUnit.apartmentComplexId = this.vmComplex.id;
        
        this.lease.apartmentComplexUnit = this.vmComplexUnit;

        this.lease.tenantId = this.vmTenant.id;
        this.lease.tenant = this.vmTenant;
        
        this.lease.startDate = Date.parse(this.vmStartDate);
        this.lease.durationMonths = parseInt(this.vmDuration);
        this.lease.rentMonthly = parseInt(this.vmRent);
        this.lease.apartmentComplexUnit.unitNumber = parseInt(this.vmUnitNumber);
    }
}
