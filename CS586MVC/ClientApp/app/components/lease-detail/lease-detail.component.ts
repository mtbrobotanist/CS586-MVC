import {Component, OnInit, OnDestroy, Inject} from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Http, Headers } from "@angular/http";
import { Lease } from "../leases/leases.component.interfaces";

@Component({
  selector: 'app-lease-detail',
  templateUrl: './lease-detail.component.html',
  styleUrls: ['./lease-detail.component.css']
})
export class LeaseDetailComponent implements OnInit, OnDestroy {

    private leases:Lease[];
    private sub: any;
    private id:number;
    private editMode:boolean = false;
    private deleted:boolean = false;
    
    private actualStartDate:string;
    
    private vmStartDate:string;
    private vmDuration:number;
    private vmRent:number;
    private vmUnitNumber:number;
    
    constructor(private http: Http, 
                @Inject('BASE_URL') private baseUrl: 
                    string, private route: ActivatedRoute) {
      
    }

    ngOnInit() {
        this.sub = this.route.params.subscribe(params => {
          this.id = +params['id'];
          
          let url = this.baseUrl + 'propertydata/leases/' + this.id.toString();
          
          this.http.get(url).subscribe(result => {
              
              this.leases = result.json() as Lease[];
              this.copyToViewModel();
              
          }, error => console.error(error));
        });

    }
  
    ngOnDestroy() {
        this.sub.unsubscribe();
    }
    
    toggleEditMode() {
        this.editMode = !this.editMode;
    }
    
    submit()
    {
        this.toggleEditMode();
        this.copytoLease();
        
        let headers = new Headers();
        headers.append('Content-Type', 'application/json');
        headers.append('Date', Date.now().toString());
        
        let url = this.baseUrl + "propertydata/leases/" + this.id.toString();
        
        return this.http
            .put(url, JSON.stringify(this.leases[0]), {headers: headers})
            .subscribe(result => {console.log(result);},
                     error => {console.log(error)});
    }
    
    cancel()
    {
        this.toggleEditMode();
        this.copyToViewModel();
    }
    
    private copyToViewModel()
    {
        this.vmStartDate = new Date(this.leases[0].startDate).toDateString();
        this.actualStartDate = this.vmStartDate;
        this.vmDuration = this.leases[0].durationMonths;
        this.vmRent = this.leases[0].rentMonthly;
        this.vmUnitNumber = this.leases[0].apartmentComplexUnit.unitNumber;
    }
    
    private copytoLease()
    {
        this.leases[0].startDate = Date.parse(this.vmStartDate);
        this.actualStartDate = this.vmStartDate;
        this.leases[0].durationMonths = this.vmDuration;
        this.leases[0].rentMonthly = this.vmRent;
        this.leases[0].apartmentComplexUnit.unitNumber = this.vmUnitNumber;
    }
    
    public deleteLease()
    {
        let url = this.baseUrl + 'propertydata/leases/' + this.id.toString();
        this.http.delete(url).subscribe();
        this.deleted = true;
    }
}


