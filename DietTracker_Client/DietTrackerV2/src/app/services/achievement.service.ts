import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Achievment} from '../classes/achievement';
import { HttpClient,HttpHeaders } from '@angular/common/http';
import { environment } from '../../environments/environment';


@Injectable({
  providedIn: 'root'
})
export class AchievementService {
  public achievments: Achievment[];
  public httpClient: HttpClient;

  headers = new HttpHeaders().set('Content-Type', 'application/json');

  constructor( httpClient: HttpClient) {
    this.httpClient = httpClient;
  }
  /* interface instead of class */
  getAll(): Observable<Achievment[]> {
    return this.httpClient.get<Achievment[]>(`${environment.apiBase}/api/activities`);
  }

  addAchievement(achievment: Achievment){
    const json = JSON.stringify(achievment);

    return this.httpClient.post('https://localhost:5001/api/Achievements', json, {headers: this.headers});
  }

  getAchievementByID(id: string){
    return this.httpClient.get<Achievment>('https://localhost:5001/api/Achievements/' + id);
  }
  // getById
  // add
  // delete
  // ...
}
