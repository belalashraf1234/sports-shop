import { HttpClient } from '@angular/common/http';
import { Component, inject } from '@angular/core';
import { MatButton } from '@angular/material/button';

@Component({
  selector: 'app-test-error',
  imports: [MatButton],
  templateUrl: './test-error.component.html',
  styleUrl: './test-error.component.scss'
})
export class TestErrorComponent {
  baseUrl="http://localhost:5001/api/";
  private http=inject(HttpClient);
  validationsErros?:string[];
  get404Error(){
    this.http.get(this.baseUrl+"Buggy/notfound").subscribe({
      next:response=>console.log(response),
      error:error=>console.log(error)
    });
  }
  get400Error(){
    this.http.get(this.baseUrl+"Buggy/badrequest").subscribe({
      next:response=>console.log(response),
      error:error=>console.log("errorbase",error)
    });
  }
  get401Error(){
    this.http.get(this.baseUrl+"Buggy/unauthorized").subscribe({
      next:response=>console.log(response),
      error:error=>console.log(error)
    });
  }
  get500Error(){
    this.http.get(this.baseUrl+"Buggy/internalerror").subscribe({
      next:response=>console.log(response),
      error:error=>console.log(error)
    });
  }
  get400ValidationError(){
    this.http.post(this.baseUrl+"Buggy/validationerror",{}).subscribe({
      next:response=>console.log(response),
      error:error=>this.validationsErros=error
    });
  }
  
}
