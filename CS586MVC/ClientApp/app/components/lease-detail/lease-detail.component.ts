import {Component, OnInit, OnDestroy, Inject} from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Http, Headers } from "@angular/http";
//import { EditableTableModule } from "ng-editable-table/editable-table/editable-table.module";
import { Lease } from "../leases/leases.component.interfaces";


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
          
          this.http.get(this.baseUrl + 'propertydata/leases/' + this.id.toString()).subscribe(result => {
              this.leases = result.json() as Lease[];
              
              this.trialLease = Object.assign(this.leases[0]);
              
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
      
      let url = this.baseUrl + "/propertydata/leases/" + this.trialLease.id;
      
      return this.http
          .put(url, JSON.stringify(this.trialLease), {headers: headers})
          .subscribe();
  }
  
  cancel()
  {
      this.toggleEditMode();
      this.trialLease = Object.assign(this.leases[0]);
  }

}


