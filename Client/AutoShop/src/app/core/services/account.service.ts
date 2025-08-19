import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { environment } from '../../../environments/environment';
import { Address, User } from '../../shared/models/user';
import { map, tap } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
baseUrl = environment.baseUrl;
private http = inject(HttpClient);
currentUser = signal<User | null>(null);

login(values: any) {
  let params = new HttpParams();
  params = params.append('useCookies', true);
  console.log('Attempting login to:', this.baseUrl + 'login');
  return this.http.post(this.baseUrl + 'login', values, {params,withCredentials:true});
}
register(values: any) {
  return this.http.post(this.baseUrl+'account/register', values);
}
getUserInfo() {
  console.log('Attempting to get user info from:', this.baseUrl + 'account/user-info');
  return this.http.get<User>(this.baseUrl + 'account/user-info').pipe(
    map((user: User) => {
      console.log('Received user info:', user);
      this.currentUser.set(user);
      return user;
    })
  );
}
logout() {
  return this.http.post(this.baseUrl + 'account/logout', {});
}
updateAddress(address: Address) {
  return this.http.put(this.baseUrl + 'account/address', address).pipe(
    tap(()=>{
      this.currentUser.update(user=>{
        if(user){
          user.address=address;
        }
        return user;
      })
    }

    )
  );
}
getAuthState(){
  return this.http.get<{isAuthenticated:boolean,email:string}>(this.baseUrl+'account/auth-status');
}
}
