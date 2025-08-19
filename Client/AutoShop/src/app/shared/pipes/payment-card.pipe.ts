import { Pipe, PipeTransform } from '@angular/core';
import { ConfirmationToken } from '@stripe/stripe-js';

@Pipe({
  name: 'paymentCard'
})
export class PaymentCardPipe implements PipeTransform {

  transform(value?:ConfirmationToken['payment_method_preview'], ...args: unknown[]): unknown {
    if(value?.card){
      const {brand,last4,exp_month,exp_year}=value.card;
      return `${brand.toLocaleUpperCase()} **** **** **** ${last4} exp ${exp_month}/${exp_year}`;
    }
    return "No payment method found";
  }

}
