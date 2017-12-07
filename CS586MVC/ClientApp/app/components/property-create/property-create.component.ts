import {Component, OnInit, OnDestroy, Inject} from '@angular/core';
import {ActivatedRoute} from '@angular/router';
import {Http, Headers} from "@angular/http";
import {ApartmentComplex} from "../properties/properties.component.interfaces";

@Component({
    selector: 'app-property-create',
    templateUrl: './property-create.component.html',
    styleUrls: ['./property-create.component.css']
})

export class PropertyCreateComponent implements OnInit, OnDestroy {

    private vmName: string;
    private vmAddress: string;
    private vmTotalUnits: string;

    private complex: ApartmentComplex;

    constructor(private http: Http,
                @Inject('BASE_URL') private baseUrl: string, 
                private route: ActivatedRoute) {
    }

    ngOnInit() {
        this.complex = new ApartmentComplex();
    }

    ngOnDestroy() {

    }

    public cancel() {
        this.vmName = "";
        this.vmAddress = "";
        this.vmTotalUnits = "";
    }
    
    public submit() {
        this.copyToApartmentComplex();

        let headers = new Headers();
        headers.append('Content-Type', 'application/json');
        headers.append('Date', Date.now().toString());

        let url = this.baseUrl + "propertydata/properties";

        return this.http
            .post(url, JSON.stringify(this.complex), {headers: headers})
            .subscribe(result => {console.log(result);},
                error => {console.log(error)});
    }

    private copyToApartmentComplex() {
        this.complex.name = this.vmName;
        this.complex.address = this.vmAddress;
        this.complex.size = parseInt(this.vmTotalUnits);
    }

}
