import { inject, Injectable } from '@angular/core';
import { ConfirmationToken, loadStripe, Stripe, StripeAddressElement, StripeAddressElementOptions, StripeElements, StripePaymentElement } from '@stripe/stripe-js';
import { environment } from '../../../environments/environment';
import { CartService } from './cart.service';
import { HttpClient } from '@angular/common/http';
import { Cart } from '../../shared/models/cart';
import { firstValueFrom, map } from 'rxjs';
import { AccountService } from './account.service';
@Injectable({
  providedIn: 'root'
})
export class StripeService {
  private stripePromise?:Promise<Stripe|null>;
  baseUrl=environment.baseUrl;
  cartService=inject(CartService);
  accountService=inject(AccountService);
  http=inject(HttpClient);
  private elements?:StripeElements;
  private addressElement?:StripeAddressElement;
  private paymentElement?:StripePaymentElement;


  constructor() { 
    this.stripePromise=loadStripe(environment.Publishablekey);
  }

  async initializeElements(){
    if(!this.elements){
      const stripe=await this.getStripeInstance();
      if(stripe){
        const cart=await firstValueFrom(this.createOrUpdatePPaymentIntent());
        this.elements=stripe.elements(
          {
            clientSecret:cart.clientSecret,
            appearance:{labels:"floating"}
          }
        );
      }
      else{
        throw new Error("stripe has not benn loaded");
      }
    }
    return this.elements;
  }
  async createAddressElements(){
    if(!this.addressElement){
      const elements=await this.initializeElements();
      const user=this.accountService.currentUser();
      let defaaultValues:StripeAddressElementOptions['defaultValues']={};

      if(user){
        defaaultValues.name=user.firstName + ' ' + user.lastName;
        
      }
      if(user?.address){
        defaaultValues.address={
          line1:user.address.line1,
          line2:user.address.line2,
          city:user.address.city,
          state:user.address.state,
          postal_code:user.address.postalCode,
          country:user.address.country
        }
      }
      if(elements){
        const options:StripeAddressElementOptions={
          mode:'shipping',
          defaultValues:defaaultValues
        }
        this.addressElement=elements.create('address',options)
      }else{
        throw new Error("elements instans has not benn loaded")
      }
     
    }
    return this.addressElement;
  }
  async createPaymentElement(){
    if(!this.paymentElement){
      const elements=await this.initializeElements();
      if(elements){
        this.paymentElement=elements.create('payment');
      }
      else{
        throw new Error("elements instans has not benn loaded")
      }
    }
    return this.paymentElement;
  }
  async CreateInformationToken(){
    const stripe=await this.getStripeInstance();
    const elements=await this.initializeElements();
    const result=await elements.submit();
    if(result.error) throw new Error(result.error.message);
    if(stripe){
      return await stripe.createConfirmationToken({elements});
    }
    else{
      throw new Error("stripe has not benn loaded");
    }
  }
  async confirmPayment(confirmationToken:ConfirmationToken){
    const stripe=await this.getStripeInstance();
    const elements=await this.initializeElements();
    const result=await elements.submit();
    if(result.error) throw new Error(result.error.message);
    const clientSecret=this.cartService.cart()?.clientSecret;
    if(stripe&&clientSecret){
      return await stripe.confirmPayment({
        clientSecret:clientSecret,
        confirmParams:{
          confirmation_token:confirmationToken.id,
        },
        redirect:'if_required'
      });
    }
    else{
      throw new Error("stripe has not benn loaded");
    }
  }

  getStripeInstance(){
    return this.stripePromise;
  }
  createOrUpdatePPaymentIntent(){
    const cart=this.cartService.cart();
    if(!cart) throw new Error('problem with cart');
   return this.http.post<Cart>(this.baseUrl+'payment/'+cart.id,{}).pipe(
    map(cart=>{
      this.cartService.cart.set(cart);
      return cart;
    })
   )
  }
  disposeAddressElement(){
   this.elements= undefined;
   this.addressElement=undefined;
   this.paymentElement=undefined;
  }
}
