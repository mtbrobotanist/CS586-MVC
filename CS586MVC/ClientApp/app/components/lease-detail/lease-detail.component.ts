import {Component, OnInit, OnDestroy, Inject} from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Http } from "@angular/http";
import {Lease} from "../leases/leases.component.interfaces";

@Component({
  selector: 'app-lease-detail',
  templateUrl: './lease-detail.component.html',
  styleUrls: ['./lease-detail.component.css']
})
export class LeaseDetailComponent implements OnInit, OnDestroy {

    private leases:Lease[];
    private sub: any;
    private id:number;
    
  constructor(private http: Http, @Inject('BASE_URL') private baseUrl: string, private route: ActivatedRoute) {
      
  }

  ngOnInit() {
      this.sub = this.route.params.subscribe(params => {
          this.id = +params['id'];
          
          this.http.get(this.baseUrl + 'propertydata/leases/' + this.id.toString()).subscribe(result => {
              this.leases = result.json() as Lease[];
          }, error => console.error(error));
      });

  }
  
  ngOnDestroy() {
     this.sub.unsubscribe();
  }

}


