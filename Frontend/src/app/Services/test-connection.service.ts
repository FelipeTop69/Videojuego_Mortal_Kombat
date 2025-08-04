import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { Test } from '../Models/Test.model';
import { GenericService } from './generic.service';

@Injectable({
  providedIn: 'root'
})
export class TestConnectionService extends GenericService<any> {
  constructor(http:HttpClient){
    const baseURL = environment.apiURL + 'WeatherForecast/'
    super(http, baseURL)
  }
}
