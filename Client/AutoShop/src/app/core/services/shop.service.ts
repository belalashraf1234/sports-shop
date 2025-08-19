import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Pagination } from '../../shared/models/pagination';
import { Product } from '../../shared/models/product';
import { ShopParams } from '../../shared/models/shopParams';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ShopService {
baseUrl=environment.baseUrl;
  private http=inject(HttpClient);
  categories:string[]=[];
  brands:string[]=[];  
  getProducts(shopParams:ShopParams){
    let params=new HttpParams();
    if(shopParams.brands&&shopParams.brands.length>0){
      params=params.append("brands",shopParams.brands.join(","));
    }
    if(shopParams.categories&&shopParams.categories.length>0){
      params=params.append("categories",shopParams.categories.join(","));
    }
    if(shopParams.sort){
      params=params.append("sort",shopParams.sort);
    }
    if(shopParams.search){
      params=params.append("search",shopParams.search);
    }
   params= params.append("PageSize",shopParams.pageSize);
    params= params.append("PageIndex",shopParams.pageNumber);
    params= params.append("search",shopParams.search);
    console.log(params);
    
    return this.http.get<Pagination<Product>>(this.baseUrl+"Prodcut/GetAllProducts",{params});
  }
  getproduct(id:number){
    return this.http.get<Product>(this.baseUrl+"Prodcut/"+id);
  }

  getBrands(){
    if(this.brands.length>0) return;
    return this.http.get<string[]>(this.baseUrl+"Prodcut/brands").subscribe(res=>{
      this.brands=res;
      console.log(this.brands);
      
    }
     
    );
  }
  getCategories(){
    if(this.categories.length>0) return;
    return this.http.get<string[]>(this.baseUrl+"Prodcut/categories").subscribe(res=>{
      this.categories=res;
      console.log(this.categories);
      
    });
  }
}
