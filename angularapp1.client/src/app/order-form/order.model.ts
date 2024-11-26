export class OrderDetail {
  productId?: number;
  productName?: string;
  price?: number;
  qty?: number;
}

export class OrderViewModel {
  id: number = 0;
  customerId: number = 0;
  orderDate: string = '';
  remarks?: string;  // Optional property
  orderDtls: any[] = [];
}
