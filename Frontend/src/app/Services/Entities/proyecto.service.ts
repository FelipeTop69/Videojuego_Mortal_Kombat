import { Injectable } from '@angular/core';
import { GenericService } from '../generic.service';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment.development';
import { PedidoCreateMod, PedidoMod } from '../../Models/PedidoMod.model';

@Injectable({
  providedIn: 'root'
})
export class PedidoService extends GenericService<PedidoMod, PedidoCreateMod> {

  constructor(http: HttpClient) {
    const urlBase = environment.apiURL;
    super(http, urlBase);
  }
}