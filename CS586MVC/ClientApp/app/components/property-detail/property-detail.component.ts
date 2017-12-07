import {Component, OnInit, OnDestroy, Inject} from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Http, Headers } from "@angular/http";
import {ApartmentComplex} from "../properties/properties.component.interfaces";

@Component({
  selector: 'app-property-detail',
  templateUrl: './property-detail.component.html',
  styleUrls: ['./property-detail.component.css']
})

export class PropertyDetailComponent implements OnInit {

    private complexes:ApartmentComplex[];
    private sub: any;
    private id:number;
    private editMode:boolean = false;
    private deleted:boolean = false;
    
    private vmName:string;
    private vmAddress:string;
    private vmTotalUnits:number;
    
    constructor(private http: Http,
                @Inject('BASE_URL') private baseUrl:
                    string, private route: ActivatedRoute) {
    }

    ngOnInit() {
        this.sub = this.route.params.subscribe(params => {
            this.id = +params['id'];

            this.http.get(this.getUrl()).subscribe(result => {

                this.complexes = result.json() as ApartmentComplex[];
                this.copyToViewModel();

            }, error => console.error(error));
        });

    }
    
    ngOnDestroy() {
        this.sub.unsubscribe();
    }
    
    private getUrl(){
        return this.baseUrl + 'propertydata/properties/' + this.id.toString();
    }
    
    toggleEditMode() {
        this.editMode = !this.editMode;
    }
    
    public cancel() {
        this.toggleEditMode();
        this.copyToViewModel();
    }
    
    public submit() {
        this.toggleEditMode();
        this.copytToCompex();

        let headers = new Headers();
        headers.append('Content-Type', 'application/json');
        headers.append('Date', Date.now().toString());

        let url = this.baseUrl + "propertydata/properties/" + this.id.toString();

        return this.http
            .put(url, JSON.stringify(this.complexes[0]), {headers: headers})
            .subscribe(result => {console.log(result);},
                error => {console.log(error)});
    }

    private copyToViewModel() {
        this.vmName = this.complexes[0].name;
        this.vmAddress = this.complexes[0].address;
        this.vmTotalUnits = this.complexes[0].size;
    }
    
    private copytToCompex()
    {
        this.complexes[0].name = this.vmName;
        this.complexes[0].address = this.vmAddress;
        this.complexes[0].size = this.vmTotalUnits;
        this.complexes[0].vacancyCount = this.vmTotalUnits - this.complexes[0].occupiedCount;
    }

}
