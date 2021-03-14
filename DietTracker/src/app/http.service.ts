import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class HttpService {

  http : HttpClient;

  constructor(http : HttpClient) {
    // Ist ein Angular Service 
    this.http = http;
  }
  headers = new HttpHeaders().set("Content-Type", "application/json");
/*
  getAllTasks(){
    return this.http.get<Task[]>('http://localhost:9000/api/getalltasks')
  }

  getAllUsers(){
    return this.http.get<User[]>('http://localhost:9000/api/getallusers');
  }

  addUser(user : User){
    var json = JSON.stringify(user);
    json = json.replace('T00:00:00.000Z','');
    return this.http.post('http://localhost:9000/api/adduser', json, {headers: this.headers});
  }

  addTask(task : Task){
    var json = this.JsonifyTask(task);
    console.log(json);
    return this.http.post('http://localhost:9000/api/addtask',json, {headers: this.headers});
  }

  getTaskById(id : number){
    return this.http.get<Task>('http://localhost:9000/api/gettaskbyid/'+ id);
  }

  deleteTask(id : number){
    return this.http.delete('http://localhost:9000/api/Delete/'+id);
  }

  updateTask(task : Task){
    var json = this.JsonifyTask(task);
    return this.http.post('http://localhost:9000/api/updateTask', json, {headers : this.headers});
  }

  private JsonifyTask(task:Task) : String{
    var json = JSON.stringify(task);
    json = json.replace('T00:00:00.000Z"','"');
    json = json.replace('T00:00:00+01:00', '');
    console.log(json);
    return json;
  }
  */
}
