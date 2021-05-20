import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { IActivity } from '../interfaces/iactivity';

@Injectable({
  providedIn: 'root'
})
export class ActivityService {

  constructor(private httpClient: HttpClient) { }

  getAll(): Observable<IActivity[]> {
    return this.httpClient.get<IActivity[]>(`${environment.apiBase}/api/activities`);
  }

  // getById
  // add
  // delete
  // ...
}
