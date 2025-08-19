import { Component, inject } from '@angular/core';
import { ShopService } from '../../../core/services/shop.service';
import {  MatDivider } from '@angular/material/divider';
import {  MatList, MatListOption, MatSelectionList } from '@angular/material/list';
import { MatButton } from '@angular/material/button';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { FormsModule } from '@angular/forms';


@Component({
  selector: 'app-filter',
  imports: [
    MatDivider,
    MatSelectionList,
    MatListOption,
    MatButton,
    FormsModule,
   

  ],
  templateUrl: './filter.component.html',
  styleUrl: './filter.component.scss'
})
export class FilterComponent {
 shopService=inject(ShopService);
 private dialogRef=inject(MatDialogRef<FilterComponent>);
 data=inject(MAT_DIALOG_DATA);
 selectedBrands:string[]=this.data.selectedBrands;
  selectedCategories:string[]=this.data.selectedCategories;

  applyFilters(){
    console.log(this.selectedBrands);
    console.log(this.selectedCategories);
    this.dialogRef.close({
      selectedBrands:this.selectedBrands,
      selectedCategories:this.selectedCategories
    }

    )
  }


}
