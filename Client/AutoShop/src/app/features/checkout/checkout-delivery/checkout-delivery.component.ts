import { Component, inject, OnInit, output } from '@angular/core';
import { CheckoutService } from '../../../core/servcies/checkout.service';
import { CurrencyPipe } from '@angular/common';
import { MatRadioModule } from '@angular/material/radio';
import { CartService } from '../../../core/services/cart.service';
import { DeliveryMethod } from '../../../shared/models/deliveryMethod';

@Component({
  selector: 'app-checkout-delivery',
  imports: [
    CurrencyPipe,
    MatRadioModule
  
  ],
  templateUrl: './checkout-delivery.component.html',
  styleUrl: './checkout-delivery.component.scss'
})
export class CheckoutDeliveryComponent  implements OnInit {
  checkoutService = inject(CheckoutService);
  cartService=inject(CartService);
  deliveryComplete = output<boolean>();

  ngOnInit(): void {
   this.checkoutService.getDeliveryMethods().subscribe(methods => {
      const method=methods.find(x=>x.id==this.cartService.cart()?.deliveryMethodId);
      if(method){
        this.cartService.selectedDeliveryMethod.set(method);
        this.deliveryComplete.emit(true);
      }
   });
  }
  updateDeliveryMethod(deliveryMethod: DeliveryMethod) {
    this.cartService.selectedDeliveryMethod.set(deliveryMethod);
    const cart = this.cartService.cart();
    if (cart) {
      cart.deliveryMethodId = deliveryMethod.id;
      this.cartService.setCart(cart);
    this.deliveryComplete.emit(true);
      

    }
}
   
  }
