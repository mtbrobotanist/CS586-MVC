import {Component, OnInit, OnDestroy, Inject} from '@angular/core';
import {ActivatedRoute, Params} from '@angular/router';
import { Http, Headers } from "@angular/http";
import { Tenant } from "../tenants/tenants.component.interfaces";
import {getUrlScheme} from "@angular/compiler";
import Result = jasmine.Result;

@Component({
  selector: 'app-tenant-detail',
  templateUrl: './tenant-detail.component.html',
  styleUrls: ['./tenant-detail.component.css']
})
export class TenantDetailComponent implements OnInit, OnDestroy {

    private tenants:Tenant[];
    private id:number;
    private editMode:boolean;
    private deleted:boolean;
    
    private vmFirstName:string;
    private vmLastName:string;
    private vmPhone:string;
    private vmEmail:string;
    
    private sub:any;
    
    constructor(private http: Http,
                @Inject('BASE_URL') private baseUrl:
                    string, private route: ActivatedRoute) {

    }

  ngOnInit() {
        this.sub = this.route.params.subscribe(params => {
            this.id = +params['id'];

            this.http.get(this.getUrl()).subscribe(result => {
                
                this.tenants = result.json() as Tenant[];
                this.copyToViewModel();
                
            }, error => console.error(error));
        })
  }
  
  ngOnDestroy() {
        this.sub.unsubscribe();
  }
  
  public toggleEditMode() {
      this.editMode = !this.editMode;
  }
  
  private copyToViewModel() {
      this.vmFirstName = this.tenants[0].firstName;
      this.vmLastName = this.tenants[0].lastName;
      this.vmPhone = this.tenants[0].phone;
      this.vmEmail = this.tenants[0].email;
  }
  
  private copyToTenant() {
        this.tenants[0].firstName = this.vmFirstName;
        this.tenants[0].lastName = this.vmLastName;
        this.tenants[0].phone = this.vmPhone;
        this.tenants[0].email = this.vmEmail;
  }
  
  
  private getUrl(){
      return this.baseUrl + 'propertydata/tenants/' + this.id.toString();
  }
  
  public deleteTenant()
    {
        this.http.delete(this.getUrl()).subscribe();
        this.tenants[0].current = false;
        this.deleted = true;
    }

    
    public cancel() {
        this.toggleEditMode();
        this.copyToViewModel();
    }
    
    public submit() {
        this.toggleEditMode();
        this.copyToTenant();
        
        let headers = new Headers();
        headers.append('Content-Type', 'application/json');
        headers.append('Date', Date.now().toString());
        
        this.http
          .put(this.getUrl(), JSON.stringify(this.tenants[0]), {headers:headers})
          .subscribe(result => console.log(result),
              error => console.error(error));
    }
}
