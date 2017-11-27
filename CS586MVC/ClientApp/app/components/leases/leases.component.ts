import { Component, Inject } from '@angular/core';
import { Http } from "@angular/http";
import {Lease} from "./leases.component.interfaces";

@Component({
  selector: 'app-leases',
  templateUrl: './leases.component.html',
  styleUrls: ['./leases.component.css']
})
export class LeasesComponent {

    public leases:Lease[];

    constructor(private http: Http, @Inject('BASE_URL') private baseUrl: string) {
        http.get(baseUrl + 'propertydata/leases').subscribe(result => {
            this.leases = result.json() as Lease[];
        }, error => console.error(error));
    }
    
    public getPrettyDate(startDate:number)
    {
        return new Date(startDate).toDateString();
    }
    
    public deleteLease(id:number)
    {
        let url = this.baseUrl + 'propertydata/leases/' + id.toString();
        this.http.delete(url).subscribe();
        let deletedLease = this.getLease(id);
        
        if (deletedLease != null)
        {
            deletedLease.tenant.current = false;
        }
    }
    
    private getLease(id:number)
    {
        for (var i = 0; i < this.leases.length; ++i)
        {
            if (this.leases[i].id === id)
            {
                return this.leases[i];
            }
            return null;
        }
    }
  
}
