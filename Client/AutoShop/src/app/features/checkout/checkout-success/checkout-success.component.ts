import { Component } from '@angular/core';
import { DatePipe } from '@angular/common';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-checkout-success',
  imports: [RouterLink,DatePipe],
  templateUrl: './checkout-success.component.html',
  styleUrl: './checkout-success.component.scss',
  providers: [DatePipe]
})
export class CheckoutSuccessComponent {
  currentDate = new Date();
}
