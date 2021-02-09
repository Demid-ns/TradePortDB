import {Product} from './product';

export class Commodity {
  id?: number;
  portId?: number;
  product: Product;
  price: number;
  quantity: number;
  forImport: boolean;
}
