import { Injectable } from '@angular/core';
import { GenericService } from '../generic.service';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment.development';
import { PizzaMod } from '../../Models/PizzaMod.model';


@Injectable({
  providedIn: 'root'
})
export class PizzaService extends GenericService<PizzaMod, PizzaMod> {

  constructor(http: HttpClient) {
    const urlBase = environment.apiURL + "api/pizza";
    super(http, urlBase);
  }
}