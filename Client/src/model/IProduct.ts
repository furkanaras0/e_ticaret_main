export interface IProduct {
  id: number;
  name: string;
  price: number;
  isActive: boolean;
  description?: string;
  imgUrl?: string;
  stock?: number;
}