import { Injectable } from '@angular/core';
import { ServicesModule } from './services.module';

@Injectable({
  providedIn: ServicesModule
})
export class LocalStorageService {

  constructor() { }
  set(key: string, value: any) {
    localStorage.setItem(key, JSON.stringify(value))
    return 1
  }
  get(key: string) {
    return JSON.parse(localStorage.getItem(key))
  }
  remove(key: string) {
    localStorage.removeItem(key)
  }

}
