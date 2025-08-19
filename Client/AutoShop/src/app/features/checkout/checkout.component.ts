import { Component, inject, OnDestroy, OnInit, signal } from '@angular/core';
import { OrderSummeryComponent } from "../../shared/components/order-summery/order-summery.component";
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatCheckboxChange, MatCheckboxModule } from '@angular/material/checkbox'; 
import { MatStep, MatStepper, MatStepperModule } from '@angular/material/stepper';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { StripeService } from '../../core/services/stripe.service';
import { Router, RouterLink } from '@angular/router';
import { ConfirmationToken, StripeAddressElement, StripeAddressElementChangeEvent, StripePaymentElement, StripePaymentElementChangeEvent } from '@stripe/stripe-js';
import { SnackbarService } from '../../core/services/snackbar.service';
import { StepperSelectionEvent } from '@angular/cdk/stepper';
import { Address } from '../../shared/models/user';
import { AccountService } from '../../core/services/account.service';
import { firstValueFrom } from 'rxjs';
import { CheckoutDeliveryComponent } from "./checkout-delivery/checkout-delivery.component";
import { CheckoutReviewComponent } from "./checkout-review/checkout-review.component";
import { CartService } from '../../core/services/cart.service';
import { CurrencyPipe } from '@angular/common';

@Component({
  selector: 'app-checkout',
  imports: [
    OrderSummeryComponent,
    MatStepperModule,
    MatFormFieldModule,
    MatIconModule,
    MatInputModule,
    MatButtonModule,
    MatCheckboxModule,
    MatProgressSpinnerModule,
    RouterLink,
    CheckoutDeliveryComponent,
    CheckoutReviewComponent,
    CurrencyPipe
  ],
  templateUrl: './checkout.component.html',
})
export class CheckoutComponent implements OnInit ,OnDestroy {
  loadig=false;
  stripeService=inject(StripeService);
  snackBar=inject(SnackbarService);
  accountService=inject(AccountService);
  cartService=inject(CartService);
  private router=inject(Router);
  addressElements?:StripeAddressElement;
  paymentElement?:StripePaymentElement;
  confirmationToken?:ConfirmationToken;
  completionStatus=signal<{address:boolean,card:boolean
    delivery:boolean
  }>({
    address:false,
    card:false,
    delivery:false
  })

  saveAddress=false;
  async ngOnInit() {
      try{
        this.addressElements=await this.stripeService.createAddressElements();
        this.addressElements.mount("#address-element");
        this.addressElements.on('change', this.handleAddressChange);

        this.paymentElement=await this.stripeService.createPaymentElement();
        this.paymentElement.mount("#payment-element");
        this.paymentElement.on('change', this.handlePaymentChange);


      }catch(error:any){
        this.snackBar.error(error.message);
      }
  }
  handleAddressChange=(event:StripeAddressElementChangeEvent)=>{
    this.completionStatus.update((status) => {
      status.address = event.complete;
      return status;
    });
  }
  handlePaymentChange=(event:StripePaymentElementChangeEvent)=>{
    this.completionStatus.update((status) => {
      status.card = event.complete;
      return status;
    });
  }
  async onStepChange(event: StepperSelectionEvent) {
    if (event.selectedIndex === 1 && !this.addressElements) {
     const address=await this.getAddressFromStripeAddress();
    address&& firstValueFrom(this.accountService.updateAddress(address))

  }
  if(event.selectedIndex===2){
    await firstValueFrom(this.stripeService.createOrUpdatePPaymentIntent());
  }
  if(event.selectedIndex===3){
    await this.getConfirmationToken();
  }
  }
  private async getAddressFromStripeAddress():Promise<Address|null> {
    const result=await this.addressElements?.getValue();
    const address=result?.value.address;

    if(address){
      return {
        line1: address.line1 ,
        line2: address.line2 ||null ,
        city: address.city ,
        state: address.state ,
        postalCode: address.postal_code ,
        country: address.country
      };
    }
    return null;
  }
  async getConfirmationToken(){
   try{
    if(Object.values(this.completionStatus()).every(status=>status===true)){
     const result=await this.stripeService.CreateInformationToken();
     if(result.error) throw new Error(result.error.message);
     this.confirmationToken=result.confirmationToken;
    }
   }catch(error:any){
    this.snackBar.error(error.message);
    
   }
  }
async confirmPayment(stepper:MatStepper){
  this.loadig = true;
  try{
    if(this.confirmationToken ){
      const result= await this.stripeService.confirmPayment(this.confirmationToken);
      if(result.error) throw new Error(result.error.message);
      else{
        console.log(result);
        this.cartService.deleteCart();
        this.cartService.selectedDeliveryMethod.set(null);
        this.router.navigateByUrl('/checkout/success');
      }
    }
  }catch(error:any){
    this.snackBar.error(error.message);
    stepper.previous();
  }
  finally{
    this.loadig = false;
  }
}
  handleDeliveryComplete(event: boolean) {
    this.completionStatus.update((status) => {
      status.delivery = event;
      return status;
    });
  }
  onSaveAddressCheckBox(event: MatCheckboxChange) {
    this.saveAddress = event.checked;
  }
  ngOnDestroy(): void {
   
    this.stripeService.disposeAddressElement();
  }


}
