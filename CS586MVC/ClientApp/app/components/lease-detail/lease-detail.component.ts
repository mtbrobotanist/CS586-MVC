import {Component, OnInit, OnDestroy, Inject} from '@angular/core';
import * as _angular_ from 'angular';
import { ActivatedRoute } from '@angular/router';
import { Http, Headers } from "@angular/http";
import { EditableTableModule } from "ng-editable-table/editable-table/editable-table.module";
import { ApartmentComplexUnit, Lease} from "../leases/leases.component.interfaces";

declare global {
    const angular: typeof _angular_;
}

@Component({
  selector: 'app-lease-detail',
  templateUrl: './lease-detail.component.html',
  styleUrls: ['./lease-detail.component.css']
})
export class LeaseDetailComponent implements OnInit, OnDestroy {

    private leases:Lease[];
    private trialLease:Lease;
    private sub: any;
    private id:number;
    private editMode:boolean = false;
    
    constructor(private http: Http, @Inject('BASE_URL') private baseUrl: string, private route: ActivatedRoute) {
      
    }

    ngOnInit() {
        this.sub = this.route.params.subscribe(params => {
          this.id = +params['id'];
          
          let url = this.baseUrl + 'propertydata/leases/' + this.id.toString();
          
          this.http.get(url).subscribe(result => {
              this.leases = result.json() as Lease[];
              
              this.copyToTrialLease();
              
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
        
        let headers = new Headers();
        headers.append('Content-Type', 'application/json');
        headers.append('Date', Date.now().toString());
        
        let url = this.baseUrl + "propertydata/leases/" + this.id.toString();
        
        return this.http
            .put(url, JSON.stringify(this.trialLease), {headers: headers})
            .subscribe(result => {console.log(result);},
                     error => {console.log(error)});
        }
    
    cancel()
    {
        this.toggleEditMode();
        this.copyToTrialLease();
    }
    
    private copyToTrialLease() 
    {
        angular.copy(this.leases[0], this.trialLease);
    }
}


