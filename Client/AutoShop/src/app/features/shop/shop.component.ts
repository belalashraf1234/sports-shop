import { Component, inject, OnInit } from '@angular/core';
import { ShopService } from '../../core/services/shop.service';
import { Product } from '../../shared/models/product';

import { MatDialog } from '@angular/material/dialog';
import { ProductItemComponent } from './product-item/product-item.component';
import { FilterComponent } from './filter/filter.component';
import { MatButton } from '@angular/material/button';
import { MatIcon } from '@angular/material/icon';
import { MatListOption, MatSelectionList, MatSelectionListChange } from '@angular/material/list';
import { MatMenu, MatMenuTrigger } from '@angular/material/menu';
import { ShopParams } from '../../shared/models/shopParams';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { Pagination } from '../../shared/models/pagination';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-shop',
  imports: [ MatButton, ProductItemComponent, MatIcon,
    MatSelectionList,
    MatListOption,
    MatMenu,
    MatMenuTrigger,
    MatPaginator,
    FormsModule
  ],
  templateUrl: './shop.component.html',
  styleUrl: './shop.component.scss',
})
export class ShopComponent implements OnInit {
  private shopService = inject(ShopService);
  products?: Pagination<Product>;


  private dialogService = inject(MatDialog);

  sortOptions=[
    {name:"Alphabetical",value:"name"},
    {name:"Price:Low-Heigh",value:"PriceAsc"},
    {name:"Price:Heigh-Low",value:"PriceDesc"}
  ];
  pageSizeOptions=[5,10,15];
  shopParmas=new ShopParams();

  ngOnInit(): void {
    this.intializeShop();
  }
  intializeShop() {
    this.shopService.getBrands();
    this.shopService.getCategories();
    this.getProducts();
    
  }

  getProducts(){
    this.shopService.getProducts(this.shopParmas).subscribe({
      next: (res) => {
        this.products = res;
      },
      error: (err: any) => {
        console.log(err);
      },
      complete: () => {
        console.log('complete');
      },
    });
  }
  openFilterDialog() {
    const dialogRef = this.dialogService.open(FilterComponent, {
      minWidth: '500px',
      data: {
        selectedBrands: this.shopParmas.brands,
        selectedCategories: this.shopParmas.categories,
      },
    });
    dialogRef.afterClosed().subscribe({
      next: (result) => {
        if (result) {
          console.log(result);
          this.shopParmas.brands = result.selectedBrands;
            this.shopParmas.categories = result.selectedCategories;
            this.shopParmas.pageNumber=1;
           this.getProducts();
        }
      },
    });
  }
  onSortChange(event:MatSelectionListChange){
    const selectedoption=event.options[0];
    if(selectedoption){
      this.shopParmas.sort=selectedoption.value;
      console.log(this.shopParmas.sort);
      this.shopParmas.pageNumber=1
      this.getProducts();
    }
  }
  handelPageEvent(event:PageEvent){
    this.shopParmas.pageNumber=event.pageIndex+1;
    this.shopParmas.pageSize=event.pageSize;
    this.getProducts();
  }
  onSearchChange(){
    this.shopParmas.pageNumber=1;
    this.getProducts();
  }
}
